using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FreeFlow.Game.Components
{
    /**/
    [RequireComponent(typeof(Button))]
    public class SimpleButton : MonoBehaviour
    {
        #region Events

        public Action OnClick;

        #endregion

        #region Fields

        private Button _button;

        #endregion

        #region Mono LifeCycle

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            AddListener(_button);
        }

        private void OnDisable()
        {
            RemoveListener(_button);
        }

        private void OnDestroy()
        {
            RemoveListener(_button);
        }

        #endregion

        #region Listeners

        private void AddListener(Button button)
        {
            RemoveListener(button);
            if (button != null)
            {
                button.onClick.AddListener(HandleOnClick);
            }
        }

        private void RemoveListener(Button button)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(HandleOnClick);
            }
        }

        #endregion


        #region Handlers

        private void HandleOnClick()
        {
            DispatchOnClick();
        }

        #endregion

        #region Dispatchers

        private void DispatchOnClick()
        {
            OnClick?.Invoke();
        }

        #endregion
    }
}
