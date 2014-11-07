using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MailManager;

namespace PairedComparison
{
    class DataManagerWithSendMail20131212 : DataManagerRandom
    {
        protected SendMail Manager = new SendMail();
        public DataManagerWithSendMail20131212()
        {
        }

        public DataManagerWithSendMail20131212(PictureBox LeftPictureBox, PictureBox RightPictureBox)
            : base(LeftPictureBox, RightPictureBox)
        {
        }

        public DataManagerWithSendMail20131212(PictureBox LeftPictureBox, PictureBox RightPictureBox, List<System.IO.DirectoryInfo> DirectoryInfo)
            : base(LeftPictureBox, RightPictureBox, DirectoryInfo)
        {
        }

        public override void vSetName(string strSentName)
        {
            base.vSetName(strSentName);
        }

        public override bool bNext(eSelect ieSelect)
        {
            string[] straImageReadFrom = System.IO.Path.GetDirectoryName(icilImageList.fiaFileName[DataList[iNowImageSetNo]][0].FullName).Split('\\');
            string strImageReadFrom = straImageReadFrom[straImageReadFrom.Length - 1];
            bool bEndCheck = true;

            //もしまだ組み合わせてない画像のパターンがあったらbEndCheckをFalseに。
            for (int i = 0; i < iNowImageAllCount; i++)
                for (int j = 0; j < iNowImageAllCount; j++)
                    if (bCheck[i][j] == false)
                    {
                        bEndCheck = false;
                        break;
                    }

            if (bEndCheck)
            {
                if (base.bNext(ieSelect))
                    return false;
                else
                {
                    return true;
                }
            }
            else
            {
                base.bNext(ieSelect);
                return true;
            }

        }
    }
}
