using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirena.Books.Api.Models
{
    public class StatByTimeResultModel
    {
        public TimeSpan SellInterval { get; set; }
        public int SoldBooks { get; set; }

        public static StatByTimeResultModel FromEnity(KeyValuePair<TimeSpan, int> data)
        {
            return new StatByTimeResultModel
            {
                SellInterval = data.Key,
                SoldBooks = data.Value
            };
        }
    }
}
