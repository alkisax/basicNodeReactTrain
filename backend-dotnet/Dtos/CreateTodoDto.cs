using System.ComponentModel.DataAnnotations;

namespace backend_dotnet;

public record class CreateTodoDto
(
  [MinLength(1)][MaxLength(300)]
  string Todo,
  [MaxLength(100)]
  string? User = "",
  bool Completed = false
);
