using Microsoft.EntityFrameworkCore;
using TodoList.Core.Context;

namespace TodoList.Application.Users.Domain;

public interface IUserDbContext : IDbContext
{
    DbSet<User> Users { get; }
}
