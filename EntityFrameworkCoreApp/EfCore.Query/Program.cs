using EfCore.Query.Data.Context;
using EfCore.Query.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EfCore.Query
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //blog tablosuna data ekleyelim
            var context = new BlogContext();
            //context.Blogs.Add(new Blog 
            //{
            //    Title = "Blog-1",
            //    Url = "ysk.com/blog-1"
            //});
            //context.Blogs.Add(new Blog
            //{
            //    Title = "Blog-2",
            //    Url = "ysk.com/blog-2"
            //});
            //context.Blogs.Add(new Blog
            //{
            //    Title = "Blog-3",
            //    Url = "ysk.com/blog-3"
            //});
            //context.Blogs.Add(new Blog
            //{
            //    Title = "Blog-4",
            //    Url = "ysk.com/blog-4"
            //});
            //context.SaveChanges();

            //verileri çekmek
            //IEnumerable && IQueryable
            //var blogs = context.Blogs.AsEnumerable();
            ////db 'ye gidip bir daha sorgu atmadı.
            //blogs.Where(x => x.Title.Contains("Blog-1") || x.Title.Contains("Blog-2"));
            //foreach (var item in blogs)
            //{
            //    Console.WriteLine(item.Title);
            //}

            //Tracking 
            //var updatedBlog = context.Blogs.SingleOrDefault(x => x.Id == 1);
            //updatedBlog.Title = "Güncellendi";
            //var updatedBlogState = context.Entry(updatedBlog).State;
            //context.SaveChanges();

            // no tracking
            //var updatedBlog = context.Blogs.AsNoTracking().SingleOrDefault(x => x.Id == 1);
            //updatedBlog.Title = "Güncellendi";
            //var updatedBlogState = context.Entry(updatedBlog).State;
            //context.SaveChanges();

            //lazy loading 
            //var blogs = context.Blogs.ToList();
            //foreach (var blog in blogs)
            //{
            //    Console.WriteLine($"{blog.Title} Blogun yorumları");
            //    foreach (var comment in blog.Comments)
            //    {
            //        Console.WriteLine($"     {comment.Content}");
            //    }
            //}

            //eagle loading
            //var blogs = context.Blogs.Include(x=>x.Comments.Where(x=> x.Content.Contains("Yorum1"))).ToList();
            //foreach (var blog in blogs)
            //{
            //    Console.WriteLine($"{blog.Title} Blogun yorumları");
            //    foreach (var comment in blog.Comments)
            //    {
            //        Console.WriteLine($"\t \t{comment.Content}");
            //    }
            //}

            //explicit loading
            var blog = context.Blogs.SingleOrDefault(x => x.Id == 1);
            context.Entry(blog).Collection(x => x.Comments).Load();
            foreach (var item in blog.Comments)
            {
                Console.WriteLine(item.Content);
            }


            Console.WriteLine("Hello World!");
        }
    }
}
