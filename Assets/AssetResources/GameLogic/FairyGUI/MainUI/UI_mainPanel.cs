/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MainUI
{
    public partial class UI_mainPanel : GComponent
    {
        public GList m_list;
        public UI_mapBtn m_mapBtn;
        public GLoader m_avatar;
        public GGroup m_avatar_2;
        public GGroup m_healthBar;
        public GGroup m_manaBar;
        public const string URL = "ui://8cf5amhlhskg2y";

        public static UI_mainPanel CreateInstance()
        {
            return (UI_mainPanel)UIPackage.CreateObject("MainUI", "mainPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(2);
            m_mapBtn = (UI_mapBtn)GetChildAt(3);
            m_avatar = (GLoader)GetChildAt(5);
            m_avatar_2 = (GGroup)GetChildAt(7);
            m_healthBar = (GGroup)GetChildAt(10);
            m_manaBar = (GGroup)GetChildAt(13);
        }
    }
}