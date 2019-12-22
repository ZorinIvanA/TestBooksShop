using System;
using System.Collections.Generic;

namespace Sirena.Books.Api.Models
{
    /// <summary>
    /// Модель статистики по времени
    /// </summary>
    public class StatByTimeResultModel
    {
        public DateTime SellInterval { get; set; }
        public int SoldBooks { get; set; }

        public static StatByTimeResultModel FromEnity(KeyValuePair<DateTime, int> data)
        {
            return new StatByTimeResultModel
            {
                SellInterval = data.Key,
                SoldBooks = data.Value
            };
        }
    }
}
