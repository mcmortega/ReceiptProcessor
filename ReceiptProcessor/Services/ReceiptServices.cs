using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using ReceiptProcessor.Models;
using System.Text.RegularExpressions;

namespace ReceiptProcessor.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly IAmazonRekognition _rekognitionClient;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;
        private readonly string _tableName;

        public ReceiptService(
            IAmazonS3 s3Client,
            IAmazonDynamoDB dynamoDbClient,
            IAmazonRekognition rekognitionClient,
            IConfiguration configuration)
        {
            _s3Client = s3Client;
            _dynamoDbClient = dynamoDbClient;
            _rekognitionClient = rekognitionClient;
            _configuration = configuration;
            _bucketName = configuration["AWS:S3BucketName"] ?? throw new ArgumentNullException("S3BucketName");
            _tableName = configuration["AWS:DynamoDBTableName"] ?? throw new ArgumentNullException("DynamoDBTableName");
        }

        public async Task<ReceiptUploadResult> UploadAndProcessReceiptAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new ReceiptUploadResult
                    {
                        Success = false,
                        Message = "No file provided"
                    };
                }

                // Validate file type
                var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
                if (!allowedTypes.Contains(file.ContentType.ToLower()))
                {
                    return new ReceiptUploadResult
                    {
                        Success = false,
                        Message = "Only JPEG and PNG images are allowed"
                    };
                }

                // Generate unique receipt ID
                var receiptId = Guid.NewGuid().ToString();
                var s3Key = $"receipts/{DateTime.UtcNow:yyyy/MM/dd}/{receiptId}{Path.GetExtension(file.FileName)}";

                // Upload to S3
                using var stream = file.OpenReadStream();
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = s3Key,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(putRequest);

                // Process with Rekognition
                var detectTextRequest = new DetectTextRequest
                {
                    Image = new Image
                    {
                        S3Object = new Amazon.Rekognition.Model.S3Object
                        {
                            Bucket = _bucketName,
                            Name = s3Key
                        }
                    }
                };

                var detectTextResponse = await _rekognitionClient.DetectTextAsync(detectTextRequest);

                // Extract text
                var extractedText = new List<string>();
                string? totalAmount = null;
                string? merchantName = null;

                foreach (var text in detectTextResponse.TextDetections)
                {
                    if (text.Type == TextTypes.LINE)
                    {
                        extractedText.Add(text.DetectedText);

                        // Try to find total amount
                        if (totalAmount == null &&
                            (text.DetectedText.ToUpper().Contains("TOTAL") ||
                             text.DetectedText.ToUpper().Contains("AMOUNT")))
                        {
                            var match = Regex.Match(text.DetectedText, @"[\$]?(\d+[.,]\d{2})");
                            if (match.Success)
                            {
                                totalAmount = match.Groups[1].Value;
                            }
                        }

                        // First line is often merchant name
                        if (merchantName == null && extractedText.Count == 1)
                        {
                            merchantName = text.DetectedText;
                        }
                    }
                }

                // Store in DynamoDB
                var receipt = new Receipt
                {
                    ReceiptId = receiptId,
                    UploadDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    S3Key = s3Key,
                    ExtractedText = extractedText,
                    TotalAmount = totalAmount,
                    MerchantName = merchantName,
                    ProcessedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };

                await SaveReceiptToDynamoDbAsync(receipt);

                return new ReceiptUploadResult
                {
                    Success = true,
                    Message = "Receipt processed successfully",
                    ReceiptId = receiptId,
                    ExtractedText = extractedText,
                    TotalAmount = totalAmount
                };
            }
            catch (Exception ex)
            {
                return new ReceiptUploadResult
                {
                    Success = false,
                    Message = $"Error processing receipt: {ex.Message}"
                };
            }
        }

        public async Task<List<Receipt>> GetAllReceiptsAsync()
        {
            try
            {
                var table = Table.LoadTable(_dynamoDbClient, _tableName);
                var scanConfig = new ScanOperationConfig();
                var search = table.Scan(scanConfig);

                var receipts = new List<Receipt>();
                do
                {
                    var documents = await search.GetNextSetAsync();
                    foreach (var document in documents)
                    {
                        receipts.Add(DocumentToReceipt(document));
                    }
                } while (!search.IsDone);

                return receipts.OrderByDescending(r => r.UploadDate).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting receipts: {ex.Message}");
                return new List<Receipt>();
            }
        }

        public async Task<Receipt?> GetReceiptByIdAsync(string receiptId)
        {
            try
            {
                var table = Table.LoadTable(_dynamoDbClient, _tableName);
                var document = await table.GetItemAsync(receiptId);

                return document != null ? DocumentToReceipt(document) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting receipt: {ex.Message}");
                return null;
            }
        }

        private async Task SaveReceiptToDynamoDbAsync(Receipt receipt)
        {
            var table = Table.LoadTable(_dynamoDbClient, _tableName);
            var document = new Document
            {
                ["ReceiptId"] = receipt.ReceiptId,
                ["UploadDate"] = receipt.UploadDate,
                ["S3Key"] = receipt.S3Key,
                ["ExtractedText"] = receipt.ExtractedText,
                ["ProcessedAt"] = receipt.ProcessedAt
            };

            if (!string.IsNullOrEmpty(receipt.TotalAmount))
                document["TotalAmount"] = receipt.TotalAmount;

            if (!string.IsNullOrEmpty(receipt.MerchantName))
                document["MerchantName"] = receipt.MerchantName;

            await table.PutItemAsync(document);
        }

        private Receipt DocumentToReceipt(Document document)
        {
            var receipt = new Receipt
            {
                ReceiptId = document["ReceiptId"].AsString(),
                UploadDate = document["UploadDate"].AsString(),
                S3Key = document["S3Key"].AsString(),
                ProcessedAt = document["ProcessedAt"].AsString()
            };

            if (document.ContainsKey("ExtractedText"))
                receipt.ExtractedText = document["ExtractedText"].AsListOfString();

            if (document.ContainsKey("TotalAmount"))
                receipt.TotalAmount = document["TotalAmount"].AsString();

            if (document.ContainsKey("MerchantName"))
                receipt.MerchantName = document["MerchantName"].AsString();

            return receipt;
        }
    }
}