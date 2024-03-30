using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public interface IGameSaveLoadService : IService
    {
        /// <summary>
        /// Call it when user want to save game so he can resume in future session
        /// </summary>
        public void SaveGame();
        /// <summary>
        /// Checks if there is any saved game or not returns json in case of any else return empty string
        /// </summary>
        /// <returns>json of saved game or empty string</returns>
        public string LoadGameIfAny();
        /// <summary>
        /// In case game finishes or user quits call it to delete saved game so on next launch we can receive empty string
        /// </summary>
        public void DeleteSavedGame();
    }
}