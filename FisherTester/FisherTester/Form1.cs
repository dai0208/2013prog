using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FisherClassifier;
using MatrixVector;

namespace FisherTester
{
    public partial class Form1 : Form
    {
        string inPath;
        string[] cls1Path;
        string[] cls2Path;
        Classifier tester;

        public Form1()
        {
            InitializeComponent();
        }

        private void SaveAscFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileNameWithoutExtension(inPath) + "_Converted_Fisher_" + WeightValueBox.Text + ".asc";
            sfd.Filter = "ascファイル(*.asc)|*.asc|すべてのファイル(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                tester.saveConvertedVector(sfd.FileName);
            }
        }

        #region ドラッグ＆ドロップ動作
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            listBox1.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            inPath = files[0];

            listBox1.Items.Add(inPath);
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            listBox2.Items.Clear();
            /* ディレクトリ内のascファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*.asc", SearchOption.AllDirectories);
                listBox2.Items.AddRange(files);
            }
            //string[] files = listBox2.Items.Cast<string>().ToArray();
        }

        private void listBox3_DragDrop(object sender, DragEventArgs e)
        {
            listBox3.Items.Clear();
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*.asc", SearchOption.AllDirectories);
                listBox3.Items.AddRange(files);
            }

            //string[] files = listBox3.Items.Cast<string>().ToArray();
        }
        #endregion

        #region コントロール動作
        private void runBtn_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();

            cls1Path = listBox2.Items.Cast<string>().ToArray();

            cls2Path = listBox3.Items.Cast<string>().ToArray();

            tester = new Classifier(manager.ArrayToVector(manager.ReadTextFile(inPath)), manager.ArrayToMatrix(manager.ReadTextFileList(cls1Path)), manager.ArrayToMatrix(manager.ReadTextFileList(cls2Path)));

            tester.DoFisherClassifier();

            MessageBox.Show("重みベクトルの導出が完了しました。");

            TransformBtn.Enabled = true;
        }

        private void TransformBtn_Click(object sender, EventArgs e)
        {
            tester.DoImpressionTransfer(int.Parse(WeightValueBox.Text));

            string DirName = Path.GetDirectoryName(inPath) + "\\CONVERTED_BY_FISHER\\";
            if (System.IO.Directory.Exists(DirName)) { }
            else
            {
                MessageBox.Show("フォルダが存在しないので作成します。");
                System.IO.Directory.CreateDirectory(DirName);
            }

            string FilePath = DirName + Path.GetFileName(inPath);
            tester.saveConvertedVector(FilePath);

            MessageBox.Show("変換完了！パラメータを保存しました。");
        }

        private void WeightController_Scroll(object sender, EventArgs e)
        {
            WeightValueBox.Text = WeightController.Value.ToString();
        }
        #endregion

    }
}
