namespace LibraryManagementSystem.Services
{
    public interface IReportService
    {
        string GenerateReaderReport(int readerId);
        string GenerateBorrowedBooksReport();
        string GenerateBookInventoryReport();
    }
} 