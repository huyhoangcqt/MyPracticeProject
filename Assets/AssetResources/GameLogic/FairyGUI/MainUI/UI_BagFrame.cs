/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_BagFrame : GComponent
    {
        public UI_BagBoxUp m_boxUp;
        public UI_BagBoxDown m_boxDown;
        public const string URL = "ui://8cf5amhlhx1a2r";

        public static UI_BagFrame CreateInstance()
        {
            return (UI_BagFrame)UIPackage.CreateObject("MainUI", "BagFrame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_boxUp = (UI_BagBoxUp)GetChildAt(0);
            m_boxDown = (UI_BagBoxDown)GetChildAt(1);
        }
    }
}