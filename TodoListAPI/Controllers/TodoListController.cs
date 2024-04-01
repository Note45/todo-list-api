using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Domain.Command;
using TodoListAPI.Domain.Entities;
using TodoListAPI.Domain.Services;
using TodoListAPI.Infra.Auth;

namespace TodoListAPI.Controllers;

[ApiController]
[Route("api/todolists")]
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
    [Authorize(Roles = UserRoles.User)]
    [Route("")]
    public async Task<TodoEntity> PostTodo([FromBody] CreateTodoCommand command)
    {
        return await _todoListService.AddUserTodoAsync((TodoEntity)command);
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.User)]
    [Route("")]
    public async Task<IEnumerable<TodoEntity>> GetTodoList()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        
        return await _todoListService.GetAllUserTodoAsync(userId);
    }

    [HttpPatch]
    [Authorize(Roles = UserRoles.User)]
    [Route("")]
    public async Task<bool> UpdateTodo([FromBody] UpdateTodoCommand command)
    {
        return await _todoListService.UpdateUserTodoAsync((TodoEntity)command);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoles.User)]
    [Route("{todoId}")]
    public async Task<bool> DeleteTodo(string todoId)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        
        return await _todoListService.RemoveUserTodoByDescriptionAsync(userId, todoId);
    }
}

