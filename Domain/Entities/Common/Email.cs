using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Common
{
    public class Email
    {
        [Required]
        [MaxLength(150)]
        public string Address { get; set; }
    }
}
