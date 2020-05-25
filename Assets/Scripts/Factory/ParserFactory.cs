using System;
using System.Collections.Generic;
using System.Linq;
using FreeFlow.Parsers.Interfaces;
using UnityEngine;

namespace FreeFlow.Factory
{
    /**/
    public static class ParserFactory
    {
        #region Fields

        private readonly static Dictionary<Type, IParser> _parsers = new Dictionary<Type, IParser>();

        #endregion

        #region Opretions

        public static void Add(Type type, IParser controller)
        {
            if (!_parsers.ContainsKey(type))
                _parsers.Add(type, controller);
            else
                Debug.LogWarningFormat("{0} Already in factory", type.Name);
        }

        public static void Remove(Type type)
        {
            if (_parsers.ContainsKey(type))
                _parsers.Remove(type);
            else
                Debug.LogWarningFormat("{0} not found", type.Name);
        }

        private static void create(Type type)
        {
            if (type.GetInterfaces().Contains(typeof(IParser)))
            {
                IParser controller = Activator.CreateInstance(type) as IParser;
                Add(type, controller);
            }
            else
            {
                Debug.LogWarningFormat("Can not create {0}. Because it is not implementing IController Type", type.Name);
            }
        }

        #endregion

        #region Accessors

        public static IParser Get<T>()
        {
            return Get(typeof(T));
        }

        public static IParser Get(Type type)
        {
            if (!_parsers.ContainsKey(type))
            {
                create(type);
            }

            return _parsers[type] ?? default;
        }

        #endregion
    }
}
