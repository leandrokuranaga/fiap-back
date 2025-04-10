using Fiap.Domain.GameAggregate;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class GameSeed
    {
        public static List<GameDomain> Game()
        {
            return [
                new GameDomain(1, "The Legend of Zelda: Breath of the Wild", "Action RPG", 299.00, 1),
                new GameDomain(2, "The Witcher 3: Wild Hunt", "Action RPG", 39.99, 1),
                new GameDomain(3, "Red Dead Redemption 2", "Action-adventure", 49.99, 3),
                new GameDomain(4, "Dark Souls III", "Action RPG", 29.99, 2),
                new GameDomain(5, "God of War", "Action-adventure", 39.99, 2),
                new GameDomain(6, "Minecraft", "Sandbox", 26.95, 1),
                new GameDomain(7, "Overwatch", "First-person shooter", 39.99, 3),
                new GameDomain(8, "The Last of Us Part II", "Action-adventure", 49.99, null),
            ];
        }

    }
}
