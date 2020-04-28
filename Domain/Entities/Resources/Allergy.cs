using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Resources
{
    public class Allergy
    {
        [Required]
        public string PatientId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public DateTime When { get; set; }
    }
}
