// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Data\QuoteContext.cs
using Microsoft.EntityFrameworkCore;

namespace dotnet_ai_quote_generator;

public class QuoteContext(DbContextOptions<QuoteContext> options) : DbContext(options)
{
  public DbSet<AiQuoteModel> Quotes => Set<AiQuoteModel>();
}
