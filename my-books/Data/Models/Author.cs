namespace my_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }


        //Relations
        public List<Book_Author>? Books_Authors { get; set; }
    }
}
