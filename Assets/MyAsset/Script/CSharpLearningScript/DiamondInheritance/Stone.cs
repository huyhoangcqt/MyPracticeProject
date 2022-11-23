using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : IThing
{
    public void ShowInfo()
    {
        // base.ShowInfo();
        Debug.Log("This is a stone");        
    }
}

// public class Stone : Thing
// {
//     public override void ShowInfo()
//     {
//         // base.ShowInfo();
//         Debug.Log("This is a stone");        
//     }
// }
