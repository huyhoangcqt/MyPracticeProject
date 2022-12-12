/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_BagBoxUp : GComponent
    {
        public GImage m_bg;
        public GImage m_frame;
        public GList m_switchTabButton;
        public const string URL = "ui://8cf5amhlhx1a2k";

        public static UI_BagBoxUp CreateInstance()
        {
            return (UI_BagBoxUp)UIPackage.CreateObject("MainUI", "BagBoxUp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
            m_frame = (GImage)GetChildAt(1);
            m_switchTabButton = (GList)GetChildAt(38);
        }
    }
}