using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;
using PointFormat;

namespace MutualConversion
{
    /// <summary>
    /// 少数のパラメータで再構築したデータのエラー指数を計算するクラスです。
    /// ひとつのインスタンスで複数のエラー指数を保持することができます。
    /// （１つの車で、固有ベクトルの数を変えた場合などに便利）
    /// </summary>
    public class cErrorIndex
    {
        #region プライベート変数
        /// <summary>
        /// エラー指数リスト
        /// </summary>
        protected List<double> dlErrorIndexList;
        #endregion

        #region コンストラクタ
        public cErrorIndex()
        {
            dlErrorIndexList = new List<double>();
        }
        #endregion        

        /// <summary>
        /// エラー指数を計算し、リストに追加します。
        /// </summary>
        /// <param name="cpdOriginal">再サンプリングデータ</param>
        /// <param name="cpdObject">再構築データ</param>
        public virtual void vSetErrorRate(cPointData Original, cPointData Object)
        {
            #region エラー指数の計算
            double dRestorationRatio = 0;

            //距離の和を計算
            for (int i = 0; i < Original.Length; i++)
            {
                double dDistance = Original[i].Distance(Object[i].X, Object[i].Y, Object[i].Z);
                dRestorationRatio += dDistance;
                Original[i].Tag = dDistance;
                Object[i].Tag = dDistance;
            }

            //距離の平均値をエラー指数とする
            dRestorationRatio /= Original.Length;

            //リストに追加
            dlErrorIndexList.Add(dRestorationRatio);
            #endregion
        }

        /// <summary>
        /// エラー指数をテキストとして保存します。
        /// </summary>
        /// <param name="strIndex">テキストの一行目に入る文字列（車種や人名などをいれると便利）</param>
        /// <param name="strSaveFilePath">保存先のパス</param>
        public virtual bool bSaveErrorRate(string strIndex, string strSaveFilePath)
        {
            #region エラー指数の保存
            if (File.Exists(strSaveFilePath) && MessageBox.Show("エラー指数のファイルが存在します。\n上書きしますか？", "上書き確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;

            double[] darErrorIndex = dlErrorIndex_List.ToArray();

            StringBuilder sb = new StringBuilder();

            sb.Append(strIndex + "\r\n");

            for (int i = 0; i < darErrorIndex.Length; i++)
                sb.Append(darErrorIndex[i].ToString() + "\r\n");

            StreamWriter sw = new StreamWriter(strSaveFilePath);
            try { sw.Write(sb.ToString()); }
            catch { return false; }
            finally { sw.Close(); }

            return true;
            #endregion
        }

        #region プロパティ
        /// <summary>
        /// エラー指数のリストを取得します。
        /// </summary>
        public virtual List<double> dlErrorIndex_List
        {
            get { return dlErrorIndexList; }
        }

        /// <summary>
        /// 指定番目のエラー指数を取得します。
        /// </summary>
        /// <param name="iNum">０から始まる番号</param>
        /// <returns>存在しない場合はー１を返します</returns>
        public virtual double this[int iNum]
        {
            get
            {
                try { return dlErrorIndexList[iNum]; }
                catch { return -1; }
            }
        }
        #endregion
    }
}
