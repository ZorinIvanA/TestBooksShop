using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Api.Models
{
    /// <summary>
    /// Модель статистики продаж по типам
    /// </summary>
    public class StatByTypesResultModel
    {
        public BookType BookType { get; set; }
        public int SoldBooks { get; set; }

        public static StatByTypesResultModel FromEnity(KeyValuePair<BookType, int> data)
        {
            return new StatByTypesResultModel
            {
                BookType = data.Key,
                SoldBooks = data.Value
            };
        }
    }
}
