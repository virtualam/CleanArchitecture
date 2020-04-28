using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IErrorModel
    {
        string PropertyName { get; set; }
        IEnumerable<string> Failures { get; set; }
    }
}
