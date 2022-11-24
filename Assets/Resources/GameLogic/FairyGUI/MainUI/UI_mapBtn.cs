/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_mapBtn : GButton
    {
        public GImage m_outline;
        public const string URL = "ui://8cf5amhlvvu03w";

        public static UI_mapBtn CreateInstance()
        {
            return (UI_mapBtn)UIPackage.CreateObject("MainUI", "mapBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_outline = (GImage)GetChildAt(0);
        }
    }
}