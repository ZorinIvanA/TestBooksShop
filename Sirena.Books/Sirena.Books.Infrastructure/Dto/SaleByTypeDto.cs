using System;
using System.Collections.Generic;
using System.Text;

namespace Sirena.Books.Infrastructure.Dto
{
    public struct SaleByTypeDto
    {
        public int book_type { get; set; }
        public int sold_count { get; set; }
    }
}
