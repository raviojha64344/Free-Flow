using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FreeFlow.Game.Components
{
    [RequireComponent(typeof(Button))]
    public class LevelButtonComponent : MonoBehaviour
    {

        #region Events

        public Action<int> OnLevelButtonClicked;

        #endregion

        #region Fields

        private Button button;
        private int lvlNum = 0;

        [SerializeField]
        private TextMeshProUGUI lvlTxt;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            Collect();
        }

        private void Start()
        {
            AddListener(button);
            UpdateUI();
        }

        private void OnDestroy()
        {
            RemoveListener(button);
        }

        private void Collect()
        {
            button = GetComponent<Button>();
        }

        #endregion

        #region Update UI

        private void UpdateUI()
        {
            UpdateUI(lvlNum);
        }

        private void UpdateUI(int lvlNum)
        {
            if(lvlTxt != null)
            {
                lvlTxt.text = string.Format("{0}", lvlNum.ToString("00"));
            }
        }

        #endregion

        #region Listeners

        private void AddListener(Button button)
        {
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

        #region Config

        public void SetConfig(LevelData lvlData)
        {
            lvlNum = lvlData.levelNum;
        }

        #endregion

        #region Handlers

        private void HandleOnClick()
        {
            DispatchOnClick(lvlNum);
        }

        #endregion

        #region Dispatchers

        private void DispatchOnClick(int lvlNum)
        {
            OnLevelButtonClicked?.Invoke(lvlNum);
        }

        #endregion
    }
}
