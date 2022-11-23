using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public abstract class Listener : MonoBehaviour
    {
        protected virtual void Awake(){
            RegisterListener();
        }

        protected abstract void RegisterListener();

        protected virtual void OnDestroy(){
            RemoveListener();
        }

        protected abstract void RemoveListener();
    }
}
