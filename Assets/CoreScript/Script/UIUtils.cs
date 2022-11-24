using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UIRoot{
    public Transform main;
    public Transform layer2;
    public Transform popup;
    public Transform hud;
}

public class UIUtils : Singleton<UIUtils>
{
    UIRoot root;
    private static bool isInitRoot = false;

    public Transform GetUIRoot(UILayer layer){
        if (!isInitRoot){
            InitRoot();
            isInitRoot = true;
        }
        switch (layer){
            case UILayer.Main:
                return root.main;
            case UILayer.Popup:
                return root.popup;
            case UILayer.HUD:
                return root.hud;
            default:
                return null;
        }
    }

    private void InitRoot(){
        root.main = GameObject.Find("FairyGUIPanel/MainPanel").transform;
        root.layer2 = GameObject.Find("FairyGUIPanel/Layer2").transform;
        root.popup = GameObject.Find("FairyGUIPanel/Popup").transform;
        root.hud = GameObject.Find("FairyGUIPanel/HUD").transform;
    }
}
