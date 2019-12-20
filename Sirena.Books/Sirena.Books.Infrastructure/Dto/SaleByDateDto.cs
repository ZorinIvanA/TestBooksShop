using System;
using System.Collections.Generic;
using System.Text;

namespace Sirena.Books.Infrastructure.Dto
{
    public struct SaleByDateDto
    {
        public DateTime sale_dt { get; set; }
        public int sold_count { get; set; }
    }
}
