using FluentValidation.Results;
using Rainfall.SharedLibrary.Models;

namespace Rainfall.SharedLibrary.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {

        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var errors = failures.GroupBy(e => e.PropertyName, e => e, (name, failure) => new { Key = name, Validation = failure });

            foreach (var failure in failures)
                Errors.Add(new Error(failure));
        }
    }


}
