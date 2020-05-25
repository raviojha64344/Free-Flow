using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Factory;
using FreeFlow.Game.Configs;
using FreeFlow.Game.Controllers;
using FreeFlow.Game.Views;
using UnityEngine;

namespace FreeFlow.Game.Views.Board
{
    /**/
    public class CellView : View
    {
        #region Fields

        [SerializeField]
        private Dot dotPrefab;

        [SerializeField]
        private Line linePrefab;

        [SerializeField]
        private ElementList ElementList;

        private int _cellValue = 0;
        private int _index;
        private int _targetIndex = -1;
        private Dot _dot;
        private Line _line;

        private readonly List<int> adjacents = new List<int>();
        private GameController gameController;

        #endregion

        private void Start()
        {
            Collect();
        }

        private void Collect()
        {
            gameController = ControllerFactory.Get<GameController>() as GameController;
        }

        public void CreateElement()
        {
            ElementConfig element = ElementList?.GetElementById(_cellValue) ??  null;
            if(element != null)
            {
                CreateDot(element);
            }
        }

        private void CreateDot(ElementConfig element)
        {
            _dot = Instantiate(dotPrefab, transform);
            _dot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _dot.SetConfig(element);
        }

        public bool AddLine(CellView nextCell)
        {
            if (adjacents.Contains(nextCell.Index))
            {
                if (nextCell.Line != null && nextCell.CellValue == gameController.StartCellValue)
                {
                    nextCell.RemoveLine();
                    return true;
                }

                if((nextCell.Line != null || nextCell.CellValue != 0) && nextCell.CellValue != gameController.StartCellValue)
                {
                    gameController.RemovePathFrom(nextCell);
                }

                Vector3 endPosition = Vector3.zero;
                _line = Instantiate(linePrefab, transform);
                _line.SetLineColor(gameController.CurrentLineColor);
                _line.SetPosition(0, Vector3.zero);

                if (transform.position.x < nextCell.transform.position.x)
                    endPosition = new Vector3(1f, 0f, 0f);
                else if(transform.position.x > nextCell.transform.position.x)
                    endPosition = new Vector3(-1f, 0f, 0f);
                else if(transform.position.y > nextCell.transform.position.y)
                    endPosition = new Vector3(0f, -1f, 0f);
                else if(transform.position.y < nextCell.transform.position.y)
                    endPosition = new Vector3(0f, 1f, 0f);

                _line.SetPosition(1, endPosition);

                if (nextCell.HasValue() && nextCell.CellValue == gameController.StartCellValue) {
                    nextCell.IsCompleted = true;
                }
                else
                {
                    nextCell.CellValue = gameController.StartCellValue;
                }

                return true;
            }

            return false;
        }

        public void RemoveLine()
        {
            if(_line != null)
            { 
                Destroy(_line.gameObject);
                _line = null;
            }
        }

        public void Reset()
        {
            if (!HasValue())
                CellValue = 0;
            IsCompleted = false;
        }

        #region Mutators

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetTargetIndex(int index)
        {
            _targetIndex = index;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetValue(int value)
        {
            _cellValue = value;
        }

        public void SetAdjacents(int i, int j)
        {
            AddAdjacents(i, j + 1);
            AddAdjacents(i, j - 1);
            AddAdjacents(i + 1, j);
            AddAdjacents(i - 1, j);
        }

        #endregion

        #region Accessors

        public int Index
        {
            get
            {
                return _index;
            }
        }

        public Line Line
        {
            get
            {
                return _line;
            }
        }

        public int CellValue
        {
            get
            {
                return _cellValue;
            }
            set
            {
                _cellValue = value;
            }
        }

        public bool IsCompleted { get; internal set; }

        public Color GetColor()
        {
            if (_dot != null)
                return _dot.Config.Color;

            return Color.black;
        }

        #endregion

        #region Operations

        private void AddAdjacents(int i, int j)
        {
            Collect();
            if ((i >= 0 && i < gameController.Rows) && (j >= 0 && j < gameController.Columns))
            {
                adjacents.Add(i * gameController.Columns + j);
            }
        }

        public bool HasValue()
        {
            return _dot != null;
        }

        public bool IsAdjacenet(int adjacentIndex)
        {
            if (adjacents.Count == 0)
                return false;

            return adjacents.Contains(adjacentIndex);
        }

        #endregion

        public void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}
