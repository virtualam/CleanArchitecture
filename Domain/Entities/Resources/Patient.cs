using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Resources
{
    public class Patient
    {
        [MaxLength(100)]
        [Required]
        public string PatientId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
    }
}
