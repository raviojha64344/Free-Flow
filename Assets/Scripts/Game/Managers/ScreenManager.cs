using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Game.Interfaces;
using FreeFlow.Game.Models;
using FreeFlow.Game.Views.Screens;
using UnityEngine;

namespace FreeFlow.Game.Managers
{
    /**/
    public class ScreenManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private ScreenModel ScreenModel;

        private IScreen screen;

        #endregion

        #region Accessors

        private ScreenView GetScreen(string screenName)
        {
            return ScreenModel?.GetScreen(screenName) ?? null;
        }

        #endregion

        public IScreen Spawn(string screenName)
        {
            if (screen != null)
            {
                screen.Hide();
                screen = null;
            }

            screen = Instantiate(GetScreen(screenName), transform);
            screen.Show();
            return screen;
        }

        public void RemoveCurrentScreen()
        {
            if(screen != null)
            {
                Destroy((screen as ScreenView).gameObject);
                screen = null;
            }
        }
    }
}
