using System;
using System.Collections.Generic;
using System.Text;

namespace Sirena.Books.Infrastructure.Dto
{
    /// <summary>
    /// Единица статистики продаж по типу
    /// </summary>
    public struct SaleByTypeDto
    {
        public int book_type { get; set; }
        public int sold_count { get; set; }
    }
}
