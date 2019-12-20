using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Enums;

namespace Sirena.Books.Domain.Interfaces
{
    public interface ISummaryService
    {
        Task<IDictionary<DateTime, int>> GetSoldByTimesAsync(
            DateTime? minDate, DateTime? maxDate,
            CancellationToken cancellationToken);

        Task<IDictionary<BookType, int>> GetSoldByTypesAsync(
            DateTime? minDate, DateTime? maxDate,
            CancellationToken cancellationToken);
    }
}
