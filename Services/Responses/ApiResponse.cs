using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Services.Responses
{
    public class ApiResponse
    {
        private readonly IDictionary<string, string[]> _failures = new Dictionary<string, string[]>();

        public IDictionary<string, string[]> Errors { get; }
        public object Result { get; }

        public bool HasData { get; }

        public ApiResponse() => Errors = _failures;

        public ApiResponse(IEnumerable<ValidationFailure> failures) : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                _failures.Add(propertyName, propertyFailures);
            }
        }

        public ApiResponse(object result, bool hasData) : this()
        {
            Result = result;
            HasData = hasData;
        }
    }
}
