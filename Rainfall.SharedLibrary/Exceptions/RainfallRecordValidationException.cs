using FluentValidation.Results;
using Error = Rainfall.SharedLibrary.Models.Error;

namespace Rainfall.SharedLibrary.Exceptions
{
    public class RainfallRecordValidationException : RainfallException
    {
        public RainfallRecordValidationException()
            : base("One or more validation failures have occurred.")
        {

        }

        public RainfallRecordValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
                Errors.Add(new Error(failure));
        }
    }

}
