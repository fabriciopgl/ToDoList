using CSharpFunctionalExtensions;
using MediatR;
using TodoList.Application.Users.Commands;
using TodoList.Application.Users.Domain;

namespace TodoList.Application.Users.Handlers;

public class UpdateUserHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result.Failure("User not found");

        user.Update(request.Name, request.Email);
        await userRepository.Update(cancellationToken);

        return Result.Success();
    }
}
