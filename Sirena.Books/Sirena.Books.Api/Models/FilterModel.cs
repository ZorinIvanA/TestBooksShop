using System.ComponentModel.DataAnnotations;

namespace Sirena.Books.Api.Models
{
    /// <summary>
    /// Модель для фильтрации книги
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// Есть ли на складе
        /// </summary>
        public bool? Exists { get; set; }
        /// <summary>
        /// Типы книг
        /// </summary>
        public int[] Types { get; set; }
        /// <summary>
        /// Минимальная цена
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? MinCost { get; set; }
        /// <summary>
        /// Максимальная цена
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? MaxCost { get; set; }
        /// <summary>
        /// Первые буквы имени автора
        /// </summary>
        public string Author{ get; set; }
        /// <summary>
        /// Первые буквы названия книги
        /// </summary>
        public string Name { get; set; }
    }
}