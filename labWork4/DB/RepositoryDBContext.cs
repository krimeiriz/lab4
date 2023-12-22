using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using labWork4.Models;
using Microsoft.EntityFrameworkCore;

namespace labWork4.DB
{
    public class RepositoryDBContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }

        public string DbPath { get; } = DEFAULT_SOURCE_PATH;
        private const string DEFAULT_SOURCE_PATH = "repository_SQLite.db";

        public RepositoryDBContext() : this(null)
        {
            
        }
        public RepositoryDBContext(string? soursePath)
        {
            if (!string.IsNullOrEmpty(soursePath))
            {
                DbPath = soursePath;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
