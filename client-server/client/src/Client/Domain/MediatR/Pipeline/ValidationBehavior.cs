using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.MediatR.Pipeline
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        where TResponse : new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            var validationResults = await Task.WhenAll(
                _validators.Select(v => Task.Factory.StartNew(() =>
                {
                    var context = new ValidationContext<TRequest>(request);
                    return v.Validate(context);
                }, cancellationToken)
                ));

            var validationFailuresErrors = validationResults
                .SelectMany(e => e.Errors)
                .Where(f => f != null && f.Severity == Severity.Error)
                .Distinct()
                .ToList();

            var validationFailuresWarningOrInfo = validationResults
                .SelectMany(e => e.Errors)
                .Where(f => f != null && f.Severity != Severity.Error)
                .Distinct()
                .ToList();


            if (validationFailuresErrors.Count > 0)
            {
                throw new ValidationException(validationFailuresErrors);
            }

            if (validationFailuresWarningOrInfo.Count > 0)
            {
                return new TResponse();
            }

            return await next();
        }
    }
}
