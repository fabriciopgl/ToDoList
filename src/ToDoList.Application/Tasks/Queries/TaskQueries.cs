using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoList.Application.Tasks.Domain;

namespace ToDoList.Application.Tasks.Queries;

public class TaskQueries(IConfiguration configuration) : ITaskQueries
{
    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("Tasks"));

        const string sql = @"SELECT *
                               FROM Tasks
                              ORDER BY Id
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var command = new CommandDefinition(sql, new { Offset = (page - 1) * pageSize, PageSize = pageSize }, cancellationToken: cancellationToken);
        return await connection.QueryAsync<TaskItem>(command);
    }

}
