using UnityEngine;

namespace cyberspeed.MatchGame
{
    public class GameModeData : IGameModeService
    {
        private int rows, columns;
        public int GetGridItemSize()
        {
            Debug.Log($"GetGridItemSize {rows} {columns}");
            return Resources.Load<RowColumnSizeMap>("RowColumnSizeMap").GetCellSize(rows, columns);
        }

        public int GetNumberOfColumns()
        {
            return columns;
        }

        public int GetNumberOfRows()
        {
            return rows;
        }

        public void SetGameGrid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

    }
}