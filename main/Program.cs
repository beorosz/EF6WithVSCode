using System;
using System.Linq;
using EF6Test.Context;
using EF6Test.Entities;

namespace EF6Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new BloggingContext())
        {
            // Create and save a new Blog
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();

            var blog = new Blog { Name = name };
            db.Blogs.Add(blog);
            db.Posts.Add(new Post { Blog = blog, Title = "title", Content = "content", Role = Role.SalesRep });
            db.SaveChanges();

            // Display all Blogs from the database
            var query = from b in db.Blogs
                        orderby b.Name
                        select b;

            Console.WriteLine("All blogs in the database:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        }
    }
}