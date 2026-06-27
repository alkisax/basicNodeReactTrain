// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Endpoints\AIQuoteEndpoints.cs
namespace dotnet_ai_quote_generator
{
  public static class AiQuoteEndpoints
  {
    public static void MapAiQuoteEndpoints(this WebApplication app)
    {
      var group = app.MapGroup("/generate-ai-quote");

      group.MapGet("/deepseek", (AiController controller) => controller.TestDeepSeek());
      group.MapGet("/deepseek-call", (AiController controller) => controller.TestDeepSeekCall());
    }
  }
}
