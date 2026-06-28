# User Auth Reading Order
το flow του user authentication στο ASP.NET backend.

## 1. `Models/User.cs`
Το βασικό database model του χρήστη.
```csharp
public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public string Role { get; set; } = "USER";
  public required string HashedPassword { get; set; }
}
```
Αυτό είναι το entity που αποθηκεύεται στη SQLite.
---

## 2. DTOs
### `CreateUserDto.cs`
Τι στέλνει ο client όταν κάνει register.
```csharp
public record class CreateUserDto(
  string Username,
  string? Name,
  string? Email,
  string Password
);
```
### `LoginUserDto.cs`
Τι στέλνει ο client όταν κάνει login.
```csharp
public record class LoginUserDto(
  string Username,
  string Password
);
```
### `UserSummaryDto.cs`
Τι επιστρέφουμε προς τα έξω χωρίς password/hash.
```csharp
public record UserSummaryDto(
  int Id,
  string Username,
  string? Email,
  string Role,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
```

### `UpdateUserDto.cs` / `UpdateRoleDto.cs`
Χρησιμοποιούνται για update profile και αλλαγή role.
---

## 3. `Data/AppDbContext.cs`
Δηλώνει τα database tables στο EF Core.
```csharp
public DbSet<User> Users => Set<User>();
public DbSet<AiQuoteModel> Quotes => Set<AiQuoteModel>();
```
Το `DbContext` είναι το αντίστοιχο κέντρο ελέγχου της βάσης.
---

## 4. `Dao/UserDao.cs`
Το DAO κάνει τις άμεσες κινήσεις στη βάση.
Περιλαμβάνει:
- `GetAll()`
- `GetById(int id)`
- `GetByUsername(string username)`
- `GetByEmail(string email)`
- `Create(User user)`
- `Update(int id, User updatedData)`
- `Delete(int id)`
Παράδειγμα:
```csharp
var user = await _db.Users
  .FirstOrDefaultAsync(u => u.Username == username);
```
Αυτό είναι περίπου σαν SQL:
```sql
SELECT * FROM Users WHERE Username = ...
```
---

## 5. `service/AuthService.cs`
Ασχολείται με authentication logic.
Κάνει:
- δημιουργία JWT token
- verify password με BCrypt
- verify JWT token
- extract token από `Authorization: Bearer ...`
---

## 6. `Controllers/AuthController.cs`
Εδώ γίνεται το πραγματικό register/login flow.
### Register
```txt
check username
hash password
create user
return safe user data
```
### Login
```txt
find user
verify password
generate JWT token
return token + user data
```
---

## 7. `Endpoints/AuthEndpoints.cs`
Δηλώνει τα auth routes.
```csharp
POST /auth/register
POST /auth/login
POST /auth/refresh
```
Παράδειγμα:
```csharp
group.MapPost("/login", async (LoginUserDto dto, AuthController controller) =>
{
  return await controller.Login(dto);
});
```
---

## 8. `Controllers/UserController.cs`
Διαχειρίζεται user CRUD/admin actions.
Περιλαμβάνει:
- get all users
- get user by id
- create user
- update user
- update role
- delete user
Δεν επιστρέφει ποτέ `HashedPassword`.
---

## 9. `Endpoints/UserEndpoint.cs`
Δηλώνει τα user routes και τα authorization policies.
```csharp
GET /users             → AdminOnly
GET /users/{id}        → SelfOrAdmin
PUT /users/{id}        → SelfOrAdmin
PUT /users/{id}/role   → AdminOnly
DELETE /users/{id}     → SelfOrAdmin
```
Παράδειγμα:
```csharp
.RequireAuthorization("AdminOnly");
```
---

## 10. `Extensions/AuthExtensions.cs`
Ρυθμίζει πώς το ASP.NET διαβάζει και ελέγχει JWT tokens.
Κάνει setup:
```csharp
services.AddAuthentication(...)
services.AddAuthorization(...)
```
Και φτιάχνει policies:
```txt
AdminOnly    → μόνο role ADMIN
SelfOrAdmin  → ίδιο user id ή ADMIN
```
Χωρίς αυτό, τα `.RequireAuthorization(...)` δεν ξέρουν πώς να δουλέψουν.
---

## 11. `Program.cs`
Ενώνει όλα τα κομμάτια με Dependency Injection και middleware.
Services:
```csharp
builder.Services.AddScoped<UserDao>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthController>();
builder.Services.AddJwtAuth(builder.Configuration);
```
Middleware:
```csharp
app.UseAuthentication();
app.UseAuthorization();
```
Routes:
```csharp
app.MapAuthEndpoints();
app.MapUsersEndpoints();
```
---
# NuGet Packages Used
## EF Core / SQLite
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```
Για SQLite database και migrations.

## EF CLI Tool
```bash
dotnet tool install --global dotnet-ef
```
Για commands όπως:
```bash
dotnet ef migrations add InitialCreate --output-dir Data\Migrations
dotnet ef database update
```
## Validation
```bash
dotnet add package Microsoft.Extensions.Validation
```
Για attributes όπως:
```csharp
[Required]
[MinLength(6)]
[EmailAddress]
```
και:
```csharp
builder.Services.AddValidation();
```

## BCrypt
```bash
dotnet add package BCrypt.Net-Next
```
Για password hashing:
```csharp
BCrypt.Net.BCrypt.HashPassword(password);
BCrypt.Net.BCrypt.Verify(password, hash);
```

## JWT
```bash
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
Για δημιουργία και έλεγχο JWT tokens.

# Final Flow

```txt
Register
→ CreateUserDto
→ AuthController.Register
→ BCrypt hash
→ UserDao.Create
→ SQLite Users table

Login
→ LoginUserDto
→ AuthController.Login
→ UserDao.GetByUsername
→ BCrypt Verify
→ AuthService.GenerateAccessToken
→ JWT token

Protected route
→ Authorization: Bearer token
→ AuthExtensions validates token
→ RequireAuthorization policy
→ Controller method runs
```
