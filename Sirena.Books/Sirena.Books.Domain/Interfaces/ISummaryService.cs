using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Domain.Interfaces
{
    /// <summary>
    /// Служба взаимодействия со статистикой
    /// </summary>
    public interface ISummaryService
    {
        /// <summary>
        /// Возвращает статистику по продажам по датам
        /// </summary>
        /// <param name="minDate">Дата начала периода статистики</param>
        /// <param name="maxDate">Дата конца периода статистики</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<IDictionary<DateTime, int>> GetSoldByTimesAsync(
            DateTime? minDate, DateTime? maxDate,
            CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает статистику продаж по типу
        /// </summary>
        /// <param name="minDate">Дата начала периода</param>
        /// <param name="maxDate">Дата конца периода</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<IDictionary<BookType, int>> GetSoldByTypesAsync(
            DateTime? minDate, DateTime? maxDate,
            CancellationToken cancellationToken);
    }
}
