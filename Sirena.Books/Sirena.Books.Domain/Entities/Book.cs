using System;
using System.Collections.Generic;
using System.Text;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public BookType Type { get; set; }
        public int RestInStorage { get; set; }
    }
}
