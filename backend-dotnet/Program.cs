// backend-dotnet\Program.cs
using backend_dotnet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.Services.AddCors( options =>
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

var connString = builder.Configuration.GetConnectionString("TodoDb");
builder.Services.AddSqlite<TodoContext>(connString);

builder.Services.AddScoped<TodoDao>();
builder.Services.AddScoped<TodoController>();

var app = builder.Build();
app.UseCors("AllowedFrontend");
app.MigrateDb();
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => "ok");
app.MapGet("/api/ping", () =>
{
  Console.WriteLine("someone pinged here");
  return "Pong";  
});
app.MapTodoEndpoints();

app.Urls.Add("http://localhost:3001");
app.Run();
