using System;

namespace dotnet_ai_quote_generator;

public class AiQuoteModel
{
  public int Id { get; set; }
  public required string Person { get; set; }
  public required string Quote { get; set; }
  public int Year { get; set; }
  public int? UserId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
