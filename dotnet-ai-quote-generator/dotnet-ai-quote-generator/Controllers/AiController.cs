// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Controllers\AiController.cs

using System.ClientModel;
using OpenAI;

namespace dotnet_ai_quote_generator;

public class AiController
{
  // φερνουμε το config απο appsettings
  private readonly IConfiguration _config;
  public AiController(IConfiguration config)
  {
    _config = config;
  }

  public IResult TestDeepSeek()
  {
    try
    {
      // appsettings
      var key = _config["DeepSeek:ApiKey"];

      // guard
      var data = new
      {
        exists = !string.IsNullOrEmpty(key),
        length = key?.Length
      };

      return Results.Ok(new
      {
        status = true,
        data
      });
    }
    catch (Exception error)
    {
      Console.WriteLine(error);
      return Results.Problem(error.Message);
      throw;
    }
  }


  // test deepseek call
  // task ↔ promise, IResult → http response result
  public async Task<IResult> TestDeepSeekCall()
  {
    try
    {
      // appsettings
      var apiKey = _config["DeepSeek:ApiKey"];
      // ο client του deepseek είναι 1-1 ιδιος με του chatgpt
      var openAiClient = new OpenAIClient(
        new ApiKeyCredential(apiKey),
        new OpenAIClientOptions()
        {
          Endpoint = new Uri("https://api.deepseek.com")
        }
      );

      var client = openAiClient.GetChatClient("deepseek-chat");
      var response = await client.CompleteChatAsync(
        "Say hello in one short sentence."
      );

      var data = new
      {
        reply = response.Value.Content[0].Text
      };

      return Results.Ok(new
      {
        status = true,
        data
      });
    }
    catch (Exception error)
    {
      Console.WriteLine(error);
      return Results.Problem(error.Message);
    }
  }
}

