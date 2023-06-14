using My_Books.Data.Models;

namespace My_Books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book
                    {
                        Title = "1st book title",
                        Description = "1st boo description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate=6,
                        Genre="Document",
                        CoverUrl="https......",
                        DateAdded=DateTime.Now
                    },
                    new Book
                    {
                        Title = "2nd book title",
                        Description = "2nd boo description",
                        IsRead = false,
                        Genre = "Document",
                        CoverUrl = "https......",
                        DateAdded = DateTime.Now

                    }
                    ) ;
                    context.SaveChanges();
                }
                
            }
        }
    }
}
