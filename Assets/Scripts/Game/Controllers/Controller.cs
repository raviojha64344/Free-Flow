using System;
using FreeFlow.Factory;
using FreeFlow.Game.Interfaces;
using UnityEngine;

namespace FreeFlow.Game.Controllers
{
    /**/
    public abstract class Controller : MonoBehaviour, IController
    {
        #region Fields

        [SerializeField]
        private bool destroyOnLoad = true;

        #endregion

        #region Mono LifeCycle

        protected virtual void Awake()
        {
            ControllerFactory.Add(this.GetType(), this);
        }

        protected virtual void OnEnable()
        {
            AddListeners();
        }

        protected virtual void Start()
        {
            Collect();
            Init();
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }

        protected virtual void OnDestroy()
        {
            RemoveListeners();
            if(destroyOnLoad)
                ControllerFactory.Remove(this.GetType());
        }

        #endregion

        protected virtual void Init()
        {
            
        }

        protected virtual void Collect()
        {
            //Templete
        }


        #region Listeners

        protected virtual void AddListeners()
        {
            RemoveListeners();
        }

        protected virtual void RemoveListeners()
        {
            //Templete
        }

        #endregion
    }
}
