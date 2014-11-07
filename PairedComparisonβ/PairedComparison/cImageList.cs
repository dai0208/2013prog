using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PairedComparison
{
    public class cImageList
    {
        List<System.IO.DirectoryInfo> ldiList = new List<System.IO.DirectoryInfo>();

        /// <summary>
        /// 画像を保持する配列です
        /// </summary>
        Bitmap[][] bmpaList;

        /// <summary>
        /// ファイル情報を保持する配列です。
        /// </summary>
        System.IO.FileInfo[][] fiList;

        #region コンストラクタ
        protected cImageList()
        {

        }

        public cImageList(List<System.IO.DirectoryInfo> ldiSent)
        {
            string[] straExtensionList = { "*.bmp", "*.png", "*.jpg" };
            vInit(ldiSent, straExtensionList);
        }

        public cImageList(List<System.IO.DirectoryInfo> ldiSent, string[] straExtensionList)
        {
            vInit(ldiSent, straExtensionList);
        }
        #endregion

        /// <summary>
        /// 与えられたリストと拡張子フィルタを使って初期化をします
        /// </summary>
        /// <param name="ldiSent">ディレクトリ情報のリスト</param>
        /// <param name="straExtensionList">拡張子フィルタ</param>
        private void vInit(List<System.IO.DirectoryInfo> ldiSent, string[] straExtensionList)
        {
            #region 与えられたリストと拡張子フィルタを使って初期化を行う
            List<Bitmap> lbmpList = new List<Bitmap>();
            List<System.IO.FileInfo> lfiList = new List<System.IO.FileInfo>();

            bmpaList = new Bitmap[ldiSent.Count][];
            fiList = new System.IO.FileInfo[ldiSent.Count][];
            int iIndex = 0;

            foreach (System.IO.DirectoryInfo di in ldiSent)
            {
                ldiList.Add(di);

                //サポートする拡張子を読み込み
                foreach (string strExtension in straExtensionList)
                {
                    foreach (System.IO.FileInfo fi in di.GetFiles(strExtension))
                    {
                        lbmpList.Add(new Bitmap(fi.FullName));
                        lfiList.Add(fi);
                    }
                }

                //リストからBitmap配列とFileInfo配列に転送
                bmpaList[iIndex] = new Bitmap[lbmpList.Count];
                fiList[iIndex] = new System.IO.FileInfo[lfiList.Count];
                for (int i = 0; i < lbmpList.Count; i++)
                {
                    bmpaList[iIndex][i] = new Bitmap(lbmpList[i]);
                    fiList[iIndex][i] = lfiList[i];
                }
                iIndex++;
                lbmpList.Clear();
                lfiList.Clear();
            }
            #endregion
        }

        #region プロパティ
        /// <summary>
        /// 画像を取得します。
        /// </summary>
        public Bitmap[][] bmpaImage
        {
            get { return bmpaList; }
        }

        /// <summary>
        /// ファイル情報を取得します。
        /// </summary>
        public System.IO.FileInfo[][] fiaFileName
        {
            get { return fiList; }
        }
        #endregion
    }
}
