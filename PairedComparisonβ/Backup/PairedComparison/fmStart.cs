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
    public partial class fmStart : Form
    {
        string strName = "";
        List<System.IO.DirectoryInfo> ldiList = new List<System.IO.DirectoryInfo>();
        fmLoadFolder ifmLoadFolder = new fmLoadFolder();
        string strSaveFolderName = "";

        public fmStart()
        {
            InitializeComponent();
        }

        private void fmStart_Load(object sender, EventArgs e)
        {
            //注意文の読み込み
            try
            {
                using (System.IO.StreamReader srLoad = new System.IO.StreamReader(@"./注意.txt"))
                {
                    string strAttention = srLoad.ReadToEnd();
                    tbxInfo.Text = strAttention;
                }
            }
            catch { };

            try
            {
                using (System.IO.StreamReader srList = new System.IO.StreamReader(@"./List.txt"))
                {
                    string strListData = srList.ReadToEnd();
                    strListData = strListData.Replace("\r\n", "\n");
                    strListData = strListData.TrimEnd('\n');
                    ldiList = ifmLoadFolder.ldiSetList(strListData.Split('\n'));
                }
            }
            catch { }

            try
            {
                using (System.IO.StreamReader srLoad = new System.IO.StreamReader(@"./SaveFolder.txt"))
                {
                    strSaveFolderName = srLoad.ReadToEnd();
                    strSaveFolderName = strSaveFolderName.TrimEnd('\n');
                    ((fmBack)Owner).vSetSaveFolder(strSaveFolderName);
                    
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strSaveFolderName);
                    if (!di.Exists)
                        MessageBox.Show("セーブ先のフォルダがありません。設定を見直してください。\nフォルダが存在しない場合は自動的に作成されます。", "セーブ先フォルダがありません", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch {
                System.Windows.Forms.MessageBox.Show("セーブフォルダを選んでください。", "セーブ先を選択", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tsmiSelectSaveFolder_Click(sender, e);
            }

            this.tbxSaveFolderName.Text = strSaveFolderName;

            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

        }

        public void vSetParam(List<System.IO.DirectoryInfo> ldiSent)
        {
            ldiList = ldiSent;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tbxName.Text == "名前を入力してください。")
                return;
            strName = tbxName.Text;
            ((fmBack)Owner).vSetParam(strName, ldiList);
            this.Close();
        }

        private void tsmiLoadFolder_Click(object sender, EventArgs e)
        {
            ifmLoadFolder.Owner = this;
            ifmLoadFolder.ShowDialog();
        }

        private void tsmiSelectSaveFolder_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbdSelectFolder = new FolderBrowserDialog();
            if (strSaveFolderName != "")
                fbdSelectFolder.SelectedPath = strSaveFolderName;

            if (fbdSelectFolder.ShowDialog() == DialogResult.OK)
            {
                strSaveFolderName = fbdSelectFolder.SelectedPath;
                ((fmBack)Owner).vSetSaveFolder(strSaveFolderName);
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"./SaveFolder.txt"))
                    {
                        sw.Write(strSaveFolderName);
                    }
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("保存先を記録できませんでした。次回再度選択をするかもう一度ダイアログを開いてください。\n今回の実験はそのまま行うことができます。", "記録エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            tbxSaveFolderName.Text = strSaveFolderName;
        }
    }
}
