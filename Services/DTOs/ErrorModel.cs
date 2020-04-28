using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class ErrorModel : IErrorModel
    {
        public string PropertyName { get; set; }
        public IEnumerable<string> Failures { get; set; }
    }
}
