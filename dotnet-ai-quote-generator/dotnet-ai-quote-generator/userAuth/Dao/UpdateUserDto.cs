// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Dtos\UpdateUserDto.cs
namespace dotnet_ai_quote_generator;

// DTO για update → όλα optional 
public record UpdateUserDto(
  string? Username,
  string? Name,
  string? Email,
  string? Password,
  string? Role
);