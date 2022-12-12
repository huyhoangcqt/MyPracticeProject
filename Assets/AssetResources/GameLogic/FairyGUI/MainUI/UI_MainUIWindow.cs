/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_MainUIWindow : GComponent
    {
        public UI_mainPanel m_mainPanel;
        public const string URL = "ui://8cf5amhldl4060";

        public static UI_MainUIWindow CreateInstance()
        {
            return (UI_MainUIWindow)UIPackage.CreateObject("MainUI", "MainUIWindow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mainPanel = (UI_mainPanel)GetChildAt(0);
        }
    }
}