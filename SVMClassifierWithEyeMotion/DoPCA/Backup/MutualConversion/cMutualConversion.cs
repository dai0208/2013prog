using System;
using System.Drawing;
using PointFormat;
using MatrixVector;
using System.Windows.Forms;
using cBitmap;

namespace MutualConversion
{
    /// <summary>
    /// ascファイル、cPointData、展開係数の相互変換を行い、それらからBitmapを作成するクラスです。
    /// 必ず継承させて使用してください。
    /// </summary>
    public class cMutualConversion
    {
        #region プライベート変数
        /// <summary>
        /// ３次元形状データ
        /// </summary>
        protected cPointData icPointData;

        /// <summary>
        /// 展開係数
        /// </summary>
        protected ColumnVector icvCoefficient;

        /// <summary>
        /// ２次元形状画像
        /// </summary>
        protected Bitmap bmpData;

        /// <summary>
        /// 固有ベクトルの行列
        /// </summary>
        protected Matrix imEigenMtx;

        /// <summary>
        /// 平均ベクトル
        /// </summary>
        protected ColumnVector icvAverageVec;

        /// <summary>
        /// ３次元形状の一部（形状または色）または画像
        /// </summary>
        protected ColumnVector icvPart;

        /// <summary>
        /// プログレスバー
        /// </summary>
        protected ToolStripProgressBar pgbMain;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// このインスタンスで使用する固有ベクトル・平均ベクトルをセットします。
        /// </summary>
        /// <param name="mEigenMtx">固有ベクトル</param>
        /// <param name="cvAverageVec">平均ベクトル</param>
        protected cMutualConversion(Matrix mEigenMtx, ColumnVector cvAverageVec)
        {
            this.imEigenMtx = mEigenMtx;
            this.icvAverageVec = cvAverageVec;
        }
        #endregion

