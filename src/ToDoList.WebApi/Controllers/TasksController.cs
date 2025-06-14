using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Tasks.Commands;
using ToDoList.Application.Tasks.Domain;
using ToDoList.Application.Tasks.Queries;

namespace ToDoList.WebApi.Controllers;

[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]

public class TasksController(ITaskItemRepository taskItemRepository, ITaskQueries taskQueries, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasksAsync([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        if (pageSize > 10)
            return BadRequest("The maximum page length allowed is 10.");

        var tasks = await taskQueries.GetAllTasksAsync(page, pageSize, cancellationToken);
        if (!tasks.Any())
            return NotFound();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(int id, CancellationToken cancellationToken)
    {
        var task = await taskItemRepository.GetTaskByIdAsync(id, cancellationToken);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> PostTask(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var taskResult = await mediator.Send(command, cancellationToken);
        if (taskResult.IsFailure)
            return BadRequest(taskResult.Error);

        return CreatedAtAction(nameof(GetTaskById), new { id = taskResult.Value, cancellationToken }, taskResult.Value);
    }

    [HttpPut]
    public async Task<IActionResult> PutTask(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var taskResult = await mediator.Send(command, cancellationToken);
        if (taskResult.IsFailure)
            return BadRequest(taskResult.Error);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if(result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}