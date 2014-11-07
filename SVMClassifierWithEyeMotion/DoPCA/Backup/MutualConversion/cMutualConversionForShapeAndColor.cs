using PointFormat;
using MatrixVector;
using System;

namespace MutualConversion
{
    /// <summary>
    /// 形状と色両方を使用してで主成分分析を行ったときに使用します。
    /// ascファイル、cPointData、展開係数の相互変換を行い、それらからBitmapを作成するクラスです。
    /// </summary>
    public class cMutualConversionForShapeAndColor : cMutualConversionWithReferenceValue
    {
        #region コンストラクタ
        /// <summary>
        /// 固有ベクトル・平均ベクトル・展開係数をセットします。
        /// </summary>
        /// <param name="mEigenmTX">固有ベクトル</param>
        /// <param name="cvAverageVec">平均ベクトル</param>
        /// <param name="mCoefficients">展開係数</param>
        public cMutualConversionForShapeAndColor(Matrix mEigenMtx, ColumnVector cvAverageVec, Matrix mCoefficients)
            : base(mEigenMtx, cvAverageVec, mCoefficients)
        {
        }
        #endregion               

        /// <summary>
        /// ３次元形状のＸＹＺＲＧＢからPointDataを作成します。
        /// </summary>
        protected override void vPartToPointData()
        {
            #region Part→PointData
            cPoint[] icarPoint = new cPoint[icvPart.Length / 6];

            for (int i = 0; i < icarPoint.Length; i++)
            {
                icarPoint[i] = new cPoint();

                icarPoint[i].X = icvPart[i * 6 + 0];
                icarPoint[i].Y = icvPart[i * 6 + 1];
                icarPoint[i].Z = icvPart[i * 6 + 2];
                icarPoint[i].R = (int)icvPart[i * 6 + 3];
                icarPoint[i].G = (int)icvPart[i * 6 + 4];
                icarPoint[i].B = (int)icvPart[i * 6 + 5];
            }

            icPointData = new cPointData(icarPoint);

            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// PointDataから３次元形状のＸＹＺＲＧＢを作成します。
        /// </summary>
        protected override void vPointDataToPart()
        {
            #region PointData→Part
            icvPart = base.cvPointDataToVector();
            #endregion
        }

        protected override void vPointDataErrorCheck(cPointData cpdCheck)
        {
            #region PointDataのエラーチェック
            if (cpdCheck.Length * 6 != icvAverageVec.Length)
                throw new ApplicationException("データのサイズが違います");
            #endregion
        }        
    }
}
