using PointFormat;
using MatrixVector;
using System;

namespace MutualConversion
{
    /// <summary>
    /// 色で主成分分析を行ったときに使用します。
    /// ascファイル、cPointData、展開係数の相互変換を行い、それらからBitmapを作成するクラスです。
    /// </summary>
    public class cMutualConversionForColor : cMutualConversionWithReferenceValue
    {      
        #region コンストラクタ
        /// <summary>
        /// 固有ベクトル・平均ベクトル・展開係数をセットします。
        /// </summary>
        /// <param name="mEigenMtx">固有ベクトル</param>
        /// <param name="cvAverageVec">平均ベクトル</param>
        /// <param name="mCoefficients">展開係数</param>
        public cMutualConversionForColor(Matrix cmEigenMtx, ColumnVector cvAverageVec, Matrix mCoefficients)
            :base(cmEigenMtx, cvAverageVec, mCoefficients)
        {
        }
        #endregion

        /// <summary>
        /// ３次元形状のＲＧＢからPointDataを作成します。
        /// </summary>
        protected override void vPartToPointData()
        {
            #region Part→PointData
            cPoint[] icarPoint = new cPoint[icvPart.Length / 3];

            for (int i = 0; i < icarPoint.Length; i++)
            {
                icarPoint[i] = new cPoint();

                icarPoint[i].X = 0;
                icarPoint[i].Y = 0;
                icarPoint[i].Z = 0;
                icarPoint[i].R = (int)icvPart[i * 3 + 0];
                icarPoint[i].G = (int)icvPart[i * 3 + 1];
                icarPoint[i].B = (int)icvPart[i * 3 + 2];
            }

            icPointData = new cPointData(icarPoint);


            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// PointDataから３次元形状のＲＧＢを作成します。
        /// </summary>
        protected override void vPointDataToPart()
        {
            #region PointData→Part
            icvPart = new ColumnVector(icPointData.Length * 3);        

            for (int i = 0; i < icPointData.Length; i++)
            {
                icvPart[i * 3 + 0] = icPointData.Items[i].R;
                icvPart[i * 3 + 1] = icPointData.Items[i].G;
                icvPart[i * 3 + 2] = icPointData.Items[i].B;
            }

            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        protected override void vPointDataErrorCheck(cPointData cpdCheck)
        {
            #region PointDataのエラーチェック
            if (cpdCheck.Length * 3 != icvAverageVec.Length)
                throw new ApplicationException("データのサイズが違います");
            #endregion
        }        
    }
}
