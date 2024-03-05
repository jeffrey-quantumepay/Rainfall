using FluentValidation.Results;
using Error = Rainfall.SharedLibrary.Models.Error;

namespace Rainfall.SharedLibrary.Exceptions
{
    public class RainfallValidationException : RainfallException
    {
        public RainfallValidationException(string message)
            : base(message)
        {

        }

        public RainfallValidationException(IEnumerable<ValidationFailure> failures)
            : this("")
        {
            foreach (var failure in failures)
                Errors.Add(new Error(failure));
        }
    }

}
