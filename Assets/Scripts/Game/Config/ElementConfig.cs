using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlow.Game.Configs
{
    /**/
    [CreateAssetMenu(fileName = "Element", menuName = "Free Flow/Game Config/Create Element")]
    public class ElementConfig : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private int _id;

        [SerializeField]
        private Color _color;

        [SerializeField]
        private Sprite _icon;

        #endregion

        #region Accessors

        public int ID => _id;
        public Color Color => _color;
        public Sprite Icon => _icon;

        #endregion
    }
}
