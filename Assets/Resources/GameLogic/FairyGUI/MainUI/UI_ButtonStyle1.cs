/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_ButtonStyle1 : GButton
    {
        public Controller m_c1;
        public const string URL = "ui://8cf5amhlhx1a2p";

        public static UI_ButtonStyle1 CreateInstance()
        {
            return (UI_ButtonStyle1)UIPackage.CreateObject("MainUI", "ButtonStyle1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(1);
        }
    }
}