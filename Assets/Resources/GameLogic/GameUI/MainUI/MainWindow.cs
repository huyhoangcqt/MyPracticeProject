using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using MainUI;

public class MainWindow : BaseWindow
{
    UI_MainUIWindow win;

    public void Show(){
        win = new UI_MainUIWindow();
        GRoot.inst.AddChild(win);
    }
}
