using System;
using FreeFlow.Game.Components;
using UnityEngine;

namespace FreeFlow.Game.Views.Screens
{
    /**/
    public class GameOverScreen : ScreenView
    {
        #region Events

        public Action OnRestartClicked;
        public Action OnMenuClicked;
        public Action OnNextClicked;

        #endregion

        #region Fields

        [SerializeField]
        private SimpleButton restartButton;

        [SerializeField]
        private SimpleButton menuButton;

        [SerializeField]
        private SimpleButton nextButton;

        #endregion

        #region Mono LifeCycle

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        #endregion

        #region Listeners

        private void AddListeners()
        {
            RemoveListeners();
            if(restartButton != null)
            {
                restartButton.OnClick += HandleOnRestartClick;
            }
            if (menuButton != null)
            {
                menuButton.OnClick += HandleOnMenuClick;
            }
            if (nextButton != null)
            {
                nextButton.OnClick += HandleOnNextClick;
            }
        }

        private void RemoveListeners()
        {
            if (restartButton != null)
            {
                restartButton.OnClick -= HandleOnRestartClick;
            }
            if (menuButton != null)
            {
                menuButton.OnClick -= HandleOnMenuClick;
            }
            if (nextButton != null)
            {
                nextButton.OnClick -= HandleOnNextClick;
            }
        }

        #endregion

        #region Handlers

        private void HandleOnNextClick()
        {
            DispatchOnNextClicked();
        }

        private void HandleOnMenuClick()
        {
            DispatchOnMenuClicked();
        }

        private void HandleOnRestartClick()
        {
            DispatchOnRestartClicked();
        }

        #endregion

        #region Dispatchers

        private void DispatchOnNextClicked()
        {
            OnNextClicked?.Invoke();
        }

        private void DispatchOnMenuClicked()
        {
            OnMenuClicked?.Invoke();
        }

        private void DispatchOnRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }

        #endregion
    }
}
