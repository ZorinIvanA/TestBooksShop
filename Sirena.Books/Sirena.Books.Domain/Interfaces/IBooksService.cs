using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sirena.Books.Domain.Entities;

namespace Sirena.Books.Domain.Interfaces
{
    /// <summary>
    /// Сервис для работы со списком книг
    /// </summary>
    public interface IBooksService
    {
        /// <summary>
        /// Возвращает список книг, отфильтрованных по параметрам. Каждый параметр может быть пустым.
        /// </summary>
        /// <param name="isExists">Существует ли книга на складе</param>
        /// <param name="types">Список типов книг</param>
        /// <param name="minCost">Минимальная стоимость книг</param>
        /// <param name="maxCost">Максимальная стоимость книг</param>
        /// <param name="Author">Автор. Возвращаются книги, чьё имя автора начинается с этих букв.</param>
        /// <param name="name">Имя. Возвращаются книги, чьё название начинается с этих букв.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<Book[]> GetByParamsAsync(bool? isExists, int[] types, 
            decimal? minCost, decimal? maxCost,string Author, string name, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Купить книгу
        /// </summary>
        /// <param name="id">id книги, которую надо купить</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task BuyAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Добавить книгу в номенклатуру
        /// </summary>
        /// <param name="book">Сведения о книге</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task AddAsync(Book book, CancellationToken cancellationToken);
    }
}
