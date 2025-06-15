using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Todos.Commands;
using ToDoList.Application.Todos.Queries;

namespace ToDoList.WebApi.Controllers;

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

        var tasks = await todoQueries.GetAllAsync(page, pageSize, cancellationToken);
        if (!tasks.Any())
            return NotFound();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var todo = await todoQueries.GetById(id, cancellationToken);
        if (todo is null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetById), 
            new { id = result.Value, cancellationToken },
            result.Value);
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var taskResult = await mediator.Send(command, cancellationToken);
        if (taskResult.IsFailure)
            return UnprocessableEntity(taskResult.Error);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}