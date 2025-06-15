using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoList.Application.Todos.Domain;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Queries;

public class TodoQueries(IConfiguration configuration, ITodoDbContext dbContext) : ITodoQueries
{
    public async Task<Todo?> GetById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Todo?> GetByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        return await dbContext.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<Todo>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("Todo"));

        const string sql = @"SELECT *
                               FROM Todos
                              ORDER BY Id
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var command = new CommandDefinition(sql, new { Offset = (page - 1) * pageSize, PageSize = pageSize }, cancellationToken: cancellationToken);
        return await connection.QueryAsync<Todo>(command);
    }

    public async Task<IEnumerable<Todo>> GetByUserIdAsync(int userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("Todo"));

        const string sql = @"SELECT *
                               FROM Todos
                              WHERE UserId = @UserId
                              ORDER BY DueDate ASC, Id DESC
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var command = new CommandDefinition(sql,
            new { UserId = userId, Offset = (page - 1) * pageSize, PageSize = pageSize },
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<Todo>(command);
    }
}