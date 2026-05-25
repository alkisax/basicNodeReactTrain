// backend-dotnet\Data\TodoContext.cs
using System;
using Microsoft.EntityFrameworkCore;

namespace backend_dotnet;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
  public DbSet<Todo> Todos => Set<Todo>();
}
