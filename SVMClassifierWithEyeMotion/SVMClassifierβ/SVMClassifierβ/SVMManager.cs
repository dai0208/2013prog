using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;

namespace SVMClassifierβ
{
    public class SVMManager
    {
        //コンストラクタ
        public SVMManager(){}
        
        /// <summary>
        /// マトリクスデータをdouble型List配列に変換します。
        /// </summary>
        /// <param name="mtx"></param>
        public List<List<double>> MatrixToArray(Matrix mtx)
        {
            List<List<double>> MatrixArray = new List<List<double>>();
            for (int col = 0; col < mtx.ColSize; col++)
            {
                MatrixArray.Add(VectorToArray(mtx.GetColVector(col)));
            }
            return MatrixArray;
        }

        /// <summary>
        /// ベクトルデータをdouble型List配列に変換します。
        /// </summary>
        /// <param name="vct"></param>
        public List<double> VectorToArray(ColumnVector vct)
        {
            List<double> VectorArray = new List<double>();
            for (int row = 0; row < vct.Length; row++)
            {
                VectorArray.Add(vct.VectorElement[row]);
            }
            return VectorArray;
        }

        /// <summary>
        /// ベクトル配列をdouble型List配列に変換します。
        /// </summary>
        /// <param name="vcts"></param>
        /// <returns></returns>
        public List<List<double>> VectArrayToArray(ColumnVector[] vcts)
        {
            List<List<double>> VectorArray = new List<List<double>>();
            for(int col = 0; col < vcts.Length;col++)
            {
                VectorArray.Add(VectorToArray(vcts[col]));
            }
            return VectorArray;
        }
    }
}
