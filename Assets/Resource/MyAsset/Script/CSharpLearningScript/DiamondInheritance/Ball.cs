using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : IThing
{
    public virtual void ShowInfo(){
        // base.ShowInfo();
        Debug.Log("This is a ball");    
    }
}
