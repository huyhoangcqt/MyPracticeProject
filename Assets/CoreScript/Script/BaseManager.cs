using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager : MonoSingleton<BaseManager>
{
    void Awake(){
        Initialize();    
        Setup();
    }

    private void Start() {
        // Run();
    }

    #region method
    protected virtual void Initialize(){

    }

    protected virtual void Setup(){

    }
    #endregion
}
