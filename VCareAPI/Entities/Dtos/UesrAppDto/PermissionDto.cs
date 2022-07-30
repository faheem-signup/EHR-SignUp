using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UesrAppDto
{
    public class PermissionDto
    {
        public int? PageId { get; set; }
        public string PageName { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanSearch { get; set; }
        public bool? CanDelete { get; set; }
        public int? SubPageId { get; set; }
        public string SubpageName { get; set; }
    }
}
