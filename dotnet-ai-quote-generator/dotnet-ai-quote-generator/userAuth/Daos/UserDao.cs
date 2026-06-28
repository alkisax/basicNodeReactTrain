// dotnet-ai-quote-generator\dotnet-ai-quote-generator\Dao\UserDao.cs

using Microsoft.EntityFrameworkCore;

namespace dotnet_ai_quote_generator;

public class UserDao
{
  // φτιάχνω μια μεταβλητή που μέσα της θα βάλω την λειτουργικότητα της db. Στο ονομα βάζω _ γιατι ...
  private readonly AppDbContext _db;

  public UserDao(AppDbContext db)
  {
    _db = db;
  }

  // GET ALL
  public async Task<List<User>> GetAll()
  {
    var users = await _db.Users
      .AsNoTracking() // κανονικά τα βάζει στην μνήμη και περιμένει αλλαγές. εδώ θέλουμε απλώς να τα δούμε
      .ToListAsync();

    return users;
  }

  // GET BY ID
  public async Task<User?> GetById(int id)
  {
    var user = await _db.Users.FindAsync(id);
    return user is null ? null : user;
  }

  // GET BY USERNAME
  public async Task<User?> GetByUsername(string username)
  {
    var user = await _db.Users
      .FirstOrDefaultAsync(u => u.Username == username);

    return user is null ? null : user;
  }

  // GET BY EMAIL
  public async Task<User?> GetByEmail(string email)
  {
    var user = await _db.Users
      .FirstOrDefaultAsync(user => user.Email == email);

    return user is null ? null : user;
  }

  // CREATE
  public async Task<User> Create(User user)
  {
    _db.Users.Add(user);
    await _db.SaveChangesAsync();
    return user;
  }

  // UPDATE
  public async Task<User?> Update(int id, User updatedData)
  {
    var user = await _db.Users.FindAsync(id);
    if (user is null) return null;

    user.Username = updatedData.Username ?? user.Username;
    user.Name = updatedData.Name ?? user.Name;
    user.Email = updatedData.Email ?? user.Email;
    user.Role = updatedData.Role ?? user.Role;
    user.HashedPassword = updatedData.HashedPassword ?? user.HashedPassword;

    await _db.SaveChangesAsync();
    return user;
  }

  // DELETE
  public async Task<User?> Delete(int id)
  {
    var user = await _db.Users.FindAsync(id);
    if (user is null) return null;

    _db.Users.Remove(user);
    await _db.SaveChangesAsync();

    return user;
  }
}