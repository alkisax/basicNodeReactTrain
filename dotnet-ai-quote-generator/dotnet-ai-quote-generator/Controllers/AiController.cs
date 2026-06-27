// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Controllers\AiController.cs

using System.ClientModel;
using System.Text.Json;
using OpenAI;

namespace dotnet_ai_quote_generator;

public class AiController
{
  // φερνουμε το config απο appsettings
  private readonly IConfiguration _config;
  private readonly QuoteDao _quoteDao;
  public AiController(IConfiguration config, QuoteDao quoteDao)
  {
    _config = config;
    _quoteDao = quoteDao;
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
        """
        Return one famous quote as valid JSON only.

        Choose a different historical person each time.
        Avoid Albert Einstein.
        Prefer philosophy, literature, politics, science, or art.

        The JSON must have exactly this shape:
        {
          "person": "string",
          "quote": "string",
          "year": 0
        }

        Do not include markdown.
        Do not include explanation.
        Do not wrap it in ```json.
        """
      );

      var jsonText = response.Value.Content[0].Text;

      // το ai επιστρέφει με lowercase ενώ εμείς στο Dto έχουμε pascal. χρειαζομαστε deserialize
      var quote = JsonSerializer.Deserialize<AiQuoteDto>(
        jsonText,
        new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true
        }
      );

      if (quote is null)
      {
        return Results.BadRequest(new
        {
          status = false,
          message = "AI did not return valid quote JSON"
        });
      }

      var savedQuote = await _quoteDao.Create(quote);

      return Results.Ok(new
      {
        status = true,
        data = savedQuote
      });
    }
    catch (Exception error)
    {
      Console.WriteLine(error);
      return Results.Problem(error.Message);
    }
  }
}

