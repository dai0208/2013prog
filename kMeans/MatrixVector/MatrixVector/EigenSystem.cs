using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixVector
{
    /// <summary>
    /// 固有値と固有ベクトルのリストを保持するクラス
    /// </summary>
    [Serializable]
    public class EigenSystem
    {
        /// <summary>
        /// 固有値と固有ベクトルのリスト
        /// </summary>
        protected List<EigenVectorAndValue> EigenListData = new List<EigenVectorAndValue>();

        #region コンストラクタ
        /// <summary>
        /// 固有値と固有ベクトルのリストを保持するクラスを作成します。
        /// </summary>
        public EigenSystem() { }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="EigenSystemData">コピー元</param>
        public EigenSystem(EigenSystem EigenSystemData)
        {
            foreach (EigenVectorAndValue EigenData in EigenSystemData.EigenListData)
                EigenListData.Add(EigenData);
        }
        #endregion

        public void EigenValueNormalize()
        {
            double Sum = 0;
            List<EigenVectorAndValue> ResultEigenListData = new List<EigenVectorAndValue>();

            for (int i = 0; i < this.Count; i++)
                Sum += EigenListData[i].EigenValue;

            for (int i = 0; i < this.Count; i++)
                ResultEigenListData.Add(new EigenVectorAndValue(EigenListData[i].EigenVector, EigenListData[i].EigenValue / Sum));

            this.EigenListData = ResultEigenListData;
        }

        /// <summary>
        /// 固有値、固有ベクトルのリストにデータを加えます
        /// 追加したデータは固有値が大きい順にソートされます。
        /// </summary>
        /// <param name="EigenData">固有値、固有ベクトルデータ</param>
        public void Add(EigenVectorAndValue EigenData)
        {
            EigenListData.Add(EigenData);
            EigenListData.Sort();
        }

        /// <summary>
        /// 固有値、固有ベクトルのデータを取得、設定します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>固有値、固有ベクトルデータ</returns>
        public EigenVectorAndValue this[int index]
        {
            get { return new EigenVectorAndValue(EigenListData[index]); }
            set { EigenListData[index] = value; }
        }

        /// <summary>
        /// 固有値、固有ベクトルのリストからデータを削除します
        /// </summary>
        /// <param name="index">削除するデータのインデックス</param>
        public void RemoveAt(int index)
        {
            EigenListData.RemoveAt(index);
            EigenListData.Sort();
        }

        /// <summary>
        /// 全ての固有ベクトルを行列形式で取得します。
        /// </summary>
        /// <returns>全ての固有ベクトル</returns>
        public Matrix GetEigenVectors()
        {
            Vector[] ReturnVector = new Vector[EigenListData.Count];
            for (int i = 0; i < EigenListData.Count; i++)
                ReturnVector[i] = EigenListData[i].EigenVector;

            return new Matrix(ReturnVector);
        }

        public Vector GetEigenValueVector(int ParamSize = 0)
        {
            if (ParamSize == 0)
                ParamSize = this.Count;

            Vector ResultVector = new Vector(ParamSize);
            for (int i = 0; i < ParamSize; i++)
                ResultVector[i] = EigenListData[i].EigenValue;

            return ResultVector;
        }

        /// <summary>
        /// 指定された累積寄与率を超える主成分番号を取得
        /// </summary>
        /// <param name="Ratio">累積寄与率</param>
        /// <returns>指定された累積寄与率を超える主成分番号</returns>
        public int GetCumulativeContributionRatioIndex(double Ratio)
        {
            if (Ratio >= 1d)
                return this.Count;

            for (int i = 0; i < this.Count; i++)
                if (this.CumulativeContributionRatio[i] < Ratio)
                    return i;

            return -1;
        }

        /// <summary>
        /// 固有値、固有ベクトルのリストの大きさを取得します。
        /// </summary>
        public int Count
        {
            get { return EigenListData.Count; }
        }

        /// <summary>
        /// 寄与率を取得（合計が1）
        /// </summary>
        public List<double> ContributionRatio
        {
            get
            {
                List<double> ResultList = new List<double>();
                double Sum = 0d;

                for (int i = 0; i < this.Count; i++)
                    Sum += EigenListData[i].EigenValue;

                for (int i = 0; i < this.Count; i++)
                    ResultList.Add(EigenListData[i].EigenValue / Sum);

                return ResultList;
            }
        }

        /// <summary>
        /// 累積寄与率を取得
        /// </summary>
        public List<double> CumulativeContributionRatio
        {
            get
            {
                List<double> ResultList = new List<double>();
                double Sum = 0d;

                for (int i = 0; i < this.Count; i++)
                    Sum += EigenListData[i].EigenValue;

                ResultList.Add(EigenListData[0].EigenValue / Sum);

                for (int i = 1; i < this.Count; i++)
                    ResultList.Add(ResultList[i - 1] + EigenListData[i].EigenValue / Sum);

                return ResultList;
            }
        }
    }
}
