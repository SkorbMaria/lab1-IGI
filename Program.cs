using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace IGI_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var opt = optionsBuilder.UseSqlite(@"DataSource=.\\DataBase.db").Options;

            using (ApplicationContext context = new ApplicationContext(opt))
            {
                //Init(context);

                SelectAllBooks(context);

                Console.WriteLine("\nPrice more than 15:");
                SelectSomeBooks(context, 15);

                Console.WriteLine("\nTotal sum by Books:");
                SelectTotalSum(context);

                Console.WriteLine("\nOne to many");
                SelectOneToMany(context);

                Console.WriteLine("\nOne to many with min count 4:");
                SelectOneToManyWithMinCount(context, 4);
                
                Console.WriteLine("\n\n\nAdd Book");
                var book = new Books { Cypher = "Cypher4", Name = "Book4", Date = "Date4", Price = 4, Sell = 40, Fee = 800 };
                InsertNewBook(context, book);
                SelectAllBooks(context);

                Console.WriteLine("\n");
                Console.WriteLine("Add Order");
                var order = new Orders
                {
                    Book = context.Books.Where(x => x.Name == "Book4").First(),
                    Customer = context.Customers.Where(x => context.Customers.First().ID == x.ID).First(),
                    RecieveDate = "RecieveDate999",
                    CompleteDate = "CompleteDate999",
                    Count = 999
                };
                InsertNewOrder(context, order);
                SelectOneToMany(context);
                
                Console.WriteLine("\nUpdate Book");
                UpdateBook(context, "Book3", "Book5");
                SelectAllBooks(context);
                
                Console.WriteLine("\nRemove Order");
                RemoveOrder(context, order);
                SelectOneToMany(context);
                
                Console.WriteLine("\nDelete Book");
                RemoveBook(context, book);
                SelectAllBooks(context);
            }
            Console.WriteLine("--");
            Console.Read();
        }

        static void Init(ApplicationContext context)
        {
            context.Autors.Add(new Autors { About = "Autor 1" });
            context.Autors.Add(new Autors { About = "Autor 2" });
            context.Autors.Add(new Autors { About = "Autor 3" });
            context.SaveChanges();

            context.Books.Add(new Books { Cypher = "Cypher1", Name = "Book1", Date = "Date1", Price = 1, Sell = 10, Fee = 200 });
            context.Books.Add(new Books { Cypher = "Cypher2", Name = "Book2", Date = "Date2", Price = 2, Sell = 20, Fee = 400 });
            context.Books.Add(new Books { Cypher = "Cypher3", Name = "Book3", Date = "Date3", Price = 3, Sell = 30, Fee = 600 });
            context.SaveChanges();

            for (int i = 0; i < 3; i++)
                for (int j = i; j < 3; j++)
                    context.Contracts.Add(new Contracts
                    {
                        DateStart = "DateStart" + i.ToString() + j.ToString(),
                        Term = i * 3 + j,
                        DateEnd = "DateEnd" + i.ToString() + j.ToString(),
                        Autor = context.Autors.Where(x => context.Autors.First().ID + j == x.ID).First(),
                        Book = context.Books.Where(x => context.Books.First().ID + i == x.ID).First()
                    });
            context.SaveChanges();

            context.Customers.Add(new Customers { Name = "Customer1", About = "About1" });
            context.Customers.Add(new Customers { Name = "Customer2", About = "About2" });
            context.Customers.Add(new Customers { Name = "Customer3", About = "About3" });
            context.SaveChanges();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    context.Orders.Add(new Orders
                    {
                        Book = context.Books.Where(x => context.Books.First().ID + i == x.ID).First(),
                        Customer = context.Customers.Where(x => context.Customers.First().ID + j == x.ID).First(),
                        RecieveDate = "RecieveDate" + i.ToString() + j.ToString(),
                        CompleteDate = "CompleteDate" + i.ToString() + j.ToString(),
                        Count = i * 3 + j
                    });
            context.SaveChanges();
        }

        static void SelectAllBooks(ApplicationContext context)
        {
            var books = context.Books;

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        static void SelectSomeBooks(ApplicationContext context, int minPrice)
        {
            var books = context.Books
                .Where(x => x.Sell > minPrice);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        static void SelectTotalSum(ApplicationContext context)
        {
            var groupOrders = context.Orders
                                .Include("Book")
                                .Include("Customer")
                                .GroupBy(x => x.Customer.ID);

            foreach (var order in groupOrders)
            {
                Console.WriteLine($"{order.Key} : {order.Sum(x => x.Book.Sell*x.Count)} p.");
            }
        }
        static void SelectOneToMany(ApplicationContext context)
        {
            var orders = context.Orders
                                .Include("Book")
                                .Include("Customer");
            foreach (var order in orders)
            {
                Console.WriteLine($"\t{order.ID} : {order.Book.Name} {order.Customer.Name} {order.Count}");
            }
        }
        static void SelectOneToManyWithMinCount(ApplicationContext context, int minCount)
        {
            var orders = context.Orders
                                .Include("Book")
                                .Include("Customer")
                                .Where(x => x.Count >= minCount);
            foreach (var order in orders)
            {
                Console.WriteLine($"\t{order.ID} : {order.Book.Name} {order.Customer.Name} {order.Count}");
            }
        }
        
        
        static void InsertNewBook(ApplicationContext context, Books book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }
        static void InsertNewOrder(ApplicationContext context, Orders order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }
        static void RemoveBook(ApplicationContext context, Books book)
        {
            context.Books.Remove(book);
            context.SaveChanges();
        }
        static void RemoveOrder(ApplicationContext context, Orders order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }
        static void UpdateBook(ApplicationContext context, string bookName, string newName)
        {
            var book = context.Books.FirstOrDefault(x => x.Name == bookName);

            if (book == null)
            {
                Console.WriteLine("Not founded");
                return;
            }

            book.Name = newName;
            context.SaveChanges();
            Console.WriteLine("Book name changed");
        }
    }
}
