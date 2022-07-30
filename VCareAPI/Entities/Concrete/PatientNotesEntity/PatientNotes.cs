using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.PatientNotesEntity
{
  public class PatientNotes : IEntity
    {
        [Key]
        public int PatientNotesId { get; set; }
        public int? PatientId { get; set; }
        public string NotesDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDemographic { get; set; }
        public bool? IsAdditionalInfo { get; set; }
    }
}
