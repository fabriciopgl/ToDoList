using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Todos.Models.InputModel;
using TodoList.WebApi.Extensions;
using ToDoList.Application.Todos.Commands;
using ToDoList.Application.Todos.Queries;

namespace ToDoList.WebApi.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]

public class TodosController(ITodoQueries todoQueries, IMediator mediator) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllPaginated([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        if (pageSize > 10)
            return BadRequest("The maximum page length allowed is 10.");

        var tasks = await todoQueries.GetByUserIdAsync(User.GetCurrentUserId(), page, pageSize, cancellationToken);

        if (!tasks.Any())
            return NotFound();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var todo = await todoQueries.GetByIdAndUserIdAsync(id, User.GetCurrentUserId(), cancellationToken);

        if (todo is null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTodoCommand(request.Title, request.Description, request.DueDate, request.Status, User.GetCurrentUserId());

        var result = await mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value, cancellationToken },
            result.Value);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTodoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTodoCommand(id, request.Title, request.Description, request.Status, User.GetCurrentUserId());

        var taskResult = await mediator.Send(command, cancellationToken);
        if (taskResult.IsFailure)
            return UnprocessableEntity(taskResult.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var todo = await todoQueries.GetByIdAndUserIdAsync(id, User.GetCurrentUserId(), cancellationToken);
        if (todo is null)
            return NotFound();

        var command = new DeleteTodoCommand(id);
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}