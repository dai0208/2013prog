using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DoPCA;
using MutualConversion;
using MatrixVector;
using System.Drawing;
using PointFormat;

namespace RestructurePCA
{
    public delegate cMutualConversionWithReferenceValue LoadPCADataMethod(PCAData PCAData);

    public class RestructPCAManager
    {
        #region メンバ変数
        /// <summary>
        /// PCAのデータ
        /// </summary>
        protected PCAData PCAData;

        /// <summary>
        /// ascファイル、cPointData、展開係数の相互変換を行い、それらからBitmapを作成するためのインスタンス
        /// </summary>
        protected cMutualConversionWithReferenceValue MutualConversion;

        /// <summary>
        /// 特定形式のファイルを読み込むためのデリゲート（通常必要ありません）
        /// </summary>
        protected LoadPCADataMethod LoadPCADataMethod;

        /// <summary>
        /// 指定されたパラメータサイズの固有ベクトル
        /// </summary>
        protected Matrix EigenVectorMatrix;

        /// <summary>
        /// 展開係数リスト
        /// </summary>
        protected List<ColumnVector> CoefficientList;

        /// <summary>
        /// 標準得点リスト
        /// </summary>
        protected List<ColumnVector> ReferenceList;

        /// <summary>
        /// 画像のリスト
        /// </summary>
        public List<Bitmap> ImageList;

        /// <summary>
        /// 平均ベクトル
        /// </summary>
        protected ColumnVector AverageVector;

        /// <summary>
        /// データソース
        /// </summary>
        protected PCASource PCASource;

        /// <summary>
        /// プログレスバー
        /// </summary>
        protected System.Windows.Forms.ToolStripProgressBar ProgressBar;

        /// <summary>
        /// 情報ウィンドウ
        /// </summary>
        fmInfo ifmInfo;
        #endregion

        #region コンストラクタ
        protected RestructPCAManager()
        {
        }

        public RestructPCAManager(string FileName, ToolStripProgressBar ProgressBar)
        {
            PCAData = PCAData.DataLoad(FileName);
            this.ProgressBar = ProgressBar;
        }

        public RestructPCAManager(PCAData PCAData, ToolStripProgressBar ProgressBar, LoadPCADataMethod LoadPCADataMethod)
            : this(PCAData, ProgressBar)
        {
            this.LoadPCADataMethod = LoadPCADataMethod;
        }

        public RestructPCAManager(PCAData PCAData, ToolStripProgressBar ProgressBar)
        {
            this.PCAData = PCAData;
            this.ProgressBar = ProgressBar;
        }
        #endregion

        /// <summary>
        /// 最大のパラメータサイズでMutualConversionを作成します
        /// </summary>
        public virtual void CreatMutualConversion()
        {
            this.CreatMutualConversion(PCAData.ParamCount);
        }

        /// <summary>
        /// 固有ベクトル、展開係数を指定されたパラメータサイズにして、それらの値からMutualConversionを作成します
        /// </summary>
        /// <param name="ParamQuantity">使用するパラメータの個数</param>
        public virtual void CreatMutualConversion(int ParamQuantity)
        {
            #region MutualConversion作成
            if (ParamQuantity > PCAData.DataCount)
                throw new ApplicationException("指定したパラメータの個数が多すぎます");

            SetProgress(PCAData.DataCount * 2 + ParamQuantity);
            this.AverageVector = new ColumnVector(PCAData.Average);
            this.PCASource = PCAData.DataType;

            ///固有ベクトルを作成(固有ベクトルのサイズは後でデータを追加しても変わらないのでここで計算)
            {
                Vector[] EigenVectors = new Vector[ParamQuantity];
                for (int i = 0; i < ParamQuantity; i++)
                {
                    StepProgress();
                    EigenVectors[i] = PCAData.EigenSystem[i].EigenVector;
                }
                EigenVectorMatrix = new Matrix(EigenVectors);
            }

            ///展開係数のリストを作成（展開係数は後でデータを追加したときに大きさが変動するのでリストで保持）
            {
                CoefficientList = new List<ColumnVector>();
                for (int i = 0; i < PCAData.DataCount; i++)
                {
                    StepProgress();
                    ColumnVector CoefficientVector = new ColumnVector(ParamQuantity);
                    for (int j = 0; j < ParamQuantity; j++)
                    {
                        CoefficientVector[j] = PCAData.Coefficient[j, i];
                    }
                    CoefficientList.Add(CoefficientVector);
                }
            }

            switch (PCAData.DataType)
            {
                case PCASource.Bitmap:
                    Size ImageSize = (Size)PCAData.DataTag;
                    this.MutualConversion = new cMutualConversionForBitmap(EigenVectorMatrix, AverageVector, CoefficientMatrix, ImageSize.Width);
                    break;
                case PCASource.AsciiDataShapeOnly:
                    this.MutualConversion = new cMutualConversionForShape(EigenVectorMatrix, AverageVector, CoefficientMatrix);
                    break;
                case PCASource.AsciiDataTextureOnly:
                    this.MutualConversion = new cMutualConversionForShape(EigenVectorMatrix, AverageVector, CoefficientMatrix);
                    break;
                case PCASource.AsciiDataShapeAndTexture:
                    this.MutualConversion = new cMutualConversionForShapeAndColor(EigenVectorMatrix, AverageVector, CoefficientMatrix);
                    break;
                default:
                    try { this.MutualConversion = LoadPCADataMethod(PCAData); }
                    catch { throw new ApplicationException("サポートされていない型が呼ばれました。"); }
                    break;
            }

            //標準得点と画像のリストを作成
            {
                ReferenceList = new List<ColumnVector>();
                ImageList = new List<Bitmap>();
                for (int i = 0; i < DataCount; i++)
                {
                    StepProgress();
                    Application.DoEvents();
                    MutualConversion.cvCoefficient_Value = CoefficientList[i];
                    ReferenceList.Add(MutualConversion.cvReference_Value);
                    ImageList.Add(MutualConversion.bmpBitmap);
                }
            }
            #endregion
        }

