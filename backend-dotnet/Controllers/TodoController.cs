// backend-dotnet\Controllers\TodoController.cs
namespace backend_dotnet;

public class TodoController
{
  private readonly TodoDao _dao;

  public TodoController(TodoDao dao)
  {
    _dao = dao;
  }

  public async Task<IResult> Create(CreateTodoDto dto)
  {
    try
    {
      var created = await _dao.Create(dto);
      var data = new TodoDto(
       created.Id,
       created.TodoText,
       created.User,
       created.Completed,
       created.CreatedAt,
       created.UpdatedAt
      );
      return Results.Created($"/todo/{created.Id}", new { status = true, data });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }

  // read all
  public async Task<IResult> ReadAll()
  {
    try
    {
      var todos = await _dao.ReadAll();

      var data = todos.Select(todo => new TodoDto(
        todo.Id,
        todo.TodoText,
        todo.User,
        todo.Completed,
        todo.CreatedAt,
        todo.UpdatedAt
      )).ToList();

      return Results.Ok(new
      {
        status = true,
        data
      });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }

  // readTodoById
  public async Task<IResult> ReadTodoById(int id)
  {
    try
    {
      var todo = await _dao.ReadTodoById(id);
      if (todo is null) return Results.NotFound(new { status = false, message = "todo not found" });

      var data = new TodoDto(
        todo.Id,
        todo.TodoText,
        todo.User,
        todo.Completed,
        todo.CreatedAt,
        todo.UpdatedAt
      );
      return Results.Ok(new { status = true, data = data });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }

  // readUserTodos
  public async Task<IResult> ReadUserTodos(string user)
  {
    try
    {
      var todos = await _dao.ReadUserTodos(user);
      // χρησιμοποιησα anonymous obj για να mimic το mongodb front που ζήταγε _id αντι για Id και todo αντι για Todo
      var data = todos.Select(todo => new
      {
        _id = todo.Id,
        todo = todo.TodoText,
        user = todo.User,
        completed = todo.Completed,
        createdAt = todo.CreatedAt,
        updatedAt = todo.UpdatedAt
      }).ToList();
      return Results.Ok(new { status = true, data });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }

  // updateTodoById
  public async Task<IResult> UpdateTodoById(int id, UpdateTodoDto updatedData)
  {
    try
    {
      var todo = await _dao.UpdateTodoById(id, updatedData);
      if (todo is null) return Results.NotFound(new { status = false, message = "todo not found" });
      // χρησιμοποιησα anonymous obj για να mimic το mongodb front που ζήταγε _id αντι για Id και todo αντι για Todo
      var data = new
      {
        _id = todo.Id,
        todo = todo.TodoText,
        user = todo.User,
        completed = todo.Completed,
        createdAt = todo.CreatedAt,
        updatedAt = todo.UpdatedAt
      };
      return Results.Ok(new { status = true, data });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }

  // deleteTodoById
  public async Task<IResult> DeleteTodoById(int id)
  {
    try
    {
      var todo = await _dao.DeleteTodoById(id);
      if (todo is null) return Results.NotFound(new { status = false, message = "todo not found" });
      var data = new TodoDto(
        todo.Id,
        todo.TodoText,
        todo.User,
        todo.Completed,
        todo.CreatedAt,
        todo.UpdatedAt
      );
      return Results.Ok(new { status = true, data });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }

  // deleteAllUserTodos
  public async Task<IResult> DeleteAllUserTodos(string user)
  {
    try
    {
      var todos = await _dao.DeleteAllUserTodos(user);

      return Results.Ok(new { status = true });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Results.Problem(detail: ex.Message, statusCode: 500);
    }
  }
}
