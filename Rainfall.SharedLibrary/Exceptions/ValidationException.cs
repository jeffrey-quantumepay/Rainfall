using FluentValidation.Results;
using Error = Rainfall.SharedLibrary.Models.Error;

namespace Rainfall.SharedLibrary.Exceptions
{
    public class RainfallValidationException : RainfallException
    {
        public RainfallValidationException()
            : base("One or more validation failures have occurred.")
        {

        }

        public RainfallValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
                Errors.Add(new Error(failure));
        }
    }

}
