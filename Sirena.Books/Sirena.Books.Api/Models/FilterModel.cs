using System.ComponentModel.DataAnnotations;

namespace Sirena.Books.Api.Models
{
    public class FilterModel
    {
        public bool? Exists { get; set; }
        public int[] Types { get; set; }
        [DataType(DataType.Currency)]
        public decimal? MinCost { get; set; }
        [DataType(DataType.Currency)]
        public decimal? MaxCost { get; set; }
        public string Author{ get; set; }
        public string Name { get; set; }
    }
}