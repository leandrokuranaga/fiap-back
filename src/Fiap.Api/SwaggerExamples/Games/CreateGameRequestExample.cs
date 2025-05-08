using Fiap.Application.Games.Models.Request;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Api.SwaggerExamples.Games
{
    [ExcludeFromCodeCoverage]
    public class CreateGameRequestExample : IExamplesProvider<CreateGameRequest>
    {
        public CreateGameRequest GetExamples()
        {
            return new CreateGameRequest
            {
                Name = "Elden Ring",
                Genre = "Adventure",
                Price = 299.90M,
            
            };
        }
    }
}
