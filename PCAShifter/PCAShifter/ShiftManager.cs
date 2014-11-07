using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;

namespace PCAShifter
{
    public class ShiftManager
    {
        Vector vctUnknown;
        Vector vctAverage;
        ColumnVector[] EigenVector;

        Vector PCAParam;

        //コンストラクタ
        public ShiftManager(Vector vctUnknown, Vector vctAverage, ColumnVector[] EigenVector)
        {
            this.vctUnknown = vctUnknown;
            this.vctAverage = vctAverage;
            this.EigenVector = EigenVector;
        }

        /// <summary>
        /// 固有ベクトルを使ってパラメータを導出します。
        /// </summary>
        public void DoShift()
        {
            PCAParam = new Vector(EigenVector.Length);
            for (int i = 0; i < EigenVector.Length; i++)
            {
                PCAParam[i] = EigenVector[i].InnerProduct(vctUnknown - vctAverage);
            }
        }
    }
}
