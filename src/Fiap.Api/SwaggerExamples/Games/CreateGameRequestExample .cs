using Fiap.Application.Games.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Fiap.Api.SwaggerExamples.Games
{
    public class CreateGameRequestExample : IExamplesProvider<CreateGameRequest>
    {
        public CreateGameRequest GetExamples()
        {
            return new CreateGameRequest
            {
                Name = "Elden Ring",
                Genre = "Adventure",
                Price = 299.90,
            
            };
        }
    }
}
