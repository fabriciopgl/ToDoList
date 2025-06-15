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
}
