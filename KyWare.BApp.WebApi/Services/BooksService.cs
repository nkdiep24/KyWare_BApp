using KyWare.BApp.Data.Models;
using KyWare.BApp.WebApi.Databases;
using System;

namespace KyWare.BApp.WebApi.Services
{
    public class BooksService
    {
        private AppDbContext _db;

        public BooksService(AppDbContext db)
        {
            _db = db;
        }

        public void AddBook(Book book)
        {
            book.DateAdd = DateTime.Now;
            _db.Books.Add(book);
            _db.SaveChanges();
        }
    }
}
