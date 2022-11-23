using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIManager : BaseManager
{
    protected override void Initialize()
    {
        base.Initialize();
        LoadUI();
    }

    protected override void Setup()
    {
        base.Setup();
    }

    #region  method
    private void LoadUI(){
        // UIPackage.AddPackage("UI/FairyGUI/mainui/MainUI");
        WindowManager.Singleton.OpenWindow(WindowID.MainWindow);
    }
    #endregion
}
