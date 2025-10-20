using ReceiptProcessor.Models;

namespace ReceiptProcessor.Services
{
    public interface IReceiptService
    {
        Task<ReceiptUploadResult> UploadAndProcessReceiptAsync(IFormFile file);
        Task<List<Receipt>> GetAllReceiptsAsync();
        Task<Receipt?> GetReceiptByIdAsync(string receiptId);
    }
}