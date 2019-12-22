using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Entities;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Domain.Services
{
    /// <summary>
    /// Служба взаимодействия с сервисом
    /// </summary>
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }
        public Task<Book[]> GetByParamsAsync(bool? isExists, int[] types,
            decimal? minCost, decimal? maxCost, string author, string name,
            CancellationToken cancellationToken)
        {
            return _booksRepository.GetBooksByParamsAsync(isExists,
                types, minCost, maxCost, author, name, cancellationToken);
        }

        public Task BuyAsync(int id, CancellationToken cancellationToken)
        {
            return _booksRepository.BuyBookAsync(id, cancellationToken);
        }

        public Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentNullException(nameof(book));
            if (string.IsNullOrWhiteSpace(book.Name))
                throw new ArgumentNullException(nameof(book));

            return _booksRepository.AddBookAsync(book, cancellationToken);
        }
    }
}
