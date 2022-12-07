using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public List<Publisher> GetPublishers() => _context.Publishers.ToList();


        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);


        public PublisherWithBookAndAuthorsVM GetPublisherWithBookAndAuthor(int id)
        {
            var publisherWithBookAndAuthor = _context.Publishers.Where(n => n.Id == id).Select(publisher => new PublisherWithBookAndAuthorsVM
            {
                Name = publisher.Name,
                BookAuthors = publisher.Books.Select(x => new BookAuthorsVM
                {
                    BookName = x.Title,
                    BookAuthors = x.Books_Authors.Select(k => k.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();

            return publisherWithBookAndAuthor;
        }


        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        public Publisher UpdatePublisher(int id, PublisherVM publisher)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (_publisher != null)
            {
                _publisher.Name = publisher.Name;

                _context.SaveChanges();
            }

            return _publisher;
        }

        public void DeletePublisher(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
        }
    }
}
