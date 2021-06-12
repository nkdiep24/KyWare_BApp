using KyWare.BApp.Data.Models;
using KyWare.BApp.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KyWare.BApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _service { get; set; }

        public BooksController(BooksService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult AddBook([FromBody] Book book)
        {
            _service.AddBook(book);
            return Ok();
        }
    }
}
