using Sirena.Books.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using Sirena.Books.Domain.Entities;
using Sirena.Books.Infrastructure.Configuration;
using Sirena.Books.Infrastructure.Dto;
using Newtonsoft.Json;

namespace Sirena.Books.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly DbInfo _info;

        public BooksRepository(IOptions<DbInfo> dbInfo)
        {
            _info = dbInfo?.Value ?? throw new ArgumentNullException(nameof(dbInfo));
        }

        internal IDbConnection Connection => new NpgsqlConnection(_info.Connection);

        public async Task<Book[]> GetBooksByParamsAsync(bool? isExists,
            int[] types, decimal? minCost, decimal? maxCost, string author,
            string name, CancellationToken cancellationToken)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("_exists", isExists, DbType.Boolean);
            queryParameters.Add("_types", types?.ToString(), DbType.String);
            queryParameters.Add("_minCost", minCost, DbType.Decimal);
            queryParameters.Add("_maxCost", maxCost, DbType.Decimal);
            queryParameters.Add("_author", author, DbType.String);
            queryParameters.Add("_name", name, DbType.String);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return (await dbConnection.QueryAsync<BookDto>(
                        "common.get_books_by_params", queryParameters,
                        commandType: CommandType.StoredProcedure))
                    .Select(x => x.ToEntity()).ToArray();
            }
        }

        public async Task AddBookAsync(Book book, CancellationToken cancellationToken)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("_name", book.Name);
            queryParameters.Add("_author", book.Author);
            queryParameters.Add("_price", book.Price, DbType.Decimal);
            queryParameters.Add("_type", book.Price, DbType.Int32);
            queryParameters.Add("_storage", book.RestInStorage, DbType.Int32);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                await dbConnection.QueryFirstOrDefaultAsync<int>(
                    "common.add_book", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task BuyBookAsync(int id, CancellationToken cancellationToken)
        {

        }

        public async Task<int[]> GetSalesByTimesAsync(DateTime minDate, DateTime maxDate, CancellationToken cancellationToken)
        {
            return new int[0];
        }

        public async Task<int[]> GetSalesByTypesAsync(DateTime minDate, DateTime maxDate, CancellationToken cancellationToken)
        {
            return new int[0];
        }
    }
}
