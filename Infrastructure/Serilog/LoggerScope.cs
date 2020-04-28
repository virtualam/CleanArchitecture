using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Serilog
{
    public class LoggerScope : ILoggerScope, IDisposable
    {
        public LoggerScope(ILogger<LoggerScope> logger)
        {
            _logger = logger;

            

            //var mylogger = new SerilogLoggerProvider().CreateLogger("d");
        }

        public ILogger<LoggerScope> _logger { get; }
        IDisposable _disposable;

        public IDisposable BeginScope(string messageFormat, params object[] args)
        {
            var correlationId = args[0];
            _disposable = LogContext.PushProperty("CorrelationId", correlationId, true);
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
