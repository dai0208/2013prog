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
    public partial class fmLoadFolder : Form
    {
        string[] straExtensionList = {"*.bmp", "*.png", "*.jpg"};
        List<System.IO.DirectoryInfo> ldiList = new List<System.IO.DirectoryInfo>();

        public fmLoadFolder()
        {
            InitializeComponent();
        }

        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            #region リストビューにD&Dされた時の挙動
            //ファイル名かフォルダ名がD&Dされたかどうか
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //D&Dされたファイル名（フォルダ名）を取得
                string[] strFolderList = (string[])e.Data.GetData(DataFormats.FileDrop);

                this.ldiSetList(strFolderList);
            }
            #endregion
        }

        private void lvMain_DragEnter(object sender, DragEventArgs e)
        {
            //D&D設定（ファイル、フォルダ以外のものを受け付けない）
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void fmLoadFolder_Load(object sender, EventArgs e)
        {
            //ウィンドウの初期位置
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            #region 選択されたアイテムを上へ移動
            if (lvMain.SelectedItems.Count == 0 || lvMain.SelectedItems[0].Index == 0)
                return;
            int iIndex = lvMain.SelectedItems[0].Index;

            ListViewItem lviWork = (ListViewItem)lvMain.Items[iIndex].Clone();
            lvMain.Items.RemoveAt(iIndex);
            lvMain.Items.Insert(iIndex - 1, lviWork);
            lvMain.Items[iIndex - 1].Selected = true;
            #endregion
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            #region 選択されたアイテムを下へ移動
            if (lvMain.SelectedItems.Count == 0 || lvMain.SelectedItems[0].Index == lvMain.Items.Count - 1)
                return;
            int iIndex = lvMain.SelectedItems[0].Index;

            ListViewItem lviWork = (ListViewItem)lvMain.Items[iIndex].Clone();
            lvMain.Items.RemoveAt(iIndex);
            lvMain.Items.Insert(iIndex + 1, lviWork);
            lvMain.Items[iIndex + 1].Selected = true;
            #endregion
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            #region 選択されたアイテムを消去
            if (lvMain.SelectedItems.Count == 0)
                return;
            int iIndex = lvMain.SelectedItems[0].Index;

            lvMain.Items[iIndex].Remove();
            if (lvMain.Items.Count == 0)
                return;
            if (iIndex == lvMain.Items.Count)
                iIndex = lvMain.Items.Count - 1;
            lvMain.Items[iIndex].Selected = true;
            #endregion
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region OKボタンが押された時
            ldiList.Clear();
            foreach (ListViewItem lviItem in lvMain.Items)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(lviItem.Text);
                if (di.Exists)
                    ldiList.Add(di);
            }
            ((fmStart)Owner).vSetParam(ldiList);
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(@"./List.txt");
                if (fi.Exists) fi.Delete();

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"./List.txt"))
                {
                    foreach (System.IO.DirectoryInfo di in ldiList)
                        sw.WriteLine(di.FullName);
                }
            }
            catch
            {
            }
            this.Close();
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((fmStart)Owner).vSetParam(null);
            this.Close();
        }

        public List<System.IO.DirectoryInfo> ldiSetList(string[] straSentList)
        {
            #region 与えられた配列からリストを作成
            List<System.IO.DirectoryInfo> ldiWork = new List<System.IO.DirectoryInfo>();

            foreach (string strFolderName in straSentList)
            {
                //得られたファイル名（フォルダ名）のディレクトリ情報
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strFolderName);

                //リストビューに追加するためのアイテム
                ListViewItem lviAdd;

                //指定された拡張子に適合するファイルの総数を保持する変数
                int iCount = 0;

                //もし得られたパスに対応するフォルダがなかったら（ファイル名とかだったら）戻る
                if (!di.Exists)
                {
                    MessageBox.Show(di.FullName + "\n画像セットリストにあるフォルダが存在しません。リストから削除します。読み込み画像の設定から確認してください。", "画像セットリストエラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                //アイテムの名前をパス名に
                lviAdd = new ListViewItem(strFolderName);

                //拡張子に適合するファイル数を計算
                foreach (string strExtension in straExtensionList)
                    iCount += di.GetFiles(strExtension).Length;

                //得られたファイル数を登録
                lviAdd.SubItems.Add(iCount.ToString());

                //アイテムを登録
                lvMain.Items.Add(lviAdd);

                //リストにディレクトリを登録
                ldiWork.Add(di);

                lvMain.Items[0].Selected = true;
            }
            return ldiWork;
            #endregion
        }

    }
}
