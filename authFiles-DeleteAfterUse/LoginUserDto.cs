// backend-csharp\Dtos\LoginUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace backend_csharp.Dtos;

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
