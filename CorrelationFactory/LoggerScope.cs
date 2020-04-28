using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorrelationFactory
{
    public class LoggerScope : ILoggerScope, IDisposable
    {
        public LoggerScope(ILogger<LoggerScope> logger)
        {
            _logger = logger;
        }

        public ILogger<LoggerScope> _logger { get; }
        IDisposable _disposable;

        public IDisposable BeginScope(string messageFormat, params object[] args)
        {
            _disposable = _logger.BeginScope(messageFormat, args[0]);
            return _disposable;
        }

        public void Dispose()
        {
            if (_disposable != null)
            {
                _disposable.Dispose();
            }
        }
    }
}
