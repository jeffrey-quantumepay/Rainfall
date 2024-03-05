using FluentValidation.Results;
using Error = Rainfall.SharedLibrary.Models.Error;


namespace Rainfall.SharedLibrary.Exceptions
{

    public class RainfallInternalServerException : RainfallException
    {

        public RainfallInternalServerException(string message)
            : base(message)
        {

        }

    }
}
