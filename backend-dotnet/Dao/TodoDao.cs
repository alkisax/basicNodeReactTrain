// backend-dotnet\Dao\TodoDao.cs
using System;
using Microsoft.EntityFrameworkCore;
namespace backend_dotnet;

public class TodoDao
{
  private readonly TodoContext _db;
  public TodoDao(TodoContext db)
  {
    _db = db;
  }
  private static Todo Map(Todo todo) => todo;

  // create
  public async Task<Todo> Create(CreateTodoDto todo)
  {
    try
    {
      Todo newTodo = new()
      {
        TodoText = todo.Todo,
        User = todo.User,
        Completed = todo.Completed,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };

      _db.Todos.Add(newTodo);
      await _db.SaveChangesAsync();
      return newTodo;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: error creating todo, {error.Message}");
    }
  }

  // read all
  public async Task<List<Todo>> ReadAll()
  {
    try
    {
      var todos = await _db.Todos
        .AsNoTracking()
        .ToListAsync();

      return todos;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: Failed to fetch todos, {error.Message}");
    }
  }

  // read one todo
  public async Task<Todo?> ReadTodoById(int id)
  {
    try
    {
      var todo = await _db.Todos.FindAsync(id);
      if (todo is null)
      {
        Console.WriteLine("todo not found");
        return null;
      }
      return todo;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: Failed to fetch todo, {error.Message}");
    }
  }

  // read user todo
  public async Task<List<Todo>> ReadUserTodos(string user)
  {
    try
    {
      var userTodos = await _db.Todos
        .Where(todo => todo.User == user)
        .ToListAsync();
      return userTodos;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: Failed to fetch user todos, {error.Message}");
    }
  }

  // update
  public async Task<Todo?> UpdateTodoById(int id, UpdateTodoDto updatedData)
  {
    try
    {
      var todo = await _db.Todos.FindAsync(id);
      if (todo is null)
      {
        Console.WriteLine($"'DAO: todo was not found to be updated'");
        return null;
      }

      todo.TodoText = updatedData.Todo ?? todo.TodoText;
      todo.User = updatedData.User ?? todo.User;
      todo.UpdatedAt = DateTime.UtcNow;
      todo.Completed = updatedData.Completed ?? todo.Completed;

      await _db.SaveChangesAsync();
      return todo;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: Failed to update todo, {error.Message}");
    }
  }

  // delete
  public async Task<Todo> DeleteTodoById(int id)
  {
    try
    {
      var deletedTodo = await _db.Todos.FindAsync(id);
      if (deletedTodo == null)
      {
        throw new Exception("DAO: todo was not found to be deleted");
      }
      _db.Todos.Remove(deletedTodo);
      await _db.SaveChangesAsync();
      return deletedTodo;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: Failed to delete todo, {error.Message}");
    }
  }

  // delete all user todos
  public async Task<bool> DeleteAllUserTodos(string user)
  {
    try
    {
      var todoListToBeDeleted = await _db.Todos
        .Where(todo => todo.User == user)
        .ToListAsync();

      if (todoListToBeDeleted.Count == 0)
      {
        Console.WriteLine($"user has no todos to be deleted");
        return false;
      }

      foreach (var todo in todoListToBeDeleted)
      {
        _db.Todos.Remove(todo);
      }
      await _db.SaveChangesAsync();
      return true;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: Failed to delete user todos, {error.Message}");
    }
  }
}
