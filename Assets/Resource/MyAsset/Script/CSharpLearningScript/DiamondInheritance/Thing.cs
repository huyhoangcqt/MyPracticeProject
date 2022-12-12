using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Thing
{
    private string desc;
    public abstract void ShowInfo();

    public void Sleep() 
    {
        Debug.Log("zzZz");
    }
}
