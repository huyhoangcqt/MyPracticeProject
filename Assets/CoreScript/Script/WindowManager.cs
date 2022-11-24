using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public enum UILayer{
    Main,
    HUD,
    Popup,
    Tutorial,
}

public enum WindowID{
    MainWindow,

}

public class WindowManager : MonoSingleton<WindowManager> {
    private Dictionary<string, GComponent> gDict;
    public GComponent layerMain;
    public GComponent layerHud;
    public GComponent layerPopup;
    public GComponent layerTutorial;

    public void Init(){
        MainWindow mainWin = new MainWindow();
        mainWin.Show();
        //Add GRoot;
        // AddLayer(UILayer.Main, 1);
        // AddLayer(UILayer.HUD, 101);
        // AddLayer(UILayer.Popup, 201);
        // AddLayer(UILayer.Tutorial, 301);
    }

    private void AddLayer(UILayer layer, int sortingOrder){
        // GComponent Glayer = new GComponent();
        // Glayer.size = GRoot._inst.GetSiz
    }

    // public void OpenWindow(WindowID winID){
    //     BaseWindow win;
    //     if (winID == WindowID.MainWindow){
    //         if (!CheckExistWindow(winID)){
    //             win = CreateWindow(winID, UILayer.Main);
    //             win.Open();
    //         }
    //     }
    // }

    public bool CheckExistWindow(WindowID winID){
        return false;
    }

    // private BaseWindow CreateWindow(WindowID winID, UILayer layer){
    //     if (winID == WindowID.MainWindow){
    //         Transform parentLayer = UIUtils.Instance.GetUIRoot(layer);
    //         if (parentLayer == null){
    //             return null;
    //         }
    //         GameObject windowPanel = new GameObject("MainWindow");
    //         windowPanel.transform.SetParent(parentLayer);
    //         return windowPanel.AddComponent<MainWindow>();
    //     }
    //     return null;
    // }
}