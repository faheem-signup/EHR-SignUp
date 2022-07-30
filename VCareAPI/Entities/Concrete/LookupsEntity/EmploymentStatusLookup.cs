﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class EmploymentStatusLookup : IEntity
    {
        [Key]
        public int EmploymentStatusLookupId { get; set; }
        public string Description { get; set; }
    }
}
