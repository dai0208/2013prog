using System;
using MatrixVector;

namespace MutualConversion
{
    /// <summary>
    /// cMutualConversionに基準値を使用する機能を追加したクラスです。
    /// 必ず継承させて使用してください。
    /// </summary>
    public class cMutualConversionWithReferenceValue : cMutualConversion
    {
        #region プライベート変数
        /// <summary>
        /// すべてデータの展開係数初期値
        /// </summary>
        private Matrix imCoefficients;

        /// <summary>
        /// 展開係数の平均値
        /// </summary>
        private ColumnVector icvCoefficientAverage;

        /// <summary>
        /// 展開係数の分散
        /// </summary>
        private ColumnVector icvDecentralization;

        /// <summary>
        /// 展開係数の標準偏差
        /// </summary>
        private ColumnVector icvStandardDeviation;

        /// <summary>
        /// 基準値
        /// </summary>
        private ColumnVector icvReference;  
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 固有ベクトル・平均ベクトル・展開係数をセットします。
        /// </summary>
        /// <param name="mEigenMtx">固有ベクトル</param>
        /// <param name="cvAverageVec">平均ベクトル</param>
        /// <param name="mCoefficients">展開係数</param>
        protected cMutualConversionWithReferenceValue(Matrix mEigenMtx, ColumnVector cvAverageVec, Matrix mCoefficients)
            : base(mEigenMtx, cvAverageVec)
        {
            this.imCoefficients = mCoefficients;

            this.vInit();
        }
        #endregion

        /// <summary>
        /// 相関係数の平均・分散・標準偏差の計算を行います。
        /// </summary>
        protected virtual void vInit()
        {
            #region 初期化
            //平均
            {
                icvCoefficientAverage = new ColumnVector(imCoefficients.RowSize);

                for (int i = 0; i < icvCoefficientAverage.Length; i++)
                    icvCoefficientAverage[i] = imCoefficients.GetRowVector(i).GetAverage();
            }

            //分散
            {
                icvDecentralization = new ColumnVector(imCoefficients.RowSize);

                for (int i = 0; i < icvDecentralization.Length; i++)
                {
                    for (int j = 0; j < imCoefficients.ColSize; j++)
                        icvDecentralization[i] += Math.Pow(imCoefficients[i, j] - icvCoefficientAverage[i], 2);

                    icvDecentralization[i] /= imCoefficients.ColSize;
                }
            }

            //標準偏差
            {
                icvStandardDeviation = new ColumnVector(imCoefficients.RowSize);

                for (int i = 0; i < icvStandardDeviation.Length; i++)
                    icvStandardDeviation[i] = Math.Sqrt(icvDecentralization[i]);
            }
            #endregion
        }

        /// <summary>
        /// 展開係数から基準値を作成します。
        /// </summary>
        protected virtual void vCoefficientToReference()
        {
            #region 展開係数→基準値
            icvReference = new ColumnVector(icvCoefficientAverage.Length);

            for (int i = 0; i < icvReference.Length; i++)
                icvReference[i] = (icvCoefficient[i] - icvCoefficientAverage[i]) / icvStandardDeviation[i];

            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// 基準値から展開係数を作成します。
        /// </summary>
        protected virtual void vReferenceToCoefficient()
        {
            #region 基準値→展開係数
            icvCoefficient = new ColumnVector(icvReference.Length);

            for (int i = 0; i < icvCoefficient.Length; i++)
                icvCoefficient[i] = icvReference[i] * icvStandardDeviation[i] + icvCoefficientAverage[i];


            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        #region プロパティ
        /// <summary>
        /// 展開係数の基準値を設定、取得します。
        /// </summary>
        public virtual ColumnVector cvReference_Value
        {
            get
            {
                base.vProgressBarReset(1);
                this.vCoefficientToReference();

                return new ColumnVector(icvReference); 
            }
            set
            {
                base.vCoefficientErrorCheck(value);

                icvReference = new ColumnVector(value);

                base.vProgressBarReset(1);
                this.vReferenceToCoefficient();
                base.cvCoefficient_Value = icvCoefficient;
            }
        }
        #endregion
    }
}
