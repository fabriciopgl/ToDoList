using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Users.Domain;

namespace TodoList.Infraestructure.Repositories;

public class UserRepository(IUserDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User> Add(User user, CancellationToken cancellationToken)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task Update(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result> Delete(User user, CancellationToken cancellationToken)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }
}
