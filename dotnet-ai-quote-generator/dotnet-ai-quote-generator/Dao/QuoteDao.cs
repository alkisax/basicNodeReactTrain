// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Dao\QuoteDao.cs
using Microsoft.EntityFrameworkCore;

namespace dotnet_ai_quote_generator;

public class QuoteDao
{
  private readonly QuoteContext _db;
  public QuoteDao(QuoteContext db)
  {
    _db = db;
  }

  // create
  public async Task<AiQuoteModel> Create(AiQuoteDto dto)
  {
    try
    {
      AiQuoteModel quote = new()
      {
        Person = dto.Person,
        Quote = dto.Quote,
        Year = dto.Year
      };

      _db.Quotes.Add(quote);
      await _db.SaveChangesAsync();
      return quote;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: failed to create quote: {error.Message}");
    }
  }

  //Read all
  public async Task<List<AiQuoteModel>> ReadAll()
  {
    try
    {
      var quotes = await _db.Quotes
        .AsNoTracking()
        .OrderByDescending(q => q.Id)
        .ToListAsync();
      return quotes;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: failed to read quotes: {error.Message}");
    }
  }

  // READ ONE
  public async Task<AiQuoteModel?> ReadById(int id)
  {
    try
    {
      return await _db.Quotes.FindAsync(id);
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: failed to read quote: {error.Message}");
    }
  }

  // DELETE
  public async Task<AiQuoteModel?> DeleteById(int id)
  {
    try
    {
      var quote = await _db.Quotes.FindAsync(id);

      if (quote is null)
      {
        return null;
      }

      _db.Quotes.Remove(quote);
      await _db.SaveChangesAsync();

      return quote;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: failed to delete quote: {error.Message}");
    }
  }

  // DELETE ALL: σβήνει όλα τα quotes από τη βάση
  public async Task<int> DeleteAll()
  {
    try
    {
      var quotes = await _db.Quotes.ToListAsync();
      _db.Quotes.RemoveRange(quotes);
      await _db.SaveChangesAsync();
      return quotes.Count;
    }
    catch (Exception error)
    {
      throw new Exception($"DAO: failed to delete all quotes: {error.Message}");
    }
  }
}
