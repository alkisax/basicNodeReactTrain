using System.ComponentModel.DataAnnotations;

namespace dotnet_ai_quote_generator;

public record LogFromFrontDto
(
  [Required]
  string Data
);
