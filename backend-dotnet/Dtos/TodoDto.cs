// backend-dotnet\Dtos\TodoDto.cs
namespace backend_dotnet;

public record class TodoDto
(
  int Id,
  string Todo,
  string? User,
  bool Completed,
  DateTime? CreatedAt,
  DateTime? UpdatedAt
);