using System;

namespace dotnet_ai_quote_generator;

public class LogFromFrontController
{
  public IResult Log(LogFromFrontDto dto)
  {
    System.Console.WriteLine($"Frontend logger: {dto.Data}");
    return Results.Ok(new
    {
      status = true
    });
  }
}
