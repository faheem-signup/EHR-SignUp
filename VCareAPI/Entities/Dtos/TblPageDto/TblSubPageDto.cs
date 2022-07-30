using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.TblPageDto
{
    public class TblSubPageDto
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public List<TblPageDto> _tblPageList { get; set; }
    }
}
