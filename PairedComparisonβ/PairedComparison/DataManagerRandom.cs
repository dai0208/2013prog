using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MailManager;

namespace PairedComparison
{
    public class DataManagerRandom:cDataManager
    {
        protected int[] DataList ;
        public string tmpMessage;
        protected SendMail Manager = new SendMail();

        public DataManagerRandom()
            : base()
        {
        }

        public DataManagerRandom(PictureBox LeftPictureBox, PictureBox RightPictureBox)
            : base(LeftPictureBox, RightPictureBox)
        {
        }

        public DataManagerRandom(PictureBox LeftPictureBox, PictureBox RightPictureBox, List<System.IO.DirectoryInfo> DirectoryInfo)
            : base(LeftPictureBox, RightPictureBox, DirectoryInfo)
        {
        }

        protected override void vInitData()
        {
            #region データマトリクスの初期化
            base.vInitData();
            
            DataList =  new int[iImageSetNum];

            for (int i = 0; i < iImageSetNum; i++)
            {
                DataList[i] = rnd.Next(iImageSetNum);
                for(int j = 0; j < i; j++)
                    if (DataList[i] == DataList[j])
                    {
                        i--; 
                        break;
                    }
            }

            #endregion
        }


        /// <summary>
        /// 新しい画像セットの評価を始めます
        /// </summary>
        /// <param name="iSentNo">画像セットのインデックス</param>
        public override void vStart(int iSentNo)
        {
            #region 新しい画像セットの評価を開始
            //iNowImageSetNo = iSentNo;
            iNowImageAllCount = icilImageList.bmpaImage[DataList[iSentNo]].Length;

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
            MessageBox.Show(Message[DataList[iSentNo]], "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ///読み流してしまった場合に備えてメッセージを一時保管
            tmpMessage = Message[DataList[iSentNo]];
            bNext(eSelect.None);
            #endregion
        }

        /// <summary>
        /// 画像を評価したときに結果を記入し、次の画像を表示します。
        /// </summary>
        /// <param name="ieSelect">右を選んだか左を選んだか</param>
        /// <returns>Trueなら続きあり、Falseなら全部終了</returns>
        public override bool bNext(eSelect ieSelect)
        {
            #region 画像を評価したときに結果を記入し、次の画像を表示
            //まだ組み合わせてない画像のパターンがあるかどうかのフラグ。Trueだともうすべての組み合わせが完了。
            bool bEndCheck = true;

            //左が選ばれたら
            if (ieSelect == eSelect.Left)
                iaData[DataList[iNowImageSetNo]][iNowLeft][iNowRight] = 1;
            else if (ieSelect == eSelect.Right)
                iaData[DataList[iNowImageSetNo]][iNowRight][iNowLeft] = 1;

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

                    string[] straImageReadFrom = System.IO.Path.GetDirectoryName(icilImageList.fiaFileName[DataList[iNowImageSetNo]][0].FullName).Split('\\');
                    string strImageReadFrom = straImageReadFrom[straImageReadFrom.Length - 1];

                    System.IO.File.WriteAllText(strSaveFolderName + @"\" + strName + "_" + strImageReadFrom + ".csv", strCreateSaveData(), Encoding.GetEncoding("Shift_JIS"));
                    Manager.AddAttachemnt(strSaveFolderName + @"\" + strName + "_" + strImageReadFrom + ".csv");

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

                if (iNowImageSetNo ==  iImageSetNum - 1)
                {
                    //現在の画像セットが最終セットと同じだったら
                    System.Windows.Forms.MessageBox.Show("お疲れ様でした。すべての組み合わせが完了しました。\nこれからデータの送信を行いますので少し時間がかかります。", "完了です。", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Manager.SendMessage();
                    MessageBox.Show("データの送信が完了しました。\n全ての実験が完了しました。お疲れ様でした。", "送信完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            pbxLeft.Image = icilImageList.bmpaImage[DataList[iNowImageSetNo]][iNowLeft];
            pbxRight.Image = icilImageList.bmpaImage[DataList[iNowImageSetNo]][iNowRight]; System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            pbxLeft.Visible = true;
            pbxRight.Visible = true;

            System.Windows.Forms.Application.DoEvents();

            return true;
            #endregion
        }

        //virtual →　override　に変更
        protected override string strCreateSaveData()
        {
            //現在の画像枚数よりも縦横１大きいマトリクスを作成
            string[][] strSaveData = new string[iNowImageAllCount + 1][];
            for (int i = 0; i < iNowImageAllCount + 1; i++)
                strSaveData[i] = new string[iNowImageAllCount + 1];

            //ColumnとRowを作成
            for (int i = 1; i < iNowImageAllCount + 1; i++)
            {
                strSaveData[i][0] = this.icilImageList.fiaFileName[DataList[iNowImageSetNo]][i - 1].Name;
                strSaveData[0][i] = this.icilImageList.fiaFileName[DataList[iNowImageSetNo]][i - 1].Name;
            }

            //データを記入
            for (int i = 0; i < iNowImageAllCount; i++)
                for (int j = 0; j < iNowImageAllCount; j++)
                {
                    strSaveData[i + 1][j + 1] = iaData[DataList[iNowImageSetNo]][i][j].ToString();
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

        public override void vSetName(string strSentName)
        {
            base.vSetName(strSentName);
            Manager.CreateMessage("logmein@akamatsu.info", "stuakmt@gmail.com", strName + "  2013/12/12一対比較法実験結果", strName + "  2013/12/12一対比較法実験結果");
        }
    }
}
