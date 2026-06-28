// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Data\QuoteContext.cs
using Microsoft.EntityFrameworkCore;

namespace dotnet_ai_quote_generator;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<AiQuoteModel> Quotes => Set<AiQuoteModel>();
  public DbSet<User> Users => Set<User>();
}

// για να ξαναδημιουργήσω την db αν την σβήσω
// dotnet ef migrations add InitialCreate --output-dir Data\Migrations