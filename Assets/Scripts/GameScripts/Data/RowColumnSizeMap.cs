using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RowColumnSizeMap", menuName = "ScriptableObjects/RowColumnSizeMap", order = 1)]
public class RowColumnSizeMap : ScriptableObject
{
    [Serializable]
    public class RowColumnSize
    {
        public int row;
        public int column;
        public int cellSize;
    }

    public RowColumnSize[] rowColumnSizes;

    public int GetCellSize(int rowCount, int columnCount)
    {
        Debug.Log($"Get GetCellSize : {rowCount} {columnCount}");
        return Array.Find(rowColumnSizes, p => p.row == rowCount && p.column == columnCount).cellSize;
    }
}