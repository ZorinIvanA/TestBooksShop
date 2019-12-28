using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Entities;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Domain.Interfaces
{
    /// <summary>
    /// Репозиторий для взаимодействия с книгами
    /// </summary>
    public interface IBooksRepository
    {
        /// <summary>
        /// Возвращает список книг по переданным параметрам
        /// </summary>
        /// <param name="isExists">Есть ли на складе</param>
        /// <param name="types">Список типов книг</param>
        /// <param name="minCost">Минимальная цена</param>
        /// <param name="maxCost">Максимальная цена</param>
        /// <param name="author">Первые буквы имени автора</param>
        /// <param name="name">Первые буквы названия книги</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Book[]> GetBooksByParamsAsync(bool? isExists, int[] types,
            decimal? minCost, decimal? maxCost, string author, string name,
            CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет книгу
        /// </summary>
        /// <param name="book">Сведения о книге</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task AddBookAsync(Book book, CancellationToken cancellationToken);

        /// <summary>
        /// Покупка книги
        /// </summary>
        /// <param name="id">Id покупаемой книги</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task BuyBookAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список продаж по датам
        /// </summary>
        /// <param name="minDate">Минимальная дата</param>
        /// <param name="maxDate">Максимальная дата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<IDictionary<DateTime, int>> GetSalesByTimesAsync(DateTime minDate, DateTime maxDate,
            CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список продаж по типам
        /// </summary>
        /// <param name="minDate">Дата начала периода</param>
        /// <param name="maxDate">Дата окончания периода</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<IDictionary<BookType,int>> GetSalesByTypesAsync(DateTime minDate, DateTime maxDate,
            CancellationToken cancellationToken);
    }
}
