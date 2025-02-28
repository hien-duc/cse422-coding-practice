namespace LibraryManagementSystem.Models
{
    public interface IBook
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Category { get; set; }
        int Quantity { get; set; }
        string BookType { get; set; }
    }
} 