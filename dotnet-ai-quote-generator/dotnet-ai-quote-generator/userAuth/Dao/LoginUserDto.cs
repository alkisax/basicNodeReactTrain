// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Dtos\LoginUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace dotnet_ai_quote_generator;

public record class LoginUserDto
(
  [Required]
  [MinLength(3)]
  [MaxLength(50)]
  string Username,

  [Required]
  [MinLength(6)]
  [MaxLength(128)]
  string Password
);
