using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Game.Configs;
using FreeFlow.Game.Views;
using UnityEngine;

namespace FreeFlow.Game.Views.Board
{
    /**/
    public class Dot : View
    {
        #region Fields

        private ElementConfig _config;

        [SerializeField]
        private SpriteRenderer _icon;

        #endregion

        #region Accessors

        public ElementConfig Config => _config;

        #endregion

        #region Mutators

        public void SetConfig(ElementConfig config)
        {
            _config = config;
        }

        #endregion

        #region Life Cycles

        private void Start()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if(_config != null)
            {
                UpdateUI(_config.Icon);
                UpdateUI(_config.Color);
            }
        }

        private void UpdateUI(Color color)
        {
            if(color != null)
            {
                GetComponent<SpriteRenderer>().color = color;
            }
        }

        private void UpdateUI(Sprite icon)
        {
            if(icon != null)
            {
                _icon.sprite = icon;
            }
        }

        #endregion

    }
}
