using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DoPCA;
using MatrixVector;

namespace SVMClassifierWithEyeMotion
{
    public partial class Form1 : Form
    {
        private myClassifier myclassifier;
        private PCABaseManager PCAHighNeu;
        private PCABaseManager PCANeuLow;

        public Form1(PCABaseManager PCAHighNeu, PCABaseManager PCANeuLow)
        {
            this.PCAHighNeu = PCAHighNeu;
            this.PCANeuLow = PCANeuLow;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void learning_btn_Click(object sender, EventArgs e)
        {
            List<string> PCAHN = new List<string>();
            List<string> PCANL = new List<string>();

            PCAHN.AddRange(lbxHighNeu.Items.Cast<string>().ToArray());
            PCANL.AddRange(lbxNeuLow.Items.Cast<string>().ToArray());

            this.PCAHighNeu.FileList = PCAHN;
            PCAData HNPCAData = PCAHighNeu.GetPCAData();

            this.PCANeuLow.FileList = PCANL;
            PCAData NLPCAData = PCANeuLow.GetPCAData();

            myclassifier = new myClassifier(HNPCAData, NLPCAData);
            myclassifier.Learning((int)nmcSizeH.Value, (int)nmcSizeN.Value);
        }

        private void classifier_btn_Click(object sender, EventArgs e)
        {
            int[] flag = new int[lbxTestDatas.Items.Count];
            Vector[] vct = myclassifier.GetVectorFromBitmap(lbxTestDatas.Items.Cast<string>().ToArray());
            for (int i = 0; i < vct.Length; i++)
            {
                flag[i] = myclassifier.PlotandGetResult(vct[i]);
            }
            WriteResultToCSV wrc = new WriteResultToCSV(lbxTestDatas.Items.Cast<string>().ToArray(), flag);
            wrc.WriteToCSV();
        }

        #region ドラッグドロップ

        private void TestDataImg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void lbxHighNeu_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lbxNeuLow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lbxNeuLow_DragDrop(object sender, DragEventArgs e)
        {
            lbxNeuLow.Items.Clear();
            /* ディレクトリ内のascファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                lbxNeuLow.Items.AddRange(files);
            }
        }

        private void lbxHighNeu_DragDrop(object sender, DragEventArgs e)
        {
            lbxHighNeu.Items.Clear();
            /* ディレクトリ内のascファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                lbxHighNeu.Items.AddRange(files);
            }
        }

        private void lbxTestDatas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lbxTestDatas_DragDrop(object sender, DragEventArgs e)
        {
            lbxTestDatas.Items.Clear();
            /* ディレクトリ内のascファイルを探索して追加 */
            foreach (string tempFilePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string[] files = Directory.GetFiles(tempFilePath, "*.*", SearchOption.AllDirectories);
                lbxTestDatas.Items.AddRange(files);
            }
        }

        #endregion

        
    }
}
