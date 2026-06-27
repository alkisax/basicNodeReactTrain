// backend-csharp\Dtos\UpdateUserDto.cs
namespace backend_csharp.Dtos;

// DTO για update → όλα optional 
public record UpdateUserDto(
  string? Username,
  string? Name,
  string? Email,
  string? Password,
  string? Role
);