// backend-csharp\Dtos\CreateUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace backend_csharp.Dtos;

public record class CreateUserDto(
  int Id,
  [Required]
  string Username,
  string? Name,
  [EmailAddress]
  string? Email,
  string Role,
  [Required]
  [MinLength(6)]
  string Password,
  string? CreatedAt,
  string? UpdatedAt
);