        /// <summary>
        /// ascファイルからPointDataを作成します。
        /// </summary>
        /// <param name="strAscFilePath">ascファイルのファイルパス</param>
        public virtual void vAscToPointData(string strAscFilePath)
        {
            #region asc→PontData
            cLoadPoint icLoadPoint = new cLoadPoint(strAscFilePath);
            icLoadPoint.bReadData();
            cpd3DModel = new cPointData(icLoadPoint.ipPoint);

            this.vPointDataErrorCheck(icPointData);

            if (pgbMain != null)
                this.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// 展開係数からPointDataを作成します。
        /// </summary>
        protected virtual void vCoefficientToPart()
        {
            #region 展開係数→Part
            //3次元形状＝固有ベクトル＊展開係数＋平均ベクトル
            icvPart = new ColumnVector(imEigenMtx * icvCoefficient + icvAverageVec);

            if (pgbMain != null)
                this.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// Partから展開係数を作成します。
        /// </summary>
        protected virtual void vPartToCoefficient()
        {
            #region Part→展開係数
            icvCoefficient = new ColumnVector(imEigenMtx.ColSize);

            //展開係数＝（３次元形状－平均ベクトル）・固有ベクトル：「・」は内積
            for (int i = 0; i < icvCoefficient.Length; i++)
                icvCoefficient[i] = (icvPart - icvAverageVec).InnerProduct(imEigenMtx.GetColVector(i));

            if (pgbMain != null)
                this.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// PointDataからBitmapを作成します。
        /// </summary>
        protected virtual void vPointDataToBitmap()
        {
            #region PointData→Bitmap
            bmpData = new cCreateBitmapFrom3DPointFast(icPointData, 5).Bitmap;
            
            if (pgbMain != null)
                this.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// 展開係数から画像を作成します。
        /// </summary>
        protected virtual void vCoefficientToBitmap()
        {
            #region 展開係数→Bitmap
            if(pgbMain != null)
                pgbMain.Maximum += 2;

            this.vCoefficientToPart();
            this.vPartToPointData();
            this.vPointDataToBitmap();
            #endregion
        }

        /// <summary>
        /// PointDataからＸＹＺＲＧＢベクトルを作成します。
        /// </summary>
        protected virtual ColumnVector cvPointDataToVector()
        {
            #region PointData→XYZRGBベクトル
            ColumnVector icvRe = new ColumnVector(icPointData.Length * 6);

            for (int i = 0; i < icPointData.Length; i++)
            {
                icvRe[i * 6 + 0] = icPointData.Items[i].X;
                icvRe[i * 6 + 1] = icPointData.Items[i].Y;
                icvRe[i * 6 + 2] = icPointData.Items[i].Z;
                icvRe[i * 6 + 3] = icPointData.Items[i].R;
                icvRe[i * 6 + 4] = icPointData.Items[i].G;
                icvRe[i * 6 + 5] = icPointData.Items[i].B;
            }

            if (pgbMain != null)
                this.vProgressBarValueUp();

            return icvRe;
            #endregion
        }

        /// <summary>
        /// 展開係数のサイズが一致しているか確認します。
        /// </summary>
        /// <param name="cvCheck">チェック対象のデータ</param>
        protected virtual void vCoefficientErrorCheck(ColumnVector cvCheck)
        {
            #region 展開係数のエラーチェック
            if (cvCheck.Length != imEigenMtx.ColSize )
                throw new ApplicationException("ベクトルのサイズが違います");
            #endregion
        }

        /// <summary>
        ///Partののサイズが一致しているか確認します。
        /// </summary>
        /// <param name="cvCheck">チェック対象のデータ</param>
        protected virtual void vPartErrorCheck(ColumnVector cvCheck)
        {
            #region Partのエラーチェック
            if (cvCheck.Length != icvAverageVec.Length)
                throw new ApplicationException("ベクトルのサイズが違います");
            #endregion
        }

        #region 未実装(必ずoverrideしてください)
        /// <summary>
        /// ３次元形状からPointDataを作成するメソッドを必ず実装しておいてください。
        /// </summary>
        protected virtual void vPartToPointData() { throw new NotImplementedException(); }

        /// <summary>
        /// PointDataから３次元形状を作成するメソッドを必ず実装しておいてください。
        /// </summary>
        protected virtual void vPointDataToPart() { throw new NotImplementedException(); }
        
        /// <summary>
        ///PointDataの次元数が一致しているかチェックするメソッドを必ず実装しておいてください。
        /// </summary>
        /// <param name="cpdCheck">チェック対象のデータ</param>
        protected virtual void vPointDataErrorCheck(cPointData cpdCheck) { throw new NotImplementedException(); }
        #endregion

        #region プログレスバー関連
        /// <summary>
        /// プログレスバーの値を増加させます。
        /// </summary>
        protected virtual void vProgressBarValueUp()
        {
            if (pgbMain != null)
            {
                pgbMain.PerformStep();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// プログレスバーをリセットします。
        /// </summary>
        /// <param name="iMax">最大値</param>
        protected virtual void vProgressBarReset(int iMax)
        {
            if (pgbMain != null)
            {
                pgbMain.Maximum = iMax;
                pgbMain.Minimum = 0;
                pgbMain.Value = 0;
                pgbMain.Step = 1;
            }
        }

        /// <summary>
        /// プログレスバーの設定をします。
        /// </summary>
        public virtual ToolStripProgressBar pgbProgressBar
        {
            set { pgbMain = value; }
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// PointDataを設定、取得します。
        /// </summary>
        public virtual cPointData cpd3DModel
        {
            get { return new cPointData(icPointData); }
            set
            {
                this.vPointDataErrorCheck(value);

                icPointData = new cPointData(value);

                this.vProgressBarReset(3);
                this.vPointDataToPart();
                this.vPartToCoefficient();
                this.vCoefficientToBitmap();                
            }
        }

        /// <summary>
        ///展開係数を設定、取得します。
        /// </summary>
        public virtual ColumnVector cvCoefficient_Value
        {
            get { return new ColumnVector(icvCoefficient); }
            set 
            {
                this.vCoefficientErrorCheck(value);

                icvCoefficient = new ColumnVector(value);

                this.vProgressBarReset(3);
                this.vCoefficientToPart();
                this.vPartToPointData();
                this.vPointDataToBitmap();
            }
        }

        /// <summary>
        /// Bitmap画像を取得します。
        /// </summary>
        public virtual Bitmap bmpBitmap
        {
            get { return new Bitmap(bmpData); }

            //overrideの時のために一応setも作っておく
            set { }
        }

        /// <summary>
        /// ３次元形状の一部分(XYZ又はRGB又は画像の色）のベクトルを設定、取得します。
        /// </summary>
        public virtual ColumnVector cvPart
        {
            get { return new ColumnVector(icvPart); }
            set
            {
                this.vPartErrorCheck(value);

                icvPart = new ColumnVector(value);

                this.vProgressBarReset(3);
                this.vPartToCoefficient();
                this.vPartToPointData();
                this.vPointDataToBitmap();
            }
        }

        /// <summary>
        /// XYZRGBが並んだベクトルを取得します。
        /// </summary>
        public virtual ColumnVector cv3DModel
        {
            get
            {
                this.vProgressBarReset(1);
                return this.cvPointDataToVector();
            }
        }
        #endregion
    }
}
