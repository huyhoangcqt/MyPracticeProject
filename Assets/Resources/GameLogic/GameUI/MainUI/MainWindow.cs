using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using MainUI;

namespace MainUI{
    public partial class UI_MainUIWindow : GComponent
    {
        public static UI_MainUIWindow CreateIstanceFromURL(){
            return (UI_MainUIWindow)UIPackage.CreateObjectFromURL(URL);
        }
    }
}

public class MainWindow : Window
{
    UI_MainUIWindow win;

    // public void Show(){
    //     win = new UI_MainUIWindow();
    //     GRoot.inst.AddChild(win);
    // }

    public MainWindow()
    {
    }

    protected override void OnInit()
    {
        // win = UI_MainUIWindow.CreateInstance();
        win = UI_MainUIWindow.CreateIstanceFromURL();
        if (win == null) {
            Debug.Log("Win null");
            return;
        }
        this.contentPane = win;
        this.Center();
        this.modal = true;
        InitView();
    }

    private void InitView(){
        GList list = win.m_mainPanel.m_list as GList;
        // GButton bagBtn = list.GetChild("inventoryBtn") as GButton;
        GButton bagBtn = list.GetChildAt(1) as GButton;
        bagBtn.onClick.Add(OpenBagButton);
    }

    private void OpenBagButton(){
        new BagWindow().Show();
    }
}
