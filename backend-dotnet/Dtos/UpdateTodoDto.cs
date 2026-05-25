using System.ComponentModel.DataAnnotations;

namespace backend_dotnet;

public record class UpdateTodoDto
(
  [MinLength(1)][MaxLength(300)]
  string? Todo,
  bool? Completed,
  [MaxLength(100)]
  string? User = ""
);
