using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using MainUI;

public enum UILayer{
    Main,
    HUD,
    Popup,
    Tutorial,
}

public enum WindowID{
    MainWindow,

}

public class WinMgr : MonoSingleton<WinMgr> {
    public const int DesignResolutionW = 1280;
    public const int DesignResolutionH = 720;

    private Dictionary<string, GComponent> gDict;
    public GComponent layerMain;
    public GComponent layerHud;
    public GComponent layerPopup;
    public GComponent layerTutorial;

    public void Init(){
        // FGUIHelper.BindAll();
        GRoot.inst.SetContentScaleFactor(DesignResolutionW, DesignResolutionH, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);

        // Add GRoot;
        AddLayer(UILayer.Main, 1);
        AddLayer(UILayer.HUD, 101);
        AddLayer(UILayer.Popup, 201);
        AddLayer(UILayer.Tutorial, 301);

        // GComponent aLayer = new GComponent();
        // aLayer.parent = gameObject as GObject;

        // MainWindow mainWin = new MainWindow();
        // mainWin.Show();

        // GObject root = GRoot.inst;
        // DisplayObject displayObject = root.displayObject;
        // GameObject go = displayObject.gameObject;
        // go.transform.parent = this.transform;

        // GComponent rootComp = root as GComponent;
        // rootComp.AddChild(new UI_MainUIWindow());

        // GComponent window = new UI_MainUIWindow();
        // window.parent = root as GComponent;

        UIPackage.AddPackage("GameLogic/FairyGUI/MainUI");

        // CreateObject();
        // GComponent mainUI = UI_MainUIWindow.CreateInstance();
        var _mainWindow = new MainWindow();
        _mainWindow.Show();
    }


    private void newBindFGUICode()
    {
        // UIObjectFactory.SetDefaultPackageItemExtension(GetComponent);
    }

    private void AddLayer(UILayer layerType, int sortingOrder){
        var layer = new GComponent();
        layer.fairyBatching = true;
        layer.sortingOrder = sortingOrder;
        layer.SetSize(GRoot.inst.width, GRoot.inst.height);
        // layer.AddRelation(GRoot.inst, RelationType.Size);
        layer.MakeFullScreen();
        GRoot.inst.AddChild(layer);
        switch (layerType) {
            case UILayer.Main:
                layerMain = layer;
                break;
            case UILayer.HUD:
                layerHud = layer;
                break;
            case UILayer.Popup:
                layerPopup = layer;
                break;
            case UILayer.Tutorial:
                layerTutorial = layer;
                break;
            default:
                return;
        }
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