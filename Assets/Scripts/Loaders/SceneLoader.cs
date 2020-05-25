using System;
using FreeFlow.Loaders.Interafces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FreeFlow.Loaders
{
    /**/
    public class SceneLoader : Loader
    {
        #region Fields

        private string _sceneName;

        #endregion

        #region Factory

        public static SceneLoader GetInstance()
        {
            return new SceneLoader();
        }

        #endregion

        #region Mutators

        public ILoader SetSceneName(string sceneName)
        {
            _sceneName = sceneName;
            return this;
        }

        #endregion

        #region Load

        public override void Load()
        {
            if (!string.IsNullOrEmpty(_sceneName))
            {
                try
                { 
                    SceneManager.LoadScene(_sceneName);
                }
                catch(Exception err)
                {
                    Debug.LogWarningFormat("Error while loading scene : {0}, Error : {1}", _sceneName, err.Message);
                }
            }
            else
            {
                Debug.LogWarning("Scene Name can not be Empty");
            }
        }

        #endregion
    }
}
