using FeedbackHub.Model.Contracts;
using FeedbackHub.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackHub.Model.DbContexts
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Путь к корню проекта (относительно bin/Debug/netX.X)
            var baseDir = AppContext.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));

            // Абсолютный путь к БД
            var dbFilePath = Path.Combine(projectRoot, "Model", "Database", "feedbackhub.db");

            // Убедись, что папка существует (иначе будет SQLite Error 14)
            Directory.CreateDirectory(Path.GetDirectoryName(dbFilePath)!);

            // Использовать SQLite
            optionsBuilder.UseSqlite($"Data Source={dbFilePath}");

            // (Опционально) логируем путь к БД
            Console.WriteLine("📦 SQLite path: " + dbFilePath);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added && entry.Entity is IHasCreatedAt created)
                {
                    created.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified && entry.Entity is IHasModifiedAt updated)
                {
                    updated.ModifiedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
