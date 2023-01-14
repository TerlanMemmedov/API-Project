using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Security.Cryptography.Xml;
using System.Threading;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private readonly AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public List<BookWithAuthorsVM> GetAllBooks()
        {
            //var books = _context.Books
            //    .Include(p => p.Publisher)
            //    .Include(ba => ba.Books_Authors).ThenInclude(a => a.Author)
            //    .ToList();
            //return books;

            var _booksWithAuthors = _context.Books.Select(book => new BookWithAuthorsVM 
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Books_Authors.Select(x => x.Author.FullName).ToList()
            }).ToList();

            return _booksWithAuthors;
        }

        public BookWithAuthorsVM GetBookById(int id)  
        {
            //var book = _context.Books
            //    .Include(n => n.Publisher)
            //    .Include(ba => ba.Books_Authors).ThenInclude(a => a.Author)
            //    .FirstOrDefault(n => n.Id == id);
            //return book;

            var _bookWithAuthors = _context.Books.Where(n => n.Id == id).Select(book => new BookWithAuthorsVM
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Books_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public void AddBook(BookVM book)
        {
            var newBook = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead? book.DateRead.Value : null,
                Rate = book.IsRead? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();


            foreach (var authorId in book.AuthorIds)
            {
                var new_book_author = new Book_Author()
                {
                    BookId = newBook.Id,
                    AuthorId = authorId
                };

                _context.Books_Authors.Add(new_book_author);
                _context.SaveChanges();
            }


        }


        public Book UpdateBook(int id, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == id);

            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _book.PublisherId = book.PublisherId;

                _context.SaveChanges();
            }

            if (_book != null)
            {
                var old_book_authors = _context.Books_Authors.Where(n => n.BookId == _book.Id).ToList();

                if (old_book_authors != null)
                {
                    _context.Books_Authors.RemoveRange(old_book_authors);
                    _context.SaveChanges();
                }
            }

            foreach (var authorId in book.AuthorIds)
            {
                var new_book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = authorId
                };

                _context.Books_Authors.Add(new_book_author);
                _context.SaveChanges();
            }
            
            return _book;
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(n => n.Id == id);

            if (book is null)
            {
                //return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

    }
}
