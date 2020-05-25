using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FreeFlow.Game.Configs
{
    [CreateAssetMenu(fileName = "Element List", menuName = "Free Flow/Game Config/Create Element List")]
    public class ElementList : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private List<ElementConfig> Elements;

        #endregion

        #region Accessors

        public ElementConfig GetElementById(int id)
        {
            if (Elements != null && Elements.Count > 0)
                return Elements.FirstOrDefault(element => element.ID == id);

            return null;
        }

        #endregion
    }
}
