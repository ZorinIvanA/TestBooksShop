using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Sirena.Books.Domain.Entities;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Infrastructure.Dto
{
    public class BookDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public decimal price { get; set; }
        public int book_type { get; set; }
        public int storage { get; set; }

        public Book ToEntity()
        {
            return new Book
            {
                Type = (BookType)book_type,
                Price = price,
                Name = name,
                Author = author,
                Id = id,
                RestInStorage = storage
            };
        }
    }
}
