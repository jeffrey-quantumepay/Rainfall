using FluentValidation.Results;
using Error = Rainfall.SharedLibrary.Models.Error;


namespace Rainfall.SharedLibrary.Exceptions
{
    public class RainfallRecordNotFoundException : RainfallException
    {

        public RainfallRecordNotFoundException()
            : base("One or more validation failures have occurred.")
        {
            base.Errors.Add(new Error(Code.RecordNotFound));
        }

    }
}
