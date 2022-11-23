using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleLibrary {
    RefInOut = 1,
    Module2 = 2,
    DiamondInheritance = 3,
}

public class Executor : MonoBehaviour
{
    public ModuleLibrary module;
    
    public void Execute(){
        Debug.Log("Execute module: " + module.ToString());
        switch (module){
            case ModuleLibrary.RefInOut:
                RefInOutModuleCtrl.Instance.Handle();
                break;
            case ModuleLibrary.DiamondInheritance:
                DiamondInheritanceCtrl.Instance.Handle();
                break;
            default:
                break;
        }
    }
}
