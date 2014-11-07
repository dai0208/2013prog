using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace ReNameKun
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lstFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lstFile_DragDrop(object sender, DragEventArgs e)
        {
            lstFile.Items.Clear();
            /* ディレクトリ内の全てのファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*", System.IO.SearchOption.AllDirectories);
                lstFile.Items.AddRange(files);
            }
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            string[] fileList = lstFile.Items.Cast<string>().ToArray();
            string[] newfileList = new string[fileList.Length];
            for (int i = 0; i < fileList.Length; i++)
            {
                if (rdbAttachHead.Checked)
                {
                    newfileList[i] = tbxAttachInfo.Text + Path.GetFileName(fileList[i]);
                }
                else if (rdbAttachTale.Checked)
                {
                    newfileList[i] = Path.GetFileNameWithoutExtension(fileList[i]) + tbxAttachInfo.Text + Path.GetExtension(fileList[i]);
                }
                else if (rdbDelete.Checked)
                {
                    if (SearchPatturn(fileList[i],tbxAttachInfo.Text,0)) newfileList[i] = Path.GetFileName(fileList[i]).Replace(tbxAttachInfo.Text, "");
                    else return;
                }
                else if (rdbPermutation.Checked)
                {
                    if (SearchPatturn(fileList[i], tbxAttachInfo.Text.Split(' ')[0], 0)) newfileList[i] = Path.GetFileName(fileList[i]).Replace(tbxAttachInfo.Text.Split(' ')[0], tbxAttachInfo.Text.Split(' ')[1]);
                    else return;
                }

                else return;

                FileSystem.RenameFile(@fileList[i], @newfileList[i]);
            }
            /* ファイルリストのクリア */
            lstFile.Items.Clear();
        }

        private bool SearchPatturn(string strTarget, string strScn, int key)
        {
            /* 目的文字の探索 */
            if (strTarget.Length < key)
            {
                if (strTarget.IndexOf(strScn) != -1)
                {
                    int iFind = strTarget.IndexOf(strScn,key);
                    SearchPatturn(strTarget, strScn, iFind + 1);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void rdbAttachHead_CheckedChanged(object sender, EventArgs e)
        {
            using (System.IO.StreamReader srLoad = new System.IO.StreamReader(@"./AttachHead.txt", System.Text.Encoding.GetEncoding("shift_jis")))
            {
                string strAttention = srLoad.ReadToEnd();
                tbxMessage.Text = strAttention;
            }
        }

        private void rdbAttachTale_CheckedChanged(object sender, EventArgs e)
        {
            using (System.IO.StreamReader srLoad = new System.IO.StreamReader(@"./AttachTale.txt", System.Text.Encoding.GetEncoding("shift_jis")))
            {
                string strAttention = srLoad.ReadToEnd();
                tbxMessage.Text = strAttention;
            }

        }
    }
}
