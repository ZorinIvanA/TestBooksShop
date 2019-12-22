using System;
using System.Collections.Generic;
using System.Text;

namespace Sirena.Books.Domain.Enums
{
    /// <summary>
    /// Тип книгт
    /// </summary>
    public enum BookType
    {
        /// <summary>
        /// Научная литература
        /// </summary>
        Science = 1,
        /// <summary>
        /// Научно-популярная литература
        /// </summary>
        SciencePopular = 2,
        /// <summary>
        /// Словари
        /// </summary>
        Dictionary = 3,
        /// <summary>
        /// Энциклопедии
        /// </summary>
        Encyclopedia = 4
    }
}
