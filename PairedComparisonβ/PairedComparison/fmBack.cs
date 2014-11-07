using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PairedComparison
{
    public partial class fmBack : Form
    {
        string strName = "";
        List<System.IO.DirectoryInfo> ldiList = new List<System.IO.DirectoryInfo>();
        DataManagerRandom icdmManager;
        string strSaveFolderName = "";
        string strMessage = "";

        bool bEnd = false;

        public fmBack()
        {
            InitializeComponent();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            pbxLeft.Left = (Screen.PrimaryScreen.Bounds.Width - pbxLeft.Width) / 2 - 200;
            pbxLeft.Top = (Screen.PrimaryScreen.Bounds.Height - pbxLeft.Height) / 2;

            pbxRight.Left = (Screen.PrimaryScreen.Bounds.Width - pbxRight.Width) / 2 + 200;
            pbxRight.Top = (Screen.PrimaryScreen.Bounds.Height - pbxRight.Height) / 2; 

            //名前入力ウィンドウを表示
            fmStart ifmStart = new fmStart();
            ifmStart.Owner = this;
            ifmStart.ShowDialog();

            //名前入力ウィンドウがALT+F4とかで閉じられると名前が入力されていない状態になるので終了する
            if (strName == "")
                this.Close();
            else
            {
                //ちゃんと名前が入力されてたらこっち。

                //PictureBoxと画像のディレクトリが保存されたリストをマネージャーに渡す。
                icdmManager = new DataManagerRandom(pbxLeft, pbxRight, ldiList);
                //保存先のフォルダと名前を指定
                icdmManager.SaveFolderName = strSaveFolderName + "\\" + strName;
                //被験者の名前を設定
                icdmManager.vSetName(strName);

                lblMessage.Text = "画像はマウスのクリック、またはキーボードの左右キーで選択することができます。\n※評価内容を読み飛ばしてしまったり忘れてしまった場合は「Ｒ」キーで再表示できます。";
                lblMessage.TextAlign = ContentAlignment.MiddleCenter;
                lblMessage.Left = (Screen.PrimaryScreen.Bounds.Width - lblMessage.Width) / 2;
                lblMessage.Top = (Screen.PrimaryScreen.Bounds.Height - pbxLeft.Height) / 2 - 100;

                lblMessage2.TextAlign = ContentAlignment.MiddleCenter;
                lblMessage2.Left = (Screen.PrimaryScreen.Bounds.Width - lblMessage.Width) / 2;
                lblMessage2.Top = (Screen.PrimaryScreen.Bounds.Height - pbxLeft.Height) / 2 - 150;

                /*
                try
                {
                    using (System.IO.StreamReader srList = new System.IO.StreamReader(@"./画面表示.txt"))
                    {
                        strMessage = srList.ReadToEnd();
                    }
                    MessageBox.Show(strMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch { }
                */

                //0番目の画像セットからスタート
                icdmManager.vStart(0);
            }
        }

        public void vSetParam(string strSentName, List<System.IO.DirectoryInfo>ldiSentList)
        {
            strName = strSentName;
            ldiList = ldiSentList;
        }

        private void pbxLeft_Click(object sender, EventArgs e)
        {
            if (!icdmManager.bNext(eSelect.Left)) 
            {
                bEnd = true;
                Application.Exit();
            }
        }

        private void pbxRight_Click(object sender, EventArgs e)
        {
            if (!icdmManager.bNext(eSelect.Right))
            {
                bEnd = true;
                Application.Exit();
            }
        }

        public void vSetSaveFolder(string strSentSaveFolderName)
        {
           strSaveFolderName = strSentSaveFolderName;
        }

        private void fmBack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                pbxRight_Click(sender, e);
            else if (e.KeyCode == Keys.Left)
                pbxLeft_Click(sender, e);
            else if (e.KeyCode == Keys.R)
                ShowMessage();
            else if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void ShowMessage()
        {
            MessageBox.Show(icdmManager.tmpMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fmBack_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (strName != "" && !bEnd )
                if (MessageBox.Show("実験を終了しますか？\n終了するとこの画像セットのデータは破棄されます。", "終了確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
        }
    }
}
