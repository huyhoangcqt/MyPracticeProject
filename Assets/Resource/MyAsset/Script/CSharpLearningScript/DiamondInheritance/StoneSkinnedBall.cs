using System;
using UnityEngine;

//FIXME Can't inherited from multiple base class
// public class StoneSkinnedBall : Ball, Stone{  
//     public override void ShowInfo(){
//         base.ShowInfo();
//     }
// }

public class StoneSkinnedBall : Ball, IStone{  
    public override void ShowInfo(){
        base.ShowInfo();
        Debug.Log("This is StoneSkinnedBall");
    }

    public void ShowDescription(){
        Debug.Log("StoneSkinnedBall is a ball which has its apperance look like a stone!");
    }
}