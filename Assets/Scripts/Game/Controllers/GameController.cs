using System;
using System.Collections.Generic;
using FreeFlow.Factory;
using FreeFlow.Game.Managers;
using FreeFlow.Game.Models;
using FreeFlow.Game.Views.Board;
using FreeFlow.Game.Views.Screens;
using FreeFlow.Loaders;
using TMPro;
using UnityEngine;

namespace FreeFlow.Game.Controllers
{
    /**/
    public class GameController : Controller
    {
        #region Fields

        [SerializeField]
        private ScreenManager screenManager;

        [SerializeField]
        private TextMeshProUGUI levelNumberTxt; 

        private LevelDataModel LevelDataModel;
        private BoardController boardController;

        private readonly Dictionary<int, List<CellView>> _path = new Dictionary<int, List<CellView>>();

        private int numRows = 0;
        private int numColumns = 0;

        private int[,] board;
        private CellView currentCell;
        private Color currentLineColor;
        private CellView startCell;
        private int startCellValue;

        private bool isGameOver = false;

        private GameOverScreen gameOverScreen = null; 

        #endregion

        #region Accessors

        public int StartCellValue => startCellValue;
        public Color CurrentLineColor => currentLineColor;
        public int Rows => numRows;
        public int Columns => numColumns;

        #endregion

        #region Mono LifeCycle

        //Gaint Update
        private void Update()
        {
            if (isGameOver)
                return;

            Vector3 rayPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                CellView cell = GetCell(rayPosition);
                if (cell != null)
                {
                    if(cell.HasValue() && !cell.IsCompleted)
                    {
                        currentCell = cell;
                        startCell = cell;
                        startCellValue = currentCell.CellValue;
                        if (!_path.ContainsKey(startCellValue))
                        {
                            _path.Add(startCellValue, new List<CellView>());
                        }
                        else if(_path.ContainsKey(StartCellValue) && _path[startCellValue].Count > 0)
                        {
                            RemovePath(StartCellValue);
                        }
                        _path[startCellValue].Add(currentCell);
                        currentLineColor = currentCell.GetColor();
                    }
                    else if(!cell.IsCompleted && cell.CellValue != 0)
                    {

                        currentCell = cell;
                        startCell = _path[cell.CellValue][0];
                        startCellValue = _path[cell.CellValue][0].CellValue;
                        currentLineColor = _path[cell.CellValue][0].GetColor();
                        _path[startCellValue].Add(currentCell);
                    }
                    else if(cell.IsCompleted)
                    {
                        RemovePath(cell.CellValue);
                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if(currentCell != null)
                {
                    CellView cell = GetCell(rayPosition);
                    if (cell != null && cell.Index != currentCell.Index)
                    {
                        if (currentCell.AddLine(cell))
                        {
                            currentCell = cell;
                            _path[startCellValue].Add(currentCell);
                            if (currentCell.IsCompleted)
                            {
                                currentCell = null;
                                startCell.IsCompleted = true;
                            }
                        }
                        else
                        {
                            RemovePath(StartCellValue);
                            currentCell = null;
                        }
                    }
                }
            }else if (Input.GetMouseButtonUp(0))
            {
                if(currentCell != null) { 
                    if (currentCell.HasValue() && currentCell.IsCompleted)
                    {
                        currentCell = null;
                        startCell = null;
                    }
                    else
                    {
                        RemovePath(StartCellValue);
                        currentCell = null;
                    }
                }
                CheckForGameOver();
            }
        }

        public void RemovePathFrom(CellView fromCell)
        {
            if (_path.ContainsKey(fromCell.CellValue))
            {
                List<CellView> cells = _path[fromCell.CellValue];

                int cellValue = fromCell.CellValue;
                int index = cells.IndexOf(fromCell);
                index = index > 0 ? index - 1 : index;

                for (int i = index; i < cells.Count; i++)
                {
                    if (i != index && !cells[i].HasValue())
                        cells[i].CellValue = 0;

                    cells[i].IsCompleted = false;
                    cells[i].RemoveLine();
                }
                int removeIndex = index == 0 ? index : index + 1;
                _path[cellValue].RemoveRange(removeIndex, _path[cellValue].Count - removeIndex);
            }
        }

        private void CheckForGameOver()
        {
            if (boardController.IsBoardFull())
            {
                Debug.Log("Game Over...");
                isGameOver = true;
                ShowGameOver();
            }
        }

        private void ShowGameOver()
        {
            gameOverScreen = screenManager.Spawn("GameOver") as GameOverScreen;
            AddListeners(gameOverScreen);
        }

        private void RemovePath(int cellValue)
        {
            if (_path.ContainsKey(cellValue))
            {
                foreach (CellView cell in _path[cellValue])
                {
                    cell.Reset();
                    cell.RemoveLine();
                }
                _path[cellValue].RemoveRange(0, _path[cellValue].Count);
            }
        }

        private CellView GetCell(Vector3 rayPosition)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(rayPosition, Vector2.zero);

            if (raycastHit.collider != null)
            {
                CellView cell = raycastHit.collider.GetComponent<CellView>();
                if (cell != null)
                    return cell;
            }
            return null;
        }

        #endregion

        #region Listeners

        private void AddListeners(GameOverScreen gameOverScreen)
        {
            if (gameOverScreen != null)
            {
                gameOverScreen.OnMenuClicked += HandleOnMenuClicked;
                gameOverScreen.OnNextClicked += HandleOnNextClicked;
                gameOverScreen.OnRestartClicked += HandleOnRestartClicked;
            }
        }

        private void HandleOnRestartClicked()
        {
            RestartGame();
        }

        private void HandleOnNextClicked()
        {
            if(LevelDataModel != null)
            {
                LevelDataModel.CurrentLevel = LevelDataModel.CurrentLevel < LevelDataModel.LevelDataList.Count ? ++LevelDataModel.CurrentLevel : 1;
                RestartGame();
            }
        }

        #endregion

        #region Init

        protected override void Collect()
        {
            base.Collect();
            boardController = ControllerFactory.Get<BoardController>() as BoardController;
        }

        protected override void Init()
        {
            LevelDataModel = LevelDataModel.Instance;

            StartGame();
        }

        private void RestartGame()
        {
            screenManager.RemoveCurrentScreen();
            boardController.ClearBoard();
            isGameOver = false;
            StartGame();
        }

        private void StartGame()
        {
            numRows = numColumns = LevelDataModel.BoardValue;
            boardController?.CreateBoard(numRows, numColumns);
            boardController?.AddPairToBoard(LevelDataModel?.GetPairData(LevelDataModel.CurrentLevel));

            UpdateUI(levelNumberTxt);
        }

        private void UpdateUI(TextMeshProUGUI lvlNumTxt)
        {
            if(lvlNumTxt != null)
            {
                lvlNumTxt.text = string.Format("Level - {0}", LevelDataModel.CurrentLevel);
            }
        }

        #endregion

        #region Handlers

        private void HandleOnMenuClicked()
        {
            SceneLoader
                .GetInstance()
                .SetSceneName("Menu")
                .Load();
        }

        #endregion
    }
}
