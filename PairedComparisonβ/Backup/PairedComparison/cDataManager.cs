using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PairedComparison
{
    public enum eSelect
    {
        Left,
        Right,
        None
    }

    public class cDataManager
    {
        #region プライベート変数
        /// <summary>
        /// 画像データのリスト
        /// </summary>
        protected cImageList icilImageList;

        /// <summary>
        /// 被験者の名前
        /// </summary>
        protected string strName;

        /// <summary>
        /// イメージセットのセット数
        /// </summary>
        protected int iImageSetNum;

        /// <summary>
        /// 現在評価している画像セットのNo
        /// </summary>
        protected int iNowImageSetNo;

        /// <summary>
        /// 現在のイメージセットの画像総枚数
        /// </summary>
        protected int iNowImageAllCount;

        /// <summary>
        /// 現在描画している左の画像の番号
        /// </summary>
        protected int iNowLeft;

        /// <summary>
        /// 現在描画している右の画像の番号
        /// </summary>
        protected int iNowRight;

        /// <summary>
        /// 画像が良いと判断されたらこのマトリクスの値が+1されます。
        /// 3次元配列で、最初の添え字は画像セット番号、2番目と3番目がその画像セットの画像ファイル番号です。
        /// </summary>
        protected int[][][] iaData;

        /// <summary>
        /// 表示したかどうかのチェック(Trueなら既に表示してFalseならまだ表示していない)
        /// </summary>
        protected bool[][] bCheck;
        
        /// <summary>
        /// 次の画像を表示するまでのインターバル
        /// </summary>
        protected int iWait = 500;

        protected string strSaveFolderName = "";

        protected Random rnd = new Random();
        protected PictureBox pbxLeft;
        protected PictureBox pbxRight;

        protected string[] Message;
        #endregion

        #region コンストラクタ
        protected cDataManager()
        {
        }

        public cDataManager(PictureBox pbxSentLeftPictureBox, PictureBox pbxSentRightPictureBox)
        {
            pbxLeft = pbxSentLeftPictureBox;
            pbxRight = pbxSentRightPictureBox;
        }

        public cDataManager(PictureBox pbxSentLeftPictureBox, PictureBox pbxSentRightPictureBox, List<System.IO.DirectoryInfo> ldiSent)
        {
            pbxLeft = pbxSentLeftPictureBox;
            pbxRight = pbxSentRightPictureBox;

            icilImageList = new cImageList(ldiSent);
            this.vInitData();
        }

        public virtual void vSetDirectoryData(List<System.IO.DirectoryInfo> ldiSent)
        {
            icilImageList = new cImageList(ldiSent);
            this.vInitData();
        }
        #endregion

        /// <summary>
        /// 被験者の名前をセットします。
        /// </summary>
        /// <param name="strSentName">被験者の名前</param>
        public virtual void vSetName(string strSentName)
        {
            #region 被験者の名前をセット
            strName = strSentName;
            #endregion
        }

        /// <summary>
        /// データマトリクスを初期化します（画像セット数*画像データ数*画像データ数 のマトリクスを作るって事）
        /// </summary>
        protected virtual void vInitData()
        {
            #region データマトリクスの初期化
            iImageSetNum = icilImageList.bmpaImage.Length;

            iaData = new int[iImageSetNum][][];

            for (int i = 0; i < iaData.Length; i++)
            {
                iaData[i] = new int[icilImageList.bmpaImage[i].Length][];
                for (int j = 0; j < iaData[i].Length; j++)
                {
                    iaData[i][j] = new int[icilImageList.bmpaImage[i].Length];
                }
            }

            using (System.IO.StreamReader srList = new System.IO.StreamReader(@"./画面表示.txt"))
            {
                string AllMessage = srList.ReadToEnd();
                AllMessage = AllMessage.Replace("\r", "");
                AllMessage = AllMessage.TrimEnd('\n');
                Message = AllMessage.Split('\n');
            }

            #endregion
        }

        /// <summary>
        /// 新しい画像セットの評価を始めます
        /// </summary>
        /// <param name="iSentNo">画像セットのインデックス</param>
        public virtual void vStart(int iSentNo)
        {
            #region 新しい画像セットの評価を開始
            iNowImageSetNo = iSentNo;
            iNowImageAllCount = icilImageList.bmpaImage[iSentNo].Length;

            bCheck = new bool[iNowImageAllCount][];
            for (int i = 0; i < bCheck.Length; i++)
            {
                bCheck[i] = new bool[bCheck.Length];
            }
            for (int i = 0; i < bCheck.Length; i++)
            {
                //同じ画像の組み合わせは表示したことにする。
                bCheck[i][i] = true;
            }
            MessageBox.Show(Message[iSentNo], "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bNext(eSelect.None);
            #endregion
        }

        /// <summary>
        /// 画像を評価したときに結果を記入し、次の画像を表示します。
        /// </summary>
        /// <param name="ieSelect">右を選んだか左を選んだか</param>
        /// <returns>Trueなら続きあり、Falseなら全部終了</returns>
        public virtual bool bNext(eSelect ieSelect)
        {
            #region 画像を評価したときに結果を記入し、次の画像を表示
            //まだ組み合わせてない画像のパターンがあるかどうかのフラグ。Trueだともうすべての組み合わせが完了。
            bool bEndCheck = true;

            //左が選ばれたら
            if (ieSelect == eSelect.Left)
                iaData[iNowImageSetNo][iNowLeft][iNowRight] = 1;
            else if (ieSelect == eSelect.Right)
                iaData[iNowImageSetNo][iNowRight][iNowLeft] = 1;

            //評価した組み合わせにチェックを入れる
            bCheck[iNowLeft][iNowRight] = true;
            bCheck[iNowRight][iNowLeft] = true;

            //もしまだ組み合わせてない画像のパターンがあったらbEndCheckをFalseに。
            for (int i = 0; i < iNowImageAllCount; i++)
                for (int j = 0; j < iNowImageAllCount; j++)
                    if (bCheck[i][j] == false)
                    {
                        bEndCheck = false;
                        break;
                    }

            //終了チェック
            if (bEndCheck)
            {
                try
                {
                    //保存フォルダがちゃんとあるか？
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strSaveFolderName);
                    if (!di.Exists) di.Create();

                    string[] straImageReadFrom = System.IO.Path.GetDirectoryName(icilImageList.fiaFileName[iNowImageSetNo][0].FullName).Split('\\');
                    string strImageReadFrom = straImageReadFrom[straImageReadFrom.Length - 1];

                    System.IO.File.WriteAllText(strSaveFolderName + @"\" + strName + "_" + strImageReadFrom + ".csv", strCreateSaveData(), Encoding.GetEncoding("Shift_JIS"));

                    /*
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(strSaveFolderName + @"\" + strName + "_" + strImageReadFrom + ".csv"))
                    {
                        string strSaveData = strCreateSaveData();
                        sw.Write(strSaveData);
                    }
                    */
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("ファイルを書き込むときにエラーが起こりました。\nこのまま管理者に報告をしてください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (iNowImageSetNo == iImageSetNum -1)
                {
                    //現在の画像セットが最終セットと同じだったら
                    System.Windows.Forms.MessageBox.Show("お疲れ様でした。すべての組み合わせが完了しました。", "完了です。", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                    System.Threading.Thread.Sleep(0);
                    System.Windows.Forms.Application.ExitThread();
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    //まだ別の画像があるなら
                    System.Windows.Forms.MessageBox.Show("現在の画像セットが完了しました。\n続いて次の画像セットに入ります(" + (iNowImageSetNo + 1) + " / " + iImageSetNum + ")", "セット終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iNowLeft = 0;
                    iNowRight = 0;
                    this.vStart(++iNowImageSetNo);
                    return true;
                }
            }

            //評価したことない組み合わせになるまで繰り返す
            do
            {
                iNowLeft = rnd.Next(iNowImageAllCount);
                iNowRight = rnd.Next(iNowImageAllCount);
            } while (bCheck[iNowLeft][iNowRight] == true);

            //画像を描画(次の画像にかえるまでに少しブランクを空けなくてはならないらしい。500ms～1000msが妥当か？？)
            pbxLeft.Visible = false;
            pbxRight.Visible = false;
            pbxLeft.Image = icilImageList.bmpaImage[iNowImageSetNo][iNowLeft];
            pbxRight.Image = icilImageList.bmpaImage[iNowImageSetNo][iNowRight]; System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(iWait);
            pbxLeft.Visible = true;
            pbxRight.Visible = true;

            System.Windows.Forms.Application.DoEvents();

            return true;
            #endregion
        }

        public string SaveFolderName
        {
            set { strSaveFolderName = value; }
            get { return strSaveFolderName; }
        }

        public string Name
        {
            set { strName = value; }
            get { return strName;}
        }

        protected virtual string strCreateSaveData()
        {
            //現在の画像枚数よりも縦横１大きいマトリクスを作成
            string[][] strSaveData = new string[iNowImageAllCount + 1][];
            for (int i = 0; i < iNowImageAllCount + 1; i++)
                strSaveData[i] = new string[iNowImageAllCount + 1];

            //ColumnとRowを作成
            for (int i = 1; i < iNowImageAllCount + 1; i++)
            {
                strSaveData[i][0] = this.icilImageList.fiaFileName[iNowImageSetNo][i - 1].Name;
                strSaveData[0][i] = this.icilImageList.fiaFileName[iNowImageSetNo][i - 1].Name;
            }

            //データを記入
            for (int i = 0; i < iNowImageAllCount; i++)
                for (int j = 0; j < iNowImageAllCount; j++)
                {
                    strSaveData[i + 1][j + 1] = iaData[iNowImageSetNo][i][j].ToString();
                }

            StringBuilder sbData = new StringBuilder();
            for (int i = 0; i < iNowImageAllCount + 1; i++)
            {
                for (int j = 0; j < iNowImageAllCount + 1; j++)
                {
                    sbData.Append(strSaveData[i][j]).Append(",");
                }
                sbData.Append("\n");
            }
            return sbData.ToString().Replace(",\n", "\n").Replace("\r\n", "\n").Replace("\n\n", "\n").TrimEnd('\n');
        }
    }
}
