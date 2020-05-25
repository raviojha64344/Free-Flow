using System.Collections;
using System.Collections.Generic;
using FreeFlow.Game.Interfaces;
using FreeFlow.Game.Views;
using UnityEngine;

namespace FreeFlow.Game.Views.Screens
{
    /**/
    public abstract class ScreenView : View, IScreen
    {
        #region Fields

        [SerializeField]
        private string screenName;

        #endregion

        #region Accessors

        public string Name => screenName;

        #endregion

        #region Interface Implementation

        public virtual void Show()
        {
            //Templete
        }

        public virtual void Hide()
        {
            //Templete
        }

        #endregion

    }
}
