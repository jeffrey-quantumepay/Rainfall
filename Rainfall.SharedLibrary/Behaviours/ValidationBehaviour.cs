using FluentValidation.Results;
using FluentValidation;
using MediatR;

using Rainfall.SharedLibrary.Exceptions;

namespace Rainfall.SharedLibrary.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                List<ValidationFailure> list = (from f in (await Task.WhenAll(_validators.Select((IValidator<TRequest> v) => v.ValidateAsync(context, cancellationToken)))).SelectMany((ValidationResult r) => r.Errors)
                                                where f != null
                                                select f).ToList();

                if (list.Count != 0)
                {
                    throw new Exceptions.RainfallRecordValidationException(list);
                }
            }

            return await next();
        }
    }
}
