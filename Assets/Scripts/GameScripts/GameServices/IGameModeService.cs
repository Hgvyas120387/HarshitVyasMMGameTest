using cyberspeed.Services;
using cyberspeed.MatchGame.UI;

namespace cyberspeed.MatchGame
{
    public interface IGameModeService : IService
    {
        /// <summary>
        /// set the grid
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="columns">number of columns</param>
        public void SetGameGrid(int rows, int columns);
        /// <summary>
        /// Gives the size of item as per game mode
        /// </summary>
        /// <returns></returns>
        public int GetGridItemSize();
        /// <summary>
        /// Get number of rows for current game mode
        /// </summary>
        /// <returns>number of rows</returns>
        public int GetNumberOfRows();
        /// <summary>
        /// Get number of columns for current game mode
        /// </summary>
        /// <returns>number of columns</returns>
        public int GetNumberOfColumns();
        /// <summary>
        /// Gives the array of card index
        /// </summary>
        /// <returns>array of card index</returns>
        public int[] GetCardArray();
        /// <summary>
        /// Call this when user opens any card it will check if cards are matched or not
        /// </summary>
        /// <param name="card">Card which opened by user</param>
        public void CardOpened(UICard card);
        /// <summary>
        /// Feed all the cards at the beginning of game play
        /// </summary>
        /// <param name="cards">All cards array</param>
        public void FeedAllCard(UICard[] cards);
        /// <summary>
        /// Gives the all uicard available in current game
        /// </summary>
        /// <returns>all ui cards<returns>
        public UICard[] GetAllCardsUI();
        /// <summary>
        /// In case we are playing any saved game call it to load that data if and when required
        /// </summary>
        public void SetSavedGameData(SavedGameData savedGameData);
        /// <summary>
        /// gives savedGameData if available else null
        /// </summary>
        /// <returns>SavedGameData instance</returns>
        public SavedGameData GetSavedGameDataIfAny();
    }
}
