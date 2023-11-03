using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Services;

namespace TodoListAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ILogger<TodoListController> _logger;
    private readonly ITodoListService _todoListService;

    public TodoListController(ILogger<TodoListController> logger, ITodoListService todoListService)
    {
        _logger = logger;
        _todoListService = todoListService;
    }

    [HttpPost]
    [Route("add")]
    public async Task<TodoEntity> PostTodo([FromBody] CreateTodoCommand command)
    {
        return await _todoListService.AddUserTodoAsync((TodoEntity)command);
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IEnumerable<TodoEntity>> GetTodoList(string userId)
    {
        return await _todoListService.GetAllUserTodoAsync(userId);
    }

    [HttpPatch]
    [Route("update")]
    public async Task<bool> UpdateTodo([FromBody] UpdateTodoCommand command)
    {
        return await _todoListService.UpdateUserTodoAsync((TodoEntity)command);
    }

    [HttpDelete]
    [Route("{userId}/{todoId}/delete")]
    public async Task<bool> DeleteTodo(string userId, string todoId)
    {
        return await _todoListService.RemoveUserTodoByDescriptionAsync(userId, todoId);
    }
}

