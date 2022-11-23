/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_BagBoxDown : GComponent
    {
        public GImage m_bg;
        public GImage m_frame;
        public const string URL = "ui://8cf5amhlhx1a2s";

        public static UI_BagBoxDown CreateInstance()
        {
            return (UI_BagBoxDown)UIPackage.CreateObject("MainUI", "BagBoxDown");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
            m_frame = (GImage)GetChildAt(1);
        }
    }
}