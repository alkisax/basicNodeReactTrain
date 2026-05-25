// backend-dotnet\Models\Todo.cs
using System;
namespace backend_dotnet;

public class Todo
{
  public int Id { get; set; }
  public required string TodoText { get; set; }
  public string? User { get; set; }
  public bool Completed { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
