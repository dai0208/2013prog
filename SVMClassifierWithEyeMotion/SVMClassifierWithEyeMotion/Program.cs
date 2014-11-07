using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HighNeuManager = PCAManagerFromBitmap.PCAManagerFromBitmap;
using NeuLowManager = PCAManagerFromBitmap.PCAManagerFromBitmap;

namespace SVMClassifierWithEyeMotion
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            HighNeuManager HNmanager = new HighNeuManager();
            NeuLowManager NLmanager = new NeuLowManager();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(HNmanager,NLmanager));
        }
    }
}
