using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext _context;


        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }


        public List<Author> GetAuthors() => _context.Authors.ToList();

        public Author GetAuthorById(int id) => _context.Authors.FirstOrDefault(n => n.Id == id);


        public AuthorWithBooksVM GetAuthorWithBooks(int id)
        {
            var _authorWithBooks = _context.Authors.Where(n => n.Id == id).Select(author => new AuthorWithBooksVM
            {
                FullName = author.FullName,
                BookTitles = author.Books_Authors.Select(x => x.Book.Title).ToList()
            }).FirstOrDefault();

            return _authorWithBooks;
        }

        public void AddAuthor(AuthorVM author)
        {
            var newAuthor = new Author()
            {
                FullName = author.FullName
            };

            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
        }
            

        public Author UpdateAuthor(int id, AuthorVM author) 
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);

            if (_author != null)
            {
                _author.FullName = author.FullName;

                _context.SaveChanges();
            }

            return _author;
        }
        
        public void DeleteAuthor(int id) 
        {
            var author = _context.Authors.FirstOrDefault(n => n.Id == id);

            if(author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }





    }
}
