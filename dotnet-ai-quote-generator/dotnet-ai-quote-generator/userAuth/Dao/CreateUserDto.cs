// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Dtos\CreateUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace dotnet_ai_quote_generator;

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
  DateTime? CreatedAt,
  DateTime? UpdatedAt
);
