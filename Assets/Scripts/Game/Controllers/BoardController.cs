using System.Collections.Generic;
using FreeFlow.Game.Models;
using FreeFlow.Game.Views;
using FreeFlow.GameData;
using UnityEngine;

namespace FreeFlow.Game.Controllers
{
    /**/
    public class BoardController : Controller
    {
        #region Fields

        [SerializeField]
        private BoardView boardView;

        private Vector2 screen;
        private float cellSize;

        private LevelDataModel LevelDataModel;

        #endregion

        protected override void Start()
        {
            base.Start();
            screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            Debug.Log(screen);
            LevelDataModel = LevelDataModel.Instance;
            cellSize = screen.x / LevelDataModel.BoardValue;
            Debug.Log(cellSize);
        }

        #region Board Operations

        public void CreateBoard(int rows, int columns)
        {
            boardView?.CreateBoard(rows, columns, cellSize);
            boardView?.SetPosition(new Vector3(-screen.x / 2f - 0.5f, screen.y / 2f - 0.5f, 0f));
        }

        public void RemoveCellLine(int index)
        {
            boardView.GetCell(index).RemoveLine();
        }

        public void AddPairToBoard(List<Pair> pairList)
        {
            foreach (Pair pair in pairList)
            {
                for (int i = 0; i < pair.pairs.Count; i++)
                {
                    boardView.GetCell(pair.pairs[i]).CellValue = pair.pairValue;
                    boardView.GetCell(pair.pairs[i]).CreateElement();
                }
            }
        }

        public void ClearBoard()
        {
            boardView.Clear();
            boardView.SetPosition(Vector3.zero);
        }

        #endregion

        #region Game Over Condition

        public bool IsBoardFull()
        {
            return boardView.CellValueCompleted() && boardView.IsAllCellFull();
        }

        #endregion
    }
}
