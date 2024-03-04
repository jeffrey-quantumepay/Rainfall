using FluentValidation;
using Rainfall.SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Application.Queries
{
    public class GetReadingByStationIdCommandValidator : AbstractValidator<GetReadingByStationIdCommand>
    {
        public GetReadingByStationIdCommandValidator()
        {
            RuleFor(v => v.StationId)
                 .NotNull()
                .WithErrorCode(Code.RequiredField.ToInt32String());

            RuleFor(v => v.Count)
                .InclusiveBetween(1, 100)
                .WithErrorCode(Code.InvalidValue.ToInt32String());
        }
    }
}
