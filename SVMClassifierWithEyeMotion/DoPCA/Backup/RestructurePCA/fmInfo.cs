using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestructurePCA
{
    public partial class fmInfo : Form
    {
        public fmInfo(RestructPCAManager PCAManager)
        {
            InitializeComponent();
            
            //パラメータ数
            lvMain.Items[0].SubItems.Add(PCAManager.ParamCount.ToString());
            
            //データ数
            lvMain.Items[1].SubItems.Add(PCAManager.DataCount.ToString());

            //ベクトル数
            lvMain.Items[2].SubItems.Add(PCAManager.VectorLength.ToString());

            //データの種類
            lvMain.Items[3].SubItems.Add(PCAManager.DataType.ToString());
        }
    }
}
