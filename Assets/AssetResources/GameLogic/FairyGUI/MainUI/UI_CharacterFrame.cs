/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_CharacterFrame : GComponent
    {
        public GImage m_bg;
        public GImage m_Frame;
        public GImage m_character;
        public UI_EquiptListVertical m_ListL;
        public UI_EquiptListVertical m_ListR;
        public GComponent m_ListB;
        public const string URL = "ui://8cf5amhlhx1a2i";

        public static UI_CharacterFrame CreateInstance()
        {
            return (UI_CharacterFrame)UIPackage.CreateObject("MainUI", "CharacterFrame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
            m_Frame = (GImage)GetChildAt(1);
            m_character = (GImage)GetChildAt(2);
            m_ListL = (UI_EquiptListVertical)GetChildAt(3);
            m_ListR = (UI_EquiptListVertical)GetChildAt(4);
            m_ListB = (GComponent)GetChildAt(5);
        }
    }
}