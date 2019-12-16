using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Entities;

namespace Sirena.Books.Domain.Interfaces
{
    public interface IBooksRepository
    {
        Task<Book[]> GetBooksByParamsAsync(bool? isExists, int[] types,
            decimal? minCost, decimal? maxCost, string author, string name,
            CancellationToken cancellationToken);

        Task AddBookAsync(Book book, CancellationToken cancellationToken);
        Task BuyBookAsync(int id, CancellationToken cancellationToken);
        Task<int[]> GetSalesByTimesAsync(DateTime minDate, DateTime maxDate,
            CancellationToken cancellationToken);

        Task<int[]> GetSalesByTypesAsync(DateTime minDate, DateTime maxDate,
            CancellationToken cancellationToken);
    }
}
