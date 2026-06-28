using dotnet_ai_quote_generator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowedFrontend", policy =>
  {
    policy
    .WithOrigins(
         "http://localhost:8081",
        "http://localhost:5173"
    )
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
});

builder.Services.AddScoped<AiController>();
builder.Services.AddScoped<QuoteDao>();
builder.Services.AddScoped<QuoteController>();
builder.Services.AddScoped<UserDao>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthController>();
builder.Services.AddScoped<LogFromFrontController>();

var connString = "Data Source=Quotes.db";
builder.Services.AddSqlite<AppDbContext>(connString);
// ρυθμίζει JWT authentication/authorization διαβάζοντας το JWT_SECRET από config
builder.Services.AddJwtAuth(builder.Configuration);

var app = builder.Build();

app.MigrateDb();

app.UseCors("AllowedFrontend");
// αυτα είναι ουσιαστικά τα middleware μου
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => "ok");
app.MapGet("/api/ping", () =>
{
  Console.WriteLine("someone pinged here");
  return "Pong";
});

app.MapAiQuoteEndpoints();
app.MapQuoteEndpoints();
app.MapAuthEndpoints();
app.MapUsersEndpoints();
app.MapLogFromFrontEndpoints();

app.Urls.Add("http://localhost:3001");

app.Run();