        public virtual void ShowInfomationWindow()
        {
            if (ifmInfo != null)
                ifmInfo.Dispose();

            ifmInfo = new fmInfo(this);
            ifmInfo.Show();
        }

        protected virtual void SetList(int Index)
        {
            ReferenceList[Index] = MutualConversion.cvReference_Value;
            CoefficientList[Index] = MutualConversion.cvCoefficient_Value;
            ImageList[Index] = MutualConversion.bmpBitmap;
        }

        protected virtual void AddList()
        {
            ReferenceList.Add(MutualConversion.cvReference_Value);
            CoefficientList.Add(MutualConversion.cvCoefficient_Value);
            ImageList.Add(MutualConversion.bmpBitmap);
        }

        public virtual bool Save(string FileName)
        {
            PCAData SavePCAData = new PCAData(this.PCASource, this.PCAData.EigenSystem, new Matrix(CoefficientList.ToArray()), this.AverageVector, this.PCAData.DataTag);
            return SavePCAData.DataSave(FileName);
        }

        #region Set系
        /// <summary>
        /// 展開係数を指定したインデックス位置に設定
        /// </summary>
        /// <param name="CoefficientVector">展開係数</param>
        /// <param name="Index">インデックス</param>
        public virtual void SetCoefficient(ColumnVector CoefficientVector, int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            MutualConversion.cvCoefficient_Value = CoefficientVector;
            this.SetList(Index);
        }

        /// <summary>
        /// 標準得点を現在のリストのインデックス位置に登録
        /// </summary>
        /// <param name="ReferenceValue">標準得点</param>
        /// <param name="Index">インデックス</param>
        public virtual void SetReferenceValue(ColumnVector ReferenceValue, int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            MutualConversion.cvReference_Value = ReferenceValue;
            this.SetList(Index);
        }

        public virtual void SetVectorData(ColumnVector Vector, int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            MutualConversion.cvPart = Vector;
            this.SetList(Index);
        }
        #endregion

        #region Add系
        /// <summary>
        /// 展開係数で現在のデータリストに追加
        /// </summary>
        /// <param name="CoefficientVector">展開係数</param>
        public virtual void AddCoefficient(ColumnVector CoefficientVector)
        {
            MutualConversion.cvCoefficient_Value = CoefficientVector;
            this.AddList();
        }

        /// <summary>
        /// 標準得点で現在のデータリストに追加
        /// </summary>
        /// <param name="ReferenceValue">標準得点</param>
        public virtual void AddReferenceValue(ColumnVector ReferenceValue)
        {
            MutualConversion.cvReference_Value = ReferenceValue;
            this.AddList();
        }

        /// <summary>
        /// ベクトルデータで現在のデータリストに追加
        /// </summary>
        /// <param name="Vector">ベクトルデータ</param>
        public virtual void AddVectorData(ColumnVector Vector)
        {
            MutualConversion.cvPart = Vector;
            this.AddList();
        }

        /// <summary>
        /// 3DPointDataで現在のデータリストに追加
        /// </summary>
        /// <param name="PointData">3DPointData</param>
        public virtual void Add3DPoint(cPointData PointData)
        {
            MutualConversion.cpd3DModel = PointData;
            this.AddList();
        }
        #endregion
        
        #region Get系
        /// <summary>
        /// 指定したインデックスの画像を返します
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>再構築されたBitmap</returns>
        public virtual Bitmap GetBitmap(int Index)
        {
            return GetBitmap(Index, new Size(128, 128));
        }

