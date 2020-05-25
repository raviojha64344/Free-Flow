using System;
using System.Collections.Generic;
using System.Linq;
using FreeFlow.Game.Interfaces;
using UnityEngine;

namespace FreeFlow.Factory
{
    /**/
    public static class ControllerFactory
    {
        #region Fields

        private readonly static Dictionary<Type, IController> _controllers = new Dictionary<Type, IController>();

        #endregion

        #region Opretions

        public static void Add(Type type, IController controller)
        {
            if (!_controllers.ContainsKey(type))
                _controllers.Add(type, controller);
            else
                Debug.LogWarningFormat("{0} Already in factory", type.Name);
        }

        public static void Remove(Type type)
        {
            if (_controllers.ContainsKey(type))
                _controllers.Remove(type);
            else
                Debug.LogWarningFormat("{0} not found", type.Name);
        }

        private static void create(Type type)
        {
            if (type.GetInterfaces().Contains(typeof(IController)))
            {
                IController controller = Activator.CreateInstance(type) as IController;
                Add(type, controller);
            }
            else
            {
                Debug.LogWarningFormat("Can not create {0}. Because it is not implementing IController Type", type.Name);
            }
        }

        #endregion

        #region Accessors

        public static IController Get<T>()
        {
            return Get(typeof(T));
        }

        public static IController Get(Type type)
        {
            if (!_controllers.ContainsKey(type))
            {
                create(type);
            }

            return _controllers[type] ?? default;
        }

        #endregion

    }
}
