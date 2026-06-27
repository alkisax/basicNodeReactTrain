// backend-csharp\Extensions\AuthExtensions.cs
// Αυτό το αρχείο ρυθμίζει ΠΩΣ το backend καταλαβαίνει και ελέγχει JWT tokens
// 1. λέει στο .NET: "θα χρησιμοποιώ JWT authentication"
// 2. λέει: "όταν έρχεται request με token, έλεγξε το έτσι:"
// έλεγξε signature (IMPORTANT)
// έλεγξε αν έχει λήξει
// ΧΩΡΙΣ αυτό:
// [Authorize] δεν δουλεύει

// αυτό μου φτιάχνει και τα policies που καλώ στα endpoints. πχ
// group.MapPut("/{id}", async (int id, UserController controller) =>
// {
//   return await controller.Update(id, data);
// })
// .RequireAuthorization("AdminOnly");

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace backend_csharp.Extensions;

public static class AuthExtensions
{
  public static void AddJwtAuth(this IServiceCollection services, IConfiguration config)
  {
    var secret = config["JWT_SECRET"];

  if (string.IsNullOrEmpty(secret))
    {
      throw new Exception("JWT_SECRET not configured");      
    };

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret!)
          )
        };        
      });

    services.AddAuthorization(options =>
    {
      options.AddPolicy("SelfOrAdmin", policy =>
        policy.RequireAssertion(context =>
        {
          var userId = context.User.FindFirst("id")?.Value;
          var role = context.User.FindFirst(ClaimTypes.Role)?.Value;

          var routeId = context.Resource switch
          {
            HttpContext http => http.Request.RouteValues["id"]?.ToString(),
            _ => null
          };

          return role == "ADMIN" || userId == routeId;
        })
      );

      options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("ADMIN")
      );
    });
  }
}
