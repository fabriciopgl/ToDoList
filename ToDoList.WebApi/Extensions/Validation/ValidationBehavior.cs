using FluentValidation;
using MediatR;

namespace ToDoList.WebApi.Extensions.Validation;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);
        return next();
    }
}
