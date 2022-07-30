using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.TBLPageEntity
{
  public class TblPage : IEntity
    {
        [Key]
        public int PageId { get; set; }
        public string PageName { get; set; }
    }
}
