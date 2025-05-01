using Fiap.Domain.Common.ValueObjects;
using Fiap.Domain.GameAggregate;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class GameSeed
    {
        public static List<Game> Game()
        {
            return [
                new Game(1, "The Legend of Zelda: Breath of the Wild", "Action RPG", new Money(299.00, "BRL"), 1),
                new Game(2, "The Witcher 3: Wild Hunt", "Action RPG", new Money(39.99, "BRL"), 1),
                new Game(3, "Red Dead Redemption 2", "Action-adventure", new Money(49.99, "BRL"), 3),
                new Game(4, "Dark Souls III", "Action RPG", new Money(29.99, "BRL"), 2),
                new Game(5, "God of War", "Action-adventure", new Money(39.99, "BRL"), 2),
                new Game(6, "Minecraft", "Sandbox", new Money(26.95, "BRL"), 1),
                new Game(7, "Overwatch", "First-person shooter", new Money(39.99, "BRL"), 3),
                new Game(8, "The Last of Us Part II", "Action-adventure", new Money(49.99, "BRL"), null),
            ];
        }

    }
}
