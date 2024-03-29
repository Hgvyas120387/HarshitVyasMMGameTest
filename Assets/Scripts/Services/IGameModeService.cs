using cyberspeed.Services;

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
    }
}
