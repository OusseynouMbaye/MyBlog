using System.Text.Json;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Migrations
{
    public class JsonDataMigrator
    {
        private readonly BlogDbContext _context;

        public JsonDataMigrator(BlogDbContext context)
        {
            _context = context;
        }

        public async Task MigrateAsync()
        {
            Console.WriteLine("🔄 Début de la migration des données JSON vers SQLite...");

            await MigrateBlogPostsAsync();
            await MigrateCategoriesAsync();
            await MigrateTagsAsync();
            await MigrateCommentsAsync();

            Console.WriteLine("✅ Migration terminée !");
        }

        private async Task MigrateBlogPostsAsync()
        {
            string blogPostsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Blogposts");

            if (!Directory.Exists(blogPostsPath))
            {
                Console.WriteLine("⚠️ Dossier Blogposts introuvable !");
                return;
            }

            foreach (var file in Directory.GetFiles(blogPostsPath, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var blogPost = JsonSerializer.Deserialize<BlogPost>(json);

                if (blogPost != null)
                {
                    if (!await _context.BlogPosts.AnyAsync(b => b.Id == blogPost.Id))
                    {
                        _context.BlogPosts.Add(blogPost);
                        Console.WriteLine($"📝 Ajout de l'article {blogPost.Title}");
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task MigrateCategoriesAsync()
        {
            string categoriesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Categories");

            if (!Directory.Exists(categoriesPath))
            {
                Console.WriteLine("⚠️ Dossier Categories introuvable !");
                return;
            }

            foreach (var file in Directory.GetFiles(categoriesPath, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var category = JsonSerializer.Deserialize<Category>(json);

                if (category != null)
                {
                    if (!await _context.Categories.AnyAsync(c => c.Id == category.Id))
                    {
                        _context.Categories.Add(category);
                        Console.WriteLine($"📝 Ajout de la catégorie {category.Name}");
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task MigrateTagsAsync()
        {
            string tagsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Tags");

            if (!Directory.Exists(tagsPath))
            {
                Console.WriteLine("⚠️ Dossier Tags introuvable !");
                return;
            }

            foreach (var file in Directory.GetFiles(tagsPath, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var tag = JsonSerializer.Deserialize<Tag>(json);

                if (tag != null)
                {
                    if (!await _context.Tags.AnyAsync(t => t.Id == tag.Id))
                    {
                        _context.Tags.Add(tag);
                        Console.WriteLine($"📝 Ajout du tag {tag.Name}");
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task MigrateCommentsAsync()
        {
            string commentsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Comments");

            if (!Directory.Exists(commentsPath))
            {
                Console.WriteLine("⚠️ Dossier Comments introuvable !");
                return;
            }

            foreach (var file in Directory.GetFiles(commentsPath, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var comment = JsonSerializer.Deserialize<Comment>(json);

                if (comment != null)
                {
                    if (!await _context.Comments.AnyAsync(c => c.Id == comment.Id))
                    {
                        _context.Comments.Add(comment);
                        Console.WriteLine($"📝 Ajout du commentaire {comment.Text}");
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
