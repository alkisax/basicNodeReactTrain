// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Dtos\UserSummaryDto.cs
namespace dotnet_ai_quote_generator;

public record UserSummaryDto(
  int Id,
  string Username,
  string? Name,
  string? Email,
  string Role,
  DateTime? CreatedAt,
  DateTime? UpdatedAt
);