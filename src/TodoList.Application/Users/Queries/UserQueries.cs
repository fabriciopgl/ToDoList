using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoList.Application.Users.Domain;

namespace TodoList.Application.Users.Queries;

public class UserQueries(IConfiguration configuration, IUserDbContext dbContext) : IUserQueries
{
    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("Todo"));

        const string sql = @"SELECT Id, Name, Email, CreatedAt, UpdatedAt
                               FROM Users
                              ORDER BY CreatedAt DESC
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var command = new CommandDefinition(sql, new { Offset = (page - 1) * pageSize, PageSize = pageSize }, cancellationToken: cancellationToken);
        return await connection.QueryAsync<User>(command);
    }
}
