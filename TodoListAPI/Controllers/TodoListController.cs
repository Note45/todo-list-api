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
    public TodoEntity PostTodo([FromBody] CreateTodoCommand command)
    {
        return _todoListService.AddUserTodoAsync((TodoEntity)command);
    }

    [HttpGet]
    [Route("{userId}")]
    public IEnumerable<TodoEntity> GetTodoList(string userId)
    {
        return _todoListService.GetAllUserTodoAsync(userId);
    }
}

