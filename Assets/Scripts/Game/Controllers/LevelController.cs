using System;
using System.Collections.Generic;
using FreeFlow.Factory;
using FreeFlow.Game.Models;
using FreeFlow.GameData;
using FreeFlow.Parsers;
using FreeFlow.Parsers.Interfaces;
using UnityEngine;

namespace FreeFlow.Game.Controllers
{
    /**/
    public class LevelController : Controller
    {
        #region Events

        public delegate void LevelParse(List<LevelData> levelData);
        public event LevelParse OnLevelParse;

        #endregion

        #region Fields

        [SerializeField]
        private TextAsset levelAsset;

        private LevelDataModel LevelDataModel;

        private IParser parser;

        #endregion

        #region Parsing

        protected override void Init()
        {
            LevelDataModel = LevelDataModel.Instance;
            parser = ParserFactory.Get<JsonParser>() as JsonParser;

            ParseLevel(levelAsset.text);
        }

        private void ParseLevel(string data)
        {
            Level Level = parser?.From<Level>(data) ?? null;
            LevelDataModel.BoardValue = Level.boardValue;
            LevelDataModel.LevelDataList = Level.Levels;
            DispatchOnLevelParse(Level?.Levels ?? null);
        }

        #endregion


        private void DispatchOnLevelParse(List<LevelData> levelData)
        {
            if(levelData != null)
            {
                Debug.Log("Parsed Level Data");
                OnLevelParse?.Invoke(levelData);
            }
            else
            {
                Debug.LogWarning("Parsing Error");
            }
        }
    }
}
