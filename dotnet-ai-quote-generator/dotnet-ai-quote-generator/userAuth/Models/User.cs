// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Models\User.cs

namespace dotnet_ai_quote_generator;

public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public string? Name { get; set; }
  public string? Email { get; set; }
  public string Role { get; set; } = "USER";
  public required string HashedPassword { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
