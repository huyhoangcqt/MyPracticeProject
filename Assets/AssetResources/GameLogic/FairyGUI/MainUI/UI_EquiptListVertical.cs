/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_EquiptListVertical : GComponent
    {
        public GList m_list;
        public const string URL = "ui://8cf5amhlhx1a2f";

        public static UI_EquiptListVertical CreateInstance()
        {
            return (UI_EquiptListVertical)UIPackage.CreateObject("MainUI", "EquiptListVertical");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(0);
        }
    }
}