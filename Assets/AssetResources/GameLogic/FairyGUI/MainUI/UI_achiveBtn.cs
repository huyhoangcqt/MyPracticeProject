/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_achiveBtn : GButton
    {
        public GImage m_outline;
        public GImage m_n2_sub;
        public const string URL = "ui://8cf5amhlvvu03x";

        public static UI_achiveBtn CreateInstance()
        {
            return (UI_achiveBtn)UIPackage.CreateObject("MainUI", "achiveBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_outline = (GImage)GetChildAt(0);
            m_n2_sub = (GImage)GetChildAt(3);
        }
    }
}