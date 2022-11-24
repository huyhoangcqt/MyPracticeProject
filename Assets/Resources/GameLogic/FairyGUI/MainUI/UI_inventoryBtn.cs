/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_inventoryBtn : GButton
    {
        public GImage m_outline;
        public const string URL = "ui://8cf5amhlvvu03v";

        public static UI_inventoryBtn CreateInstance()
        {
            return (UI_inventoryBtn)UIPackage.CreateObject("MainUI", "inventoryBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_outline = (GImage)GetChildAt(0);
        }
    }
}