using System;
using UnityEngine;

public enum UILayer{
    Main,
    Layer2,
    Popup,
    HUD,
}

public enum WindowID{
    MainWindow,

}

public class WindowManager : MonoSingleton<WindowManager> {

    public void OpenWindow(WindowID winID){
        BaseWindow win;
        if (winID == WindowID.MainWindow){
            if (!CheckExistWindow(winID)){
                win = CreateWindow(winID, UILayer.Main);
                win.Open();
            }
        }
    }

    public bool CheckExistWindow(WindowID winID){
        return false;
    }

    private BaseWindow CreateWindow(WindowID winID, UILayer layer){
        if (winID == WindowID.MainWindow){
            Transform parentLayer = UIUtils.Instance.GetUIRoot(layer);
            if (parentLayer == null){
                return null;
            }
            GameObject windowPanel = new GameObject("MainWindow");
            windowPanel.transform.SetParent(parentLayer);
            return windowPanel.AddComponent<MainWindow>();
        }
        return null;
    }
}