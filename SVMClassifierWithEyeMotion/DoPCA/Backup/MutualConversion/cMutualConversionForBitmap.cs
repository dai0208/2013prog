using PointFormat;
using MatrixVector;
using System.Drawing;
using System;

namespace MutualConversion
{
    /// <summary>
    /// 画像を主成分分析を行ったときに使用します。
    /// ascファイル、cPointData、展開係数の相互変換を行い、それらからBitmapを作成するクラスです。
    /// </summary>
    public class cMutualConversionForBitmap : cMutualConversionWithReferenceValue
    {
        #region プライベート変数
        /// <summary>
        /// 画像の横幅
        /// </summary>
        protected int iWidth;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 固有ベクトル・平均ベクトル・展開係数・画像の横幅をセットします。
        /// </summary>
        /// <param name="mEigenMtx">固有ベクトル</param>
        /// <param name="cvAverageVec">平均ベクトル</param>
        /// <param name="mCoefficients">展開係数</param>
        /// <param name="iWidth">画像の横幅</param>
        public cMutualConversionForBitmap(Matrix mEigenMtx, ColumnVector cvAverageVec, Matrix mCoefficients, int iWidth)
            : base(mEigenMtx, cvAverageVec, mCoefficients)
        {
            this.iWidth = iWidth;
        }
        #endregion

        /// <summary>
        /// BitmapからからPointDataを作成します。
        /// </summary>
        protected override void vPartToPointData()
        {
            #region Part→PointData
            cPoint[] icarPoint = new cPoint[icvPart.Length];

            for (int i = 0; i < icarPoint.Length; i++)
            {
                icarPoint[i] = new cPoint();

                icarPoint[i].X = i % iWidth;
                icarPoint[i].Y = -1 * i / iWidth;
                icarPoint[i].Z = 0;
                icarPoint[i].R = (int)icvPart[i];
                icarPoint[i].G = (int)icvPart[i];
                icarPoint[i].B = (int)icvPart[i];
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
            icvPart = new ColumnVector(icPointData.Length);

            for (int i = 0; i < icPointData.Length; i++)
            {
                int iR = icPointData.Items[i].R;
                int iG = icPointData.Items[i].G;
                int iB = icPointData.Items[i].B;

                icvPart[i] = (iR + iG + iB) / 3d;               
            }

            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// BitmapからPointDataを作成します。
        /// </summary>
        protected virtual void vBitmapToPointData()
        {
            #region Bitmap→PointData
            cPoint[] icPoint = new cPoint[bmpData.Height * bmpData.Width];
            int iCount = 0;

            for (int iY = 0; iY < bmpData.Height; iY++)
                for (int iX = 0; iX < bmpData.Width; iX++)
                {
                    Color clrNow = bmpData.GetPixel(iX, iY);

                    icPoint[iCount] = new cPoint();

                    icPoint[iCount].X = iX;
                    icPoint[iCount].Y = -1 * iY;
                    icPoint[iCount].Z = 0;
                    icPoint[iCount].R = clrNow.R;
                    icPoint[iCount].G = clrNow.G;
                    icPoint[iCount].B = clrNow.B;

                    iCount++;
                }

            icPointData = new cPointData(icPoint);

            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }
        
        /// <summary>
        /// PointDataからBitmapを作成します。
        /// </summary>
        protected override void vPointDataToBitmap()
        {
            #region PointData→Bitmap
            bmpData = new Bitmap(iWidth, icPointData.Length / iWidth);

            for (int i = 0; i < icPointData.Length; i++)
            {
                int iX = (int)icPointData.Items[i].X;
                int iY = -1 * (int)icPointData.Items[i].Y;
                int iR = icPointData.Items[i].R;
                int iG = icPointData.Items[i].G;
                int iB = icPointData.Items[i].B;

                bmpData.SetPixel(iX, iY, Color.FromArgb(iR, iG, iB));
            }


            if (pgbMain != null)
                base.vProgressBarValueUp();
            #endregion
        }

        /// <summary>
        /// Bitmapのサイズが一致しているか確認します。
        /// </summary>
        /// <param name="bmpCheck">チェック対象のデータ</param>
        protected virtual void vBitmapErrorCheck(Bitmap bmpCheck)
        {
            #region Bitmapのエラーチェック
            if (bmpCheck.Width * bmpCheck.Height != icvAverageVec.Length)
                throw new ApplicationException("ベクトルのサイズが違います");
            #endregion
        }

        protected override void vPointDataErrorCheck(cPointData cpdCheck)
        {
            #region PointDataのエラーチェック
            if (cpdCheck.Length * 3 != icvAverageVec.Length)
                throw new ApplicationException("データのサイズが違います");
            #endregion
        }        

        #region プロパティ
        /// <summary>
        /// Bitmap画像を設定、取得します。
        /// </summary>
        public override Bitmap bmpBitmap
        {
            get { return new Bitmap(bmpData); }

            set
            {
                bmpData = new Bitmap(value);

                this.vProgressBarReset(4);
                this.vBitmapToPointData();
                this.vPointDataToPart();
                base.vPartToCoefficient();
                base.vCoefficientToBitmap();
            }
        }
        #endregion
    }
}
