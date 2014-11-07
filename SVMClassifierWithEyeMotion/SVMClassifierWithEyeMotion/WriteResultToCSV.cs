using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SVMClassifierWithEyeMotion
{
    public class WriteResultToCSV
    {
        private int[] ResultClass; //-1(Low) or 0(Neutral) or 1(Hight)
        private string[] name;//画像名

        #region コンストラクタ
        public WriteResultToCSV(string[] name, int[] ResultClass)
        {
            this.name = name;
            this.ResultClass = ResultClass;
        }
        #endregion

        public void WriteToCSV()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write("High,Neutral,Low\n");
                int filenum = name.Length;
                string[] DATA = new string[filenum];

                int Hight = 0;
                int Neutral = 0;
                int Low = 0;

                for (int i = 0; i < filenum; i++)
                {
                    if (ResultClass[i] == 1)
                    {
                        DATA[i] = "High";
                        Hight++;
                    }

                    else if (ResultClass[i] == 0)
                    {
                        DATA[i] = "Neutral";
                        Neutral++;
                    }

                    else
                    {
                        DATA[i] = "Low";
                        Low++;
                    }

                }

                sw.Write(Hight + "," + Neutral + "," + Low + "\n");
                sw.Write((double)((double)Hight / (double)filenum) + "," + (double)((double)Neutral / (double)filenum) + "," + (double)((double)Low / (double)filenum) + "\n\n");

                sw.Write("画像,結果\n");
                for (int i = 0; i < filenum; i++)
                {
                    sw.Write(Path.GetFileName(name[i]) + "," + DATA[i] + "\n");
                }
                sw.Close();
                MessageBox.Show("しゅーりょーです(・ω・)");
            }
        }

    }
}
