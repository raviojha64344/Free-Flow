using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Game.Components;
using FreeFlow.Game.Models;
using FreeFlow.GameData;
using FreeFlow.Loaders;
using UnityEngine;

namespace FreeFlow.Game.Views.Screens
{
    /**/
    public class MenuScreen : ScreenView
    {
        #region Fields

        [SerializeField]
        private LevelButtonComponent lvlBtnPrefab;

        [SerializeField]
        private Transform parent;

        private List<LevelData> _levelData;
        private LevelDataModel LevelDataModel;

        #endregion

        #region Mono LifeCycle

        private void Start()
        {
            LevelDataModel = LevelDataModel.Instance;
        }

        #endregion

        #region Mutators

        public void SetData(List<LevelData> levelData)
        {
            if(levelData != null)
            {
                _levelData = levelData;
                CreateLevelButton(_levelData);
            }
        }

        #endregion

        #region Spawn

        private void CreateLevelButton(List<LevelData> levelData)
        {
            Debug.Log("Create Level Button");
            foreach (LevelData lvlData in levelData)
            {
                Spawn(lvlData);
            }
        }

        private void Spawn(LevelData lvlData)
        {
            LevelButtonComponent lvlBtnComponent = Instantiate(lvlBtnPrefab, parent);
            lvlBtnComponent.SetConfig(lvlData);
            lvlBtnComponent.OnLevelButtonClicked += HandleOnLevelButtonClicked;
        }

        #endregion

        #region Handlers

        private void HandleOnLevelButtonClicked(int lvlNum)
        {
            LevelDataModel.CurrentLevel = lvlNum;
            //Load Game
            SceneLoader
                .GetInstance()
                .SetSceneName("Game")
                .Load();
        }

        #endregion
    }
}
