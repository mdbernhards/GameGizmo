using GameGizmo.Models;
using GameGizmo.MVVM.Model;

namespace GameGizmo.Logic.Interfaces
{
    interface IApiToViewMapper
    {
        Game MapToGame(GameData gameData, List<GameScreenshot> screenshots);

        List<Game> MapToGame(List<GameData> gameData);

        Developer MapToDeveloper(DeveloperData developerData, ListOfGames games);

        List<Developer> MapToDeveloper(List<DeveloperData> developerData);
    }
}
