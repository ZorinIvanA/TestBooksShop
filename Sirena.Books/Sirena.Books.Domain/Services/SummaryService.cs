using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Enums;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Domain.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IBooksRepository _booksRepository;
        public SummaryService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        public async Task<IDictionary<DateTime, int>> GetSoldByTimesAsync(DateTime? minDate, DateTime? maxDate, CancellationToken cancellationToken)
        {
            if (minDate == null)
                minDate = DateTime.MinValue;
            if (maxDate == null)
                maxDate = DateTime.MaxValue;

            return new Dictionary<DateTime, int>(
                (await _booksRepository.GetSalesByTimesAsync(
                    minDate.Value, maxDate.Value, cancellationToken)));
        }

        public async Task<IDictionary<BookType, int>> GetSoldByTypesAsync(DateTime? minDate, DateTime? maxDate, CancellationToken cancellationToken)
        {
            if (minDate == null)
                minDate = DateTime.MinValue;
            if (maxDate == null)
                maxDate = DateTime.MaxValue;

            IDictionary<BookType, int> result = new Dictionary<BookType, int>();
            var dataFromRepo = await _booksRepository.GetSalesByTypesAsync(
                minDate.Value, maxDate.Value, cancellationToken);
            foreach (var dataItem in dataFromRepo)
            {
                if (Enum.IsDefined(typeof(BookType), dataItem.Key))
                    result.Add((BookType)dataItem.Key, dataItem.Value);
            }

            return result;
        }
    }
}
