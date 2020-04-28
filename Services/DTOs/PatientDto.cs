using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class PatientDto
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
