using FluentValidation.Results;
using Error = Rainfall.SharedLibrary.Models.Error;


namespace Rainfall.SharedLibrary.Exceptions
{
    public class RainfallNotFoundException : RainfallException
    {

        public RainfallNotFoundException(string message)
            : base(message)
        {
            base.Errors.Add(new Error(Code.RecordNotFound));
        }

    }
}
