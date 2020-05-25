using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlow.Game.Interfaces
{
    /**/
    public interface IScreen
    {
        string Name { get; }
        void Show();
        void Hide();
    }
}
