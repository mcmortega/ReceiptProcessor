namespace ReceiptProcessor.Models
{
    public class Receipt
    {
        public string ReceiptId { get; set; } = string.Empty;
        public string UploadDate { get; set; } = string.Empty;
        public string S3Key { get; set; } = string.Empty;
        public List<string> ExtractedText { get; set; } = new();
        public string? TotalAmount { get; set; }
        public string ProcessedAt { get; set; } = string.Empty;
        public string? MerchantName { get; set; }
    }

    public class ReceiptUploadResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ReceiptId { get; set; }
        public List<string> ExtractedText { get; set; } = new();
        public string? TotalAmount { get; set; }
    }
}