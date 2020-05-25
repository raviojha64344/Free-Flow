using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Factory;
using FreeFlow.Game.Controllers;
using FreeFlow.Game.Managers;
using FreeFlow.Game.Models;
using FreeFlow.Game.Views.Screens;
using FreeFlow.GameData;
using UnityEngine;

namespace FreeFlow.Game.Controllers
{
    /**/
    public class UIController : Controller
    {
        #region Fields

        private LevelController LevelController;

        [SerializeField]
        private ScreenManager screenManager;

        #endregion

        #region Collect

        protected override void Collect()
        {
            base.Collect();
            LevelController = ControllerFactory.Get<LevelController>() as LevelController;
        }

        #endregion

        #region Listeners

        protected override void AddListeners()
        {
            base.AddListeners();
            AddListener(LevelDataModel.Instance);
        }

        private void AddListener(LevelDataModel levelDataModel)
        {
            if(levelDataModel != null)
            {
                levelDataModel.OnLevelListUpdated += HandleOnLevelDataListUpdated;
            }
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            RemoveListener(LevelDataModel.Instance);
        }

        private void RemoveListener(LevelDataModel levelDataModel)
        {
            if (levelDataModel != null)
            {
                levelDataModel.OnLevelListUpdated -= HandleOnLevelDataListUpdated;
            }
        }

        #endregion

        #region Handlers

        private void HandleOnLevelDataListUpdated(List<LevelData> levelData)
        {
            MenuScreen menuScreen = screenManager.Spawn("Menu") as MenuScreen;
            menuScreen.SetData(levelData);
        }

        #endregion

    }
}
