using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Common
{
    public class Address
    {
        [MaxLength(200)]
        public string Street { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(10)]
        public string Zip { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        public AddressType? AddressType { get; set; }
    }
}
