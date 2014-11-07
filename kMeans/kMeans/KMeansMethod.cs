using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;
using OpenCvSharp;
using CvUtil;

namespace kMeans
{
    public class KMeansMethod : CvUtility
    {
        public KMeansMethod()
            :base(){ }

        /// <summary>
        /// k-means法によるクラスタリング。サンプルデータ群は縦ベクトルの集合としてください。
        /// </summary>
        /// <param name="samples">サンプルデータ群</param>
        /// <param name="clusterNum">出力クラスタの数</param>
        /// <returns>各データのラベル。入力データの添え字と対応したラベルを返す。</returns>
        public static Vector KMeansClustering(Matrix samples, int ClusterNum)
        {
            CvMat CvMtx;
            MatrixToCvMat(samples.GetTranspose(), out CvMtx);
            CvMat Clusters = Cv.CreateMat(samples.RowSize, 1, MatrixType.S32C1);

            /*反復回数、目標精度、クラスタ数の設定*/
            int maxIter = 10;
            double Epsilon = 0.5;

            /* k-meansクラスタリング */
            Cv.KMeans2(CvMtx, ClusterNum, Clusters, Cv.TermCriteria(CriteriaType.Iteration, maxIter, Epsilon));
            Vector Labels = new Vector(CvUtility.MatToVector(Clusters));

            /* 後処理 */
            CvMtx.Dispose();
            Clusters.Dispose();

            return Labels;
        }
    }
}
