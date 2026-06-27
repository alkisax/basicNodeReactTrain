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

var connString = "Data Source=Quotes.db";
builder.Services.AddSqlite<QuoteContext>(connString);

var app = builder.Build();

app.MigrateDb();

app.UseCors("AllowedFrontend");
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

app.Urls.Add("http://localhost:3001");

app.Run();
