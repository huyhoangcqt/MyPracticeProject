/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_equipmentBtn : GButton
    {
        public GImage m_outline;
        public const string URL = "ui://8cf5amhlvvu03y";

        public static UI_equipmentBtn CreateInstance()
        {
            return (UI_equipmentBtn)UIPackage.CreateObject("MainUI", "equipmentBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_outline = (GImage)GetChildAt(0);
        }
    }
}