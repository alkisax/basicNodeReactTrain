namespace dotnet_ai_quote_generator;

public static class QuoteEndpoints
{
  public static void MapQuoteEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/quotes");

    group.MapPost("/", (AiQuoteDto dto, QuoteController controller) => controller.Create(dto));

    group.MapGet("/", (QuoteController controller) => controller.ReadAll());

    group.MapGet("/{id}", (int id, QuoteController controller) => controller.ReadById(id));

    group.MapDelete("/{id}", (int id, QuoteController controller) => controller.DeleteById(id));

    group.MapDelete("/", (QuoteController controller) => controller.DeleteAll());
  }
}