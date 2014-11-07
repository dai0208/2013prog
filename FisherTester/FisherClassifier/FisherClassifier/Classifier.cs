using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MatrixVector;

namespace FisherClassifier
{
    public class Classifier
    {
        private ColumnVector inData;
        private Matrix cls1;
        private Matrix cls2;
        private ColumnVector mean1;
        private ColumnVector mean2;
        private ColumnVector w;
        private ColumnVector ConvertedVector;

        //コンストラクタ
        public Classifier(ColumnVector inData, Matrix cls1, Matrix cls2)
        {
            this.inData = inData;
            this.cls1 = cls1;
            this.cls2 = cls2;
        }
        
        //ゲッター&セッター
        public ColumnVector getConvertedVector
        {
            get { return ConvertedVector; }
        }

        public void saveConvertedVector(string savepath)
        { 
            ConvertedVector.Save(savepath);
        }

        #region 変換メソッド
        /// <summary>
        /// フィッシャーの線形判別を行います。
        /// </summary>
        /// <param name="weight"></param>
        public void DoFisherClassifier()
        {
            ///平均ベクトルの導出
            mean1 = cls1.GetAverageRow();
            mean2 = cls2.GetAverageRow();

            ///クラス内共分散行列の導出
            Matrix Sw = getCovarianceMatrix(cls1) + getCovarianceMatrix(cls2);

            ///逆行列の算出
            Matrix Sw_inv = getInverseMatrix(Sw);

            ///重みベクトルの算出
            w = Sw_inv * (mean2 - mean1);
        }

        /// <summary>
        /// 重みベクトルを基に印象変換を行います。
        /// </summary>
        /// <param name="weight"></param>
        public void DoImpressionTransfer(int weight)
        {
            ///ノルム正規化
            ColumnVector TransFormVector = w / w.GetNorm();

            ///正クラスの重心から表情の重心への距離を基に印象変換尺度の設定
            double dist = EuclideanDistance(mean1, mean2);
            dist = dist / 10.0;

            ///入力ベクトルの印象変換
            ColumnVector inData_converted = new ColumnVector(inData);
            for (int i = 0; i < inData.Length; i++)
            {
                inData_converted[i] = inData[i] + weight * dist * TransFormVector[i];
            }
            this.ConvertedVector = new ColumnVector(inData_converted);
        }

        /// <summary>
        /// 共分散行列を返します。
        /// </summary>
        /// <param name="LoadMatrix"></param>
        /// <returns></returns>
        public Matrix getCovarianceMatrix(Matrix LoadMatrix)
        {
            ///平均ベクトルを取得
            ColumnVector AverageVector = LoadMatrix.GetAverageRow();

            ///平均ベクトルがColSize個並んだ行列を作成
            Matrix AverageMatrix = Matrix.GetSameElementMatrix(AverageVector, LoadMatrix.ColSize);

            ///各要素ベクトルから平均ベクトルを引いて、差分行列を作成
            Matrix DiffMatrix = LoadMatrix - AverageMatrix;

            ///差分行列とその転置行列をかけて共分散行列を作成（必ず対称行列になります）
            SymmetricMatrix LMatrix = new SymmetricMatrix(DiffMatrix * DiffMatrix.GetTranspose());

            return LMatrix;
        }

        /// <summary>
        /// 掃出し法を適用して逆行列を返します。必ず正方行列を使ってください。
        /// </summary>
        /// <param name="LoadMatrix"></param>
        /// <returns></returns>
        public Matrix getInverseMatrix(Matrix LoadMatrix)
        {
            double buf;

            Matrix LoadMatrix_inv = new Matrix(LoadMatrix);
            #region 単位行列の作成
            for(int i=0;i<LoadMatrix.ColSize ;i++)
            {
                for(int j=0;j<LoadMatrix.ColSize;j++)
                {
                    if(i == j)
                    {
                        LoadMatrix_inv[j,i] = 1.0;
                    }
                    else
                    {
                        LoadMatrix_inv[j,i] = 0.0;
                    }
                }
            }
            #endregion

            #region 掃出法
            for(int i=0;i<LoadMatrix.ColSize;i++)
            {
                buf = 1 / LoadMatrix[i,i];

                for(int j=0;j<LoadMatrix.ColSize;j++)
                {
                    LoadMatrix[j,i] *= buf;
                    LoadMatrix_inv[j,i] *= buf;
                }
                for(int j=0;j<LoadMatrix.ColSize;j++)
                {
                    if(i != j)
                    {
                        buf = LoadMatrix[i,j];
                        for(int k=0; k<LoadMatrix.ColSize;k++)
                        {
                            LoadMatrix[k,j] -= LoadMatrix[k,i] * buf;
                            LoadMatrix_inv[k,j] -= LoadMatrix_inv[k,i] * buf;
                        }
                    }
                }
            }
            #endregion
            return LoadMatrix_inv;
        }

        /// <summary>
        /// ２ノード間のユークリッド距離を返します。※２ノードの次元数は揃えてください。
        /// </summary>
        /// <param name="node1">ノード１</param>
        /// <param name="node2">ノード２</param>
        /// <returns>距離値</returns>
        private double EuclideanDistance(ColumnVector vct1, ColumnVector vct2)
        {
            double dist = 0;

            for (int i = 0; i < vct1.Length; i++)
            {
                dist += Math.Abs(vct1[i] - vct2[i]);
            }
            return dist;
        }
        #endregion
    }
}
