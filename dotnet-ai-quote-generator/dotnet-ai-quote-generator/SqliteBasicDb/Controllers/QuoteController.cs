namespace dotnet_ai_quote_generator;

public class QuoteController
{
  // DAO = database access layer
  private readonly QuoteDao _dao;
  private readonly UserDao _userDao;

  // Το ASP.NET δίνει το QuoteDao αυτόματα με DI
  public QuoteController(QuoteDao dao, UserDao userDao)
  {
    _dao = dao;
    _userDao = userDao;
  }

  // POST /quotes
  public async Task<IResult> Create(AiQuoteDto dto)
  {
    try
    {
      var quote = await _dao.Create(dto);

      return Results.Created($"/quotes/{quote.Id}", new
      {
        status = true,
        data = quote
      });
    }
    catch (Exception error)
    {
      Console.WriteLine(error);
      return Results.Problem(error.Message);
    }
  }

  // GET /quotes
  public async Task<IResult> ReadAll()
  {
    try
    {
      var quotes = await _dao.ReadAll();

      var data = quotes.Select(q => new
      {
        q.Id,
        q.Person,
        q.Quote,
        q.Year,
        q.UserId,
        Username = q.User?.Username,
        q.CreatedAt
      });

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

  // GET /quotes/{id}
  public async Task<IResult> ReadById(int id)
  {
    try
    {
      var q = await _dao.ReadById(id);

      if (q is null)
      {
        return Results.NotFound(new
        {
          status = false,
          message = "quote not found"
        });
      }

      var data = new
      {
        q.Id,
        q.Person,
        q.Quote,
        q.Year,
        q.UserId,
        Username = q.User?.Username,
        q.CreatedAt
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

  // DELETE /quotes/{id}
  public async Task<IResult> DeleteById(int id)
  {
    try
    {
      var quote = await _dao.DeleteById(id);

      if (quote is null)
      {
        return Results.NotFound(new
        {
          status = false,
          message = "quote not found"
        });
      }

      return Results.Ok(new
      {
        status = true,
        data = quote
      });
    }
    catch (Exception error)
    {
      Console.WriteLine(error);
      return Results.Problem(error.Message);
    }
  }

  // DELETE /quotes
  public async Task<IResult> DeleteAll()
  {
    try
    {
      var deletedCount = await _dao.DeleteAll();

      return Results.Ok(new
      {
        status = true,
        deletedCount
      });
    }
    catch (Exception error)
    {
      Console.WriteLine(error);
      return Results.Problem(error.Message);
    }
  }
}