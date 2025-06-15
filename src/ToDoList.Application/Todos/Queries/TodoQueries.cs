using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoList.Application.Todos.Domain;

namespace ToDoList.Application.Todos.Queries;

public class TodoQueries(IConfiguration configuration) : ITodoQueries
{
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
