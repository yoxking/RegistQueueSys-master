using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.Framework.Utility
{
    public class TicketButton
    {
        private int ButtonLeft;

        public int iButtonLeft
        {
            get { return ButtonLeft; }
            set { ButtonLeft = value; }
        }
        private int ButtonTop;

        public int iButtonTop
        {
            get { return ButtonTop; }
            set { ButtonTop = value; }
        }
        private int ButtonWidth;

        public int iButtonWidth
        {
            get { return ButtonWidth; }
            set { ButtonWidth = value; }
        }
        private int ButtonHeight;

        public int iButtonHeight
        {
            get { return ButtonHeight; }
            set { ButtonHeight = value; }
        }
        private string ButtonBgImage1;

        public string sButtonBgImage1
        {
            get { return ButtonBgImage1; }
            set { ButtonBgImage1 = value; }
        }
        private string ButtonBgImage2;

        public string sButtonBgImage2
        {
            get { return ButtonBgImage2; }
            set { ButtonBgImage2 = value; }
        }
        private string ButtonBgImage3;

        public string sButtonBgImage3
        {
            get { return ButtonBgImage3; }
            set { ButtonBgImage3 = value; }
        }
        private string ButtonBgColor;

        public string sButtonBgColor
        {
            get { return ButtonBgColor; }
            set { ButtonBgColor = value; }
        }
        private int TitleFtSize;

        public int iTitleFtSize
        {
            get { return TitleFtSize; }
            set { TitleFtSize = value; }
        }
        private string TitleFtFamily;

        public string sTitleFtFamily
        {
            get { return TitleFtFamily; }
            set { TitleFtFamily = value; }
        }

        private string TitleFtStyle;

        public string sTitleFtStyle
        {
            get { return TitleFtStyle; }
            set { TitleFtStyle = value; }
        }
        
        private string TitleFtColor;

        public string sTitleFtColor
        {
            get { return TitleFtColor; }
            set { TitleFtColor = value; }
        }
        private int SubtitleFtSize;

        public int iSubtitleFtSize
        {
            get { return SubtitleFtSize; }
            set { SubtitleFtSize = value; }
        }
        private string SubtitleFtFamily;

        public string sSubtitleFtFamily
        {
            get { return SubtitleFtFamily; }
            set { SubtitleFtFamily = value; }
        }
        private string SubtitleFtStyle;

        public string sSubtitleFtStyle
        {
            get { return SubtitleFtStyle; }
            set { SubtitleFtStyle = value; }
        }

        private string SubtitleFtColor;

        public string sSubtitleFtColor
        {
            get { return SubtitleFtColor; }
            set { SubtitleFtColor = value; }
        }
        private bool IsShowTitle;

        public bool bIsShowTitle
        {
            get { return IsShowTitle; }
            set { IsShowTitle = value; }
        }
        private bool IsShowSubtitle;

        public bool bIsShowSubtitle
        {
            get { return IsShowSubtitle; }
            set { IsShowSubtitle = value; }
        }

    }
}
