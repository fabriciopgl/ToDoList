using CSharpFunctionalExtensions;
using MediatR;
using TodoList.Application.Users.Commands;
using TodoList.Application.Users.Domain;

namespace TodoList.Application.Users.Handlers;

public class DeleteUserHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result.Failure("Cannot find User to delete");

        await userRepository.Delete(user, cancellationToken);

        return Result.Success();
    }
}
