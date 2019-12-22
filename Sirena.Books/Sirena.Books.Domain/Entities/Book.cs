using System;
using System.Collections.Generic;
using System.Text;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Domain.Entities
{
    /// <summary>
    /// Книга
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Id книги
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название книги
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Цена книги
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Тип книги
        /// </summary>
        public BookType Type { get; set; }
        /// <summary>
        /// Осталось на складе
        /// </summary>
        public int RestInStorage { get; set; }
    }
}
