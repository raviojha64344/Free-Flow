using System.Collections;
using System.Collections.Generic;
using FreeFlow.Parsers.Interfaces;
using UnityEngine;

namespace FreeFlow.Parsers
{
    /**/
    public abstract class Parser : IParser
    {
        #region Interface Implementation

        public virtual T From<T>(string str)
        {
            return default;
        }

        public virtual string To(object obj)
        {
            return string.Empty;
        }

        #endregion
    }
}
