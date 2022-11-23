
using System;
using UnityEngine;

public class DiamondInheritanceCtrl : TestSingleton<DiamondInheritanceCtrl>{

    public void Handle(){
        StoneSkinnedBall stoneBall = new StoneSkinnedBall();
        stoneBall.ShowInfo();
    }
}