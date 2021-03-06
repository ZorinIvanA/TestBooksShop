﻿using System.ComponentModel.DataAnnotations;
using Sirena.Books.Domain.Entities;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Api.Models
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Author { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public BookType Type { get; set; }
        [Required]
        public int Storage { get; set; }

        /// <summary>
        /// Преобразует в бизнес-сущность
        /// </summary>
        /// <returns></returns>
        public Book ToEntity()
        {
            return new Book
            {
                Author = Author,
                Name = Name,
                Price = Price,
                Type = Type,
                RestInStorage = Storage
            };
        }

        public static BookModel FromEntity(Book entity)
        {
            return new BookModel()
            {
                Author = entity.Author,
                Name = entity.Name,
                Price = entity.Price,
                Type = entity.Type,
                Id = entity.Id,
                Storage = entity.RestInStorage
            };
        }
    }
}