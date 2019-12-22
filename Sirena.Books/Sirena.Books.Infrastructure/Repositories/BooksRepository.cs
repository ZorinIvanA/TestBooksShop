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
    /// <summary>
    /// Репозиторий книг
    /// </summary>
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
            queryParameters.Add("_types",
                JsonConvert.SerializeObject(types), DbType.String);
            queryParameters.Add("_minCost", minCost, DbType.VarNumeric);
            queryParameters.Add("_maxCost", maxCost, DbType.VarNumeric);
            queryParameters.Add("_author", author, DbType.String);
            queryParameters.Add("_name", name, DbType.String);
            var query =
                $"SELECT * FROM common.get_books_by_params({isExists}, '{JsonConvert.SerializeObject(types)}', {minCost}, {maxCost}, '{author}','{name}')";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return (await dbConnection.QueryAsync<BookDto>(
                        query, null,
                        commandType: CommandType.Text))
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
            var queryParameters = new DynamicParameters();
            queryParameters.Add("_id", id);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync(
                    "common.sell_book", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IDictionary<DateTime, int>> GetSalesByTimesAsync(DateTime minDate, DateTime maxDate, CancellationToken cancellationToken)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return new Dictionary<DateTime, int>(
                    (await dbConnection.QueryAsync<SaleByDateDto>(
                    $"SELECT * FROM common.get_sales_by_days('{minDate.ToString("yyyy-MM-dd")}', '{maxDate.ToString("yyyy-MM-dd")}')", null,
                    commandType: CommandType.Text))
                        .Select(x =>
                    new KeyValuePair<DateTime, int>(x.sale_dt, x.sold_count)));
            }
        }

        public async Task<IDictionary<int, int>> GetSalesByTypesAsync(DateTime minDate, DateTime maxDate, CancellationToken cancellationToken)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("startDate", minDate);
            queryParameters.Add("finishDate", maxDate);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return new Dictionary<int, int>(
                    (await dbConnection.QueryAsync<SaleByTypeDto>(
                        $"SELECT * FROM common.get_sales_by_types('{minDate.ToString("yyyy-MM-dd")}', '{maxDate.ToString("yyyy-MM-dd")}')", null,
                        commandType: CommandType.Text))
                    .Select(x =>
                        new KeyValuePair<int, int>(x.book_type, x.sold_count)));
            }
        }
    }
}
