using System;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ai_quote_generator;

public static class DataExtensions
{
  public static void MigrateDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
  }

}
