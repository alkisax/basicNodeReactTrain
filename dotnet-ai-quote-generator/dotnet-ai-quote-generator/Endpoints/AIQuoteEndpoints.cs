namespace dotnetAiQuoteGenerator
{
  public static class AiQuoteEndpoints
  {
    public static void MapAiQuoteEndpoints(this WebApplication app)
    {
      var group = app.MapGroup("/generate-ai-quote");

      group.MapGet("/deepseek", () => {});
    }
  }
}
