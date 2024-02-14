using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Domain.Common
{
    public abstract class PaginationHelper
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int StartRow
        {
            get { return (CurrentPage - 1) * PerPage + 1; ; }
        }
        public int EndRow
        {
            get { return StartRow + PerPage - 1; }
        }
    }
}
