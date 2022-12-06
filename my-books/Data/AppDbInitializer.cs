//using my_books.Data.Models;

//namespace my_books.Data
//{
//    public class AppDbInitializer
//    {
//        public static void Seed(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                


//                if (!context.Books.Any())
//                {
//                    context.Books.AddRange(new List<Book>()
//                    {
//                        new Book()
//                        {
//                            Title = "Harry Potter",
//                            Description = "Just read",
//                            IsRead = true,
//                            DateRead = DateTime.Now.AddDays(-3),
//                            Rate = 8,
//                            Genre = "Fantastic",
//                            Author = "J.K.Rowling",
//                            CoverUrl = "shit",
//                            DateAdded = DateTime.Now.AddDays(-10),  
//                        },
//                        new Book()
//                        {
//                            Title = "Metro 2034",
//                            Description = "Just read",
//                            IsRead = false,
//                            Genre = "Thriller",
//                            Author = "IWILLWRITE",
//                            CoverUrl = "shit",
//                            DateAdded = DateTime.Now.AddDays(-7),
//                        }
//                    });

//                    context.SaveChanges();
//                }
//            }
//        }
//    }
//}
