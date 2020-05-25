using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FreeFlow.Game.Views.Screens;
using UnityEngine;

namespace FreeFlow.Game.Models
{
    [CreateAssetMenu(fileName = "ScreenModel", menuName = "Free Flow/Screens/Create Screen Model")]
    public class ScreenModel : ScriptableObject
    {
        [SerializeField]
        private List<ScreenView> _screens;

        #region Accessors

        public ScreenView GetScreen(string name)
        {
            if(_screens.Any() && !string.IsNullOrEmpty(name))
                return _screens.FirstOrDefault(screen => screen.Name == name);
            return null;
        }


        //Not Working
        public ScreenView GetScreen(Type type)
        {
            if (_screens.Any() && type != null)
                _screens.FirstOrDefault(screen => screen.GetType().ToString() == type.ToString());
            return null;
        }

        #endregion
    }
}
