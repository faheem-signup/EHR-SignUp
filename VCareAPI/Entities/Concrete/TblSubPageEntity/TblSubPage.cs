using Core.Entities;
using Entities.Concrete.TBLPageEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.TblSubPageEntity
{
    public class TblSubPage : IEntity
    {
        [Key]
        public int SubPageId { get; set; }
        public int? PageId { get; set; }
        public string SubpageName { get; set; }
        [ForeignKey("PageId")]
        public virtual TblPage _TblPage { get; set; }
    }
}
