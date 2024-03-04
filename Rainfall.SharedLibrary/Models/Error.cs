using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.SharedLibrary.Models
{
    public class Error
    {
        public Error(ValidationFailure failure)
        {
            var code = (Code)Convert.ToInt32(failure.ErrorCode);
            Code = code.ToSnakeCase();
            message = failure.FormattedMessagePlaceholderValues.Aggregate(code.GetDescription(), (current, value) => current.Replace("{" + value.Key + "}", (value.Value == null ? null : value.Value.ToString())));  // https://stackoverflow.com/questions/1231768/c-sharp-string-replace-with-dictionary
            propertyName = failure.PropertyName;
        }

        public Error(Code code)
        {
            Code = code.ToSnakeCase();
            message = code.GetDescription();
        }


        public string Code { get; private set; }

        public string propertyName { get; private set; }

        public string message { get; private set; }
    }
}
