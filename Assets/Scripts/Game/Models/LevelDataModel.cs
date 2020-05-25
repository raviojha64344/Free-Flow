using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FreeFlow.GameData;
using UnityEngine;

namespace FreeFlow.Game.Models
{
    /**/
    public class LevelDataModel
    {
        #region Events

        public delegate void LevelListUpdated(List<LevelData> levelDataList);
        public event LevelListUpdated OnLevelListUpdated;

        #endregion

        #region Singleton

        private static LevelDataModel _instance;

        public static LevelDataModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LevelDataModel();
                return _instance;
            }
        }

        #endregion

        #region Propeties

        private int _boardValue;
        public int BoardValue
        {
            get
            {
                return _boardValue;
            }
            set
            {
                SetBoardValue(value);
            }
        }

        private List<LevelData> _levelDataList;
        public List<LevelData> LevelDataList
        {
            get
            {
                return _levelDataList;
            }
            set
            {
                SetLevelDataList(value);
            }
        }

        private int _currentLevel;
        public int CurrentLevel
        {
            get
            {
                return _currentLevel;
            }
            set
            {
                SetCurrentLevel(value);
            }
        }

        #endregion

        #region Accessors

        //public int[,] GetBoard()
        //{
        //    List<string> level = _levelDataList.FirstOrDefault(lvlData => lvlData.levelNum == _currentLevel);
        //    int[,] board = new int[level.Count,level.Count];
        //    for (int i = 0; i < level.Count; i++)
        //    {
        //        string[] row = level[i].Split(',');
        //        for(int j = 0; j < row.Length; j++)
        //        {
        //            if (!int.TryParse(row[j], out board[i,j]))
        //            {
        //                Debug.LogWarning("Error in level data");
        //                break;
        //            }
        //            Debug.Log(board[i,j]);
        //        }
        //    }

        //    return board;
        //}

        public List<Pair> GetPairData(int currentLevel)
        {
            return _levelDataList?.FirstOrDefault(lvlData => lvlData.levelNum == currentLevel).pairData ?? null;
        }

        #endregion

        #region Mutators

        private void SetLevelDataList(List<LevelData> value)
        {
            _levelDataList = value;
            DispatchOnLevelListUpdated(_levelDataList);
        }

        private void SetCurrentLevel(int value)
        {
            _currentLevel = value;
        }

        private void SetBoardValue(int value)
        {
            _boardValue = value;
        }

        #endregion

        #region Dispatchers

        private void DispatchOnLevelListUpdated(List<LevelData> levelDataList)
        {
            OnLevelListUpdated?.Invoke(levelDataList);
        }

        #endregion
    }
}
