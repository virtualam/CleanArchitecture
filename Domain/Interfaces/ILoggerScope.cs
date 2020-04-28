using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ILoggerScope
    {
        IDisposable BeginScope(string messageFormat, params object[] args);
    }
}
