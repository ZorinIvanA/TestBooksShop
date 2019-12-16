using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Entities;

namespace Sirena.Books.Domain.Interfaces
{
    public interface IBooksService
    {
        Task<Book[]> GetByParamsAsync(bool? isExists, int[] types, 
            decimal? minCost, decimal? maxCost,string Author, string name, 
            CancellationToken cancellationToken);

        Task BuyAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Book book, CancellationToken cancellationToken);
    }
}
