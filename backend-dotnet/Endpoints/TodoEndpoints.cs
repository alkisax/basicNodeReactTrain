// backend-dotnet\Endpoints\TodoEndpoints.cs

namespace backend_dotnet;

public static class TodoEndpoints
{
  public static void MapTodoEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/todo");

    // read all
    group.MapGet(
      "/",
      async (TodoController controller) =>
        await controller.ReadAll()
    );

    // read user todos
    group.MapGet(
      "/user-todos/{user}",
      async (string user, TodoController controller) =>
        await controller.ReadUserTodos(user)
    );

    // read todo by id
    group.MapGet(
      "/{id}",
      async (int id, TodoController controller) =>
        await controller.ReadTodoById(id)
    );

    // create
    group.MapPost(
      "/create-todo",
      async (CreateTodoDto dto, TodoController controller) =>
        await controller.Create(dto)
    );

    // update
    group.MapPost(
      "/update-todo/{id}",
      async (
        int id,
        UpdateTodoDto dto,
        TodoController controller
      ) =>
        await controller.UpdateTodoById(id, dto)
    );

    // delete todo by id
    group.MapPost(
      "/delete-todo/{id}",
      async (int id, TodoController controller) =>
        await controller.DeleteTodoById(id)
    );

    // delete all user todos
    group.MapPost(
      "/delete-all-user-todos/{user}",
      async (string user, TodoController controller) =>
        await controller.DeleteAllUserTodos(user)
    );
  }
}