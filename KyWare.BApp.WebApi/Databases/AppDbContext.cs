using System;
using KyWare.BApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KyWare.BApp.WebApi.Databases
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
