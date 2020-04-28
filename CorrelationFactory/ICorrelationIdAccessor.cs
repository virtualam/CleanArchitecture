using System;
using System.Collections.Generic;
using System.Text;

namespace CorrelationFactory
{
    public interface ICorrelationIdAccessor
    {
        string CorrelationId { get; }
    }
}