        /// <summary>
        /// 指定したインデックスの画像を指定した大きさで返します
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <param name="Size">画像のサイズ</param>
        /// <returns>再構築されたBitmap</returns>
        public virtual Bitmap GetBitmap(int Index, Size Size)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            Bitmap ReturnBitmap = cBitmap.cBitmap.bmpStretchImage(ImageList[Index], Size.Width, Size.Height);
            return ReturnBitmap;
        }

        /// <summary>
        /// 指定したインデックスのベクトルデータを取得
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>ベクトルデータ</returns>
        public virtual Vector GetVectorData(int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            MutualConversion.cvCoefficient_Value = CoefficientList[Index];
            return MutualConversion.cvPart;
        }

        /// <summary>
        /// 指定したインデックスの3DPointDataを取得します。
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>3DPointData</returns>
        public virtual cPointData GetPointData(int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            MutualConversion.cvCoefficient_Value = CoefficientList[Index];
            return MutualConversion.cpd3DModel;
        }

        /// <summary>
        /// 指定したインデックスのパラメータを取得します。
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>パラメータのベクトル</returns>
        public virtual ColumnVector GetParamData(int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            return new ColumnVector(CoefficientList[Index]);
        }

        /// <summary>
        /// 指定したインデックスの標準得点を取得します。
        /// </summary>
        /// <param name="Index">インデックス</param>
        /// <returns>標準得点のベクトル</returns>
        public virtual ColumnVector GetReferenceValue(int Index)
        {
            if (Index > this.DataCount)
                throw new ApplicationException("パラメータの範囲を超えています。");

            return ReferenceList[Index];
        }

        /// <summary>
        /// 指定したサイズのイメージリストを取得します。
        /// </summary>
        /// <param name="Size">画像のサイズ</param>
        /// <returns>イメージリスト</returns>
        public virtual ImageList GetImageList(Size Size)
        {
            ImageList ImageList = new ImageList();
            ImageList.ImageSize = Size;

            for (int i = 0; i < CoefficientList.Count; i++)
                ImageList.Images.Add(GetBitmap(i, Size));

            return ImageList;
        }

        /// <summary>
        /// イメージリストを取得します
        /// </summary>
        /// <returns>イメージリスト</returns>
        public virtual ImageList GetImageList()
        {
            return GetImageList(new Size(256, 256));
        }

        /// <summary>
        /// 現在のMutualConversionを取得します。
        /// </summary>
        /// <returns>MutualConversion</returns>
        public virtual cMutualConversionWithReferenceValue GetMutualConversion()
        {
            //TODO ちゃんとしたコピーを返した方が良い気がする
            return this.MutualConversion;
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 現在のパラメータサイズの展開係数行列を取得
        /// </summary>
        public virtual Matrix CoefficientMatrix
        {
            get
            {
                Vector[] CoefficientVectors = new Vector[CoefficientList.Count];
                for (int i = 0; i < CoefficientList.Count; i++)
                    CoefficientVectors[i] = CoefficientList[i];
                return new Matrix(CoefficientVectors);
            }
        }

        /// <summary>
        /// 現在のデータの個数を取得します
        /// </summary>
        public virtual int DataCount
        {
            get { return this.CoefficientList.Count; }
        }

        /// <summary>
        /// 現在のパラメータの個数を取得します
        /// </summary>
        public virtual int ParamCount
        {
            get { return this.EigenVectorMatrix.ColSize; }
        }

        /// <summary>
        /// 現在のベクトルサイズを取得します
        /// </summary>
        public virtual int VectorLength
        {
            get { return this.EigenVectorMatrix.RowSize; }
        }

        /// <summary>
        /// 現在のパラメータサイズの標準得点行列を取得
        /// </summary>
        public virtual Matrix ReferenceMatrix
        {
            get
            {
                ColumnVector[] ReferenceVector = new ColumnVector[CoefficientList.Count];
                for (int i = 0; i < CoefficientList.Count; i++)
                    ReferenceVector[i] = ReferenceList[i];
                return new Matrix(ReferenceVector);
            }
        }

        /// <summary>
        /// データの種類を取得します
        /// </summary>
        public virtual PCASource DataType
        {
            get { return this.PCASource; }
        }

        /// <summary>
        /// パラメータの最大値を取得します
        /// </summary>
        public virtual int MaxParamCount
        {
            get { return PCAData.ParamCount; }
        }
        #endregion

        #region プログレスバー処理関係
        /// <summary>
        /// プログレスバーの設定
        /// </summary>
        /// <param name="MaxValue"></param>
        protected void SetProgress(int MaxValue)
        {
            if (ProgressBar == null)
                return;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = MaxValue;
        }

        /// <summary>
        /// プログレスバーを増やす処理
        /// </summary>
        protected void StepProgress()
        {
            if (ProgressBar == null)
                return;
            ProgressBar.Value++;
            Application.DoEvents();
        }
        #endregion
    }
}
