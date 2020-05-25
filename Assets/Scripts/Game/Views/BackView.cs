using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Game.Components;
using FreeFlow.Loaders;
using UnityEngine;

namespace FreeFlow.Game.Views
{
    /**/
    public class BackView : View
    {
        #region Fields

        [SerializeField]
        private SimpleButton _backButton;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            AddListener(_backButton);
        }

        private void OnDisable()
        {
            RemoveListener(_backButton);
        }

        #endregion

        #region Listeners

        private void AddListener(SimpleButton backButton)
        {
            RemoveListener(backButton);
            if (backButton != null)
            {
                backButton.OnClick += HandleOnBackClicked;
            }
        }

        private void RemoveListener(SimpleButton backButton)
        {
            if (backButton != null)
            {
                backButton.OnClick -= HandleOnBackClicked;
            }
        }

        #endregion

        #region Handlers

        private void HandleOnBackClicked()
        {
            SceneLoader
                .GetInstance()
                .SetSceneName("Menu")
                .Load();
        }

        #endregion

    }
}
