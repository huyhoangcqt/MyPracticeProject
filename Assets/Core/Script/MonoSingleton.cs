using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T inst 
    {
        get {
            if (_instance == null){
                GameObject mono = GameObject.Find("MonoSingleton");
                if (mono != null) {
                    _instance = mono.GetComponent<T>();
                    if (_instance == null){
                        mono.AddComponent<T>();
                        _instance = mono.GetComponent<T>();
                        return _instance;
                    }
                    return _instance;
                }
                
                GameObject go = new GameObject("MonoSingleton");
                go.AddComponent<T>();
                _instance = go.GetComponent<T>();
                return _instance;
            };
            return _instance;
        }
    }

    private void Awake() {
        _instance = this as T;
    }
}
