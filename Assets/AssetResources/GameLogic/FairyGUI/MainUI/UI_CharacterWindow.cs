/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_CharacterWindow : GComponent
    {
        public GImage m_bg;
        public UI_CharacterFrame m_frame1;
        public UI_BagFrame m_frame2;
        public GButton m_exitBtn;
        public const string URL = "ui://8cf5amhlhx1a2b";

        public static UI_CharacterWindow CreateInstance()
        {
            return (UI_CharacterWindow)UIPackage.CreateObject("MainUI", "CharacterWindow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
            m_frame1 = (UI_CharacterFrame)GetChildAt(1);
            m_frame2 = (UI_BagFrame)GetChildAt(2);
            m_exitBtn = (GButton)GetChildAt(3);
        }
    }
}