using System;
using Core.Entities;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public class Status : IEntity
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
       
    }
}
