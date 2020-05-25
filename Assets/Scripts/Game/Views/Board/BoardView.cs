using System.Collections.Generic;
using System.Linq;
using FreeFlow.Game.Views.Board;
using UnityEngine;

namespace FreeFlow.Game.Views
{
    /**/
    public class BoardView : View
    {
        #region Fields

        private int numRows;
        private int numColumns;

        [SerializeField]
        private CellView _cellPrefab;

        private readonly List<CellView> cellViews = new List<CellView>();

        #endregion

        #region Board Operations

        public void CreateBoard(int rows, int columns, float cellSize)
        {
            numRows = rows;
            numColumns = columns;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    CellView cell = Instantiate(_cellPrefab, transform);
                    cell.SetIndex(i * columns + j);
                    //cell.SetTargetIndex(GetTargetIndex(i, j));
                    cell.SetPosition(new Vector3(j, -i, 0f));
                    cell.SetAdjacents(i, j);
                    cellViews.Add(cell);
                }
            }
        }

        #endregion

        #region Accessors

        public CellView GetCell(int index)
        {
            return cellViews.FirstOrDefault(cell => cell.Index == index);
        }

        #endregion

        #region Mutators

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Clear()
        {
            if(cellViews != null && cellViews.Count > 0)
            {
                foreach (CellView cell in cellViews)
                {
                    cell.Destroy();
                }
                cellViews.RemoveRange(0, cellViews.Count);
            }
        }

        #endregion

        #region Board Winning Conditions

        public bool IsAllCellFull()
        {
            return !(cellViews.Where(cell => !cell.HasValue() && cell.Line == null).Count() > 0);
        }

        public bool CellValueCompleted()
        {
            return !(cellViews.Where(cell => cell.HasValue() && !cell.IsCompleted).Count() > 0);
        }

        #endregion

        #region Dead Test Code

        //private int GetTargetIndex(int i, int j)
        //{
        //    if(_board[i,j] > 0)
        //    {
        //        for (int iRows = 0; iRows < numRows; iRows++)
        //        {
        //            for (int jColumns = 0; jColumns < numColumns; jColumns++)
        //            {
        //                if(i != iRows && j != jColumns && _board[i,j] == _board[iRows, jColumns])
        //                {
        //                    return iRows * numColumns + jColumns;
        //                }
        //            }
        //        }
        //    }

        //    return -1;
        //}

        #endregion
    }
}
