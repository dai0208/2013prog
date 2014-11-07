using PointFormat;
using MatrixVector;
using System;

namespace MutualConversion
{
    /// <summary>
    /// 形状で主成分分析を行ったときに使用します。
    /// ascファイル、cPointData、展開係数の相互変換を行い、それらからBitmapを作成するクラスです。
    /// </summary>
    public class cMutualConversionForShape : cMutualConversionWithReferenceValue
    {
        #region コンストラクタ
        /// <summary>
        /// 固有ベクトル・平均ベクトル・展開係数をセットします。
        /// </summary>
        /// <param name="mEigenmTX">固有ベクトル</param>
        /// <param name="cvAverageVec">平均ベクトル</param>
        /// <param name="mCoefficients">展開係数</param>
        public cMutualConversionForShape(Matrix mEigenMtx, ColumnVector cvAverageVec, Matrix mCoefficients)
            :base(mEigenMtx, cvAverageVec, mCoefficients)
        {
        }
        #endregion

        /// <summary>
        /// ３次元形状のＸＹＺからPointDataを作成します。
        /// </summary>
        protected override void vPartToPointData()
        {
            #region Part→PointData
            cPoint[] icarPoint = new cPoint[icvPart.Length / 3];

            for (int i = 0; i < icarPoint.Length; i++)
            {
                icarPoint[i] = new cPoint();

                icarPoint[i].X = icvPart[i * 3 + 0];
                icarPoint[i].Y = icvPart[i * 3 + 1];
                icarPoint[i].Z = icvPart[i * 3 + 2];
                icarPoint[i].R = 256;
                icarPoint[i].G = 256;
                icarPoint[i].B = 256;
            }

            icPointData = new cPointData(icarPoint);

            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// PointDataから３次元形状のＸＹＺを作成します。
        /// </summary>
        protected override void vPointDataToPart()
        {
            #region PointData→Part
            icvPart = new ColumnVector(icPointData.Length * 3);

            for (int i = 0; i < icPointData.Length; i++)
            {
                icvPart[i * 3 + 0] = icPointData.Items[i].X;
                icvPart[i * 3 + 1] = icPointData.Items[i].Y;
                icvPart[i * 3 + 2] = icPointData.Items[i].Z;
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
