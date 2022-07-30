using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.TblPageDto
{
    public class TblPageDto
    {
        public int PageId { get; set; }
        public int SubPageId { get; set; }
        public string PageName { get; set; }
        public string SubpageName { get; set; }
    }
}
