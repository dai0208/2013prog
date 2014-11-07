using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MatrixVector;
using _2ClassSVMUtil;
using DoPCA;

namespace SVMClassifierWithEyeMotion
{
    class myClassifier
    {
        PCAData PCAData_High_Newtral;
        PCAData PCAData_Newtral_Low;
        twoClassSVMUtil High_Newtral_SVM;
        twoClassSVMUtil Newtral_Low_SVM;
        private Matrix High_from_High_Newtral; //教師用
        private Matrix Newtral_from_High_Newtral; //教師用
        private Matrix Newtral_from_Newtral_Low; //教師用
        private Matrix Low_from_Newtral_Low; //教師用


        /// <summary>
        /// コンストラクタ。それぞれの混成マトリクスは必ず縦ベクトルの集合のものを使用してください。
        /// </summary>
        /// <param name="MtxPath_High_Newtral">High→Newtralの順に並んだ混成マトリクス</param>
        /// <param name="MtxPath_Newtral_Low">Newtral→Lowの順に並んだ混成マトリクス</param>
        public myClassifier(PCAData PCAData_High_Newtral, PCAData PCAData_Newtral_Low)
        {
            this.PCAData_High_Newtral = PCAData_High_Newtral;
            this.PCAData_Newtral_Low = PCAData_Newtral_Low;
        }


        /// <summary>
        /// 指定したファイルのXYZデータからベクトルを作成します
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>XYZベクトルデータ</returns>
        public Vector[] GetVectorFromBitmap(string[] filePath)
        {
            Vector[] ReVector = new Vector[filePath.Length];
            for (int i = 0; i < filePath.Length; i++)
            {
                double[] Data;
                Bitmap bmpData;
                try
                {
                    bmpData = new Bitmap(filePath[i]);
                    Data = new double[bmpData.Width * bmpData.Height * 3];
                    int iStride = bmpData.Width;
                    for (int iHeight = 0; iHeight < bmpData.Height; iHeight++)
                        for (int iWidth = 0; iWidth < bmpData.Width; iWidth++)
                        {
                            Data[iWidth * 3 + iHeight * iStride + 0] = bmpData.GetPixel(iWidth, iHeight).R;
                            Data[iWidth * 3 + iHeight * iStride + 1] = bmpData.GetPixel(iWidth, iHeight).G;
                            Data[iWidth * 3 + iHeight * iStride + 2] = bmpData.GetPixel(iWidth, iHeight).B;
                        }

                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message);
                }

                ReVector[i] = new Vector(Data);
                ReVector[i].TagData = bmpData.Size;
            }
            return ReVector;
        }

        /*
        public void setTestData(Image EyeImage)
        {
            Bitmap TmpImg = new Bitmap(EyeImage);
            TestData = new ColumnVector(EyeImage.Width * EyeImage.Height * 3);
            
            //横スキャンで画素をベクトル化します。
            for (int iHeight = 0; iHeight < EyeImage.Height; iHeight++)
                for (int iWidth = 0; iWidth < EyeImage.Width; iWidth++)
                {
                    this.TestData[iWidth * 3 + iHeight * EyeImage.Width + 0] = TmpImg.GetPixel(iWidth, iHeight).R;
                    this.TestData[iWidth * 3 + iHeight * EyeImage.Width + 1] = TmpImg.GetPixel(iWidth, iHeight).G;
                    this.TestData[iWidth * 3 + iHeight * EyeImage.Width + 2] = TmpImg.GetPixel(iWidth, iHeight).B;
                }
        }*/

        /// <summary>
        /// PCAで得られたデータでSVMの学習を行います。
        /// </summary>
        public void Learning(int sizeHigh_Neutral_High, int sizeNeutral_Low_Neutral)
        {
            /* Step1.High-Newtralの教師データの展開係数を分割して上から５個取得*/
            High_from_High_Newtral = PCAData_High_Newtral.Coefficient;
            this.High_from_High_Newtral = High_from_High_Newtral.GetMatrixRowFromStart(4);
            this.Newtral_from_High_Newtral = High_from_High_Newtral.GetMatrixColumnToEnd(sizeHigh_Neutral_High);
            this.High_from_High_Newtral = High_from_High_Newtral.GetMatrixColumnFromStart(sizeHigh_Neutral_High - 1);

            /* Step2.Newtral-lowの教師データの展開係数を分割して上から５個取得*/
            Newtral_from_Newtral_Low = PCAData_Newtral_Low.Coefficient;
            this.Newtral_from_Newtral_Low = Newtral_from_Newtral_Low.GetMatrixRowFromStart(4);
            this.Low_from_Newtral_Low = Newtral_from_Newtral_Low.GetMatrixColumnToEnd(sizeNeutral_Low_Neutral);
            this.Newtral_from_Newtral_Low = Newtral_from_Newtral_Low.GetMatrixColumnFromStart(sizeNeutral_Low_Neutral - 1);


            /* Step3.決定境界の算出。結果はモデルデータとして各オブジェクト内に保持されます。*/
            SVMManager svmmanager = new SVMManager();

            High_Newtral_SVM = new twoClassSVMUtil();
            High_Newtral_SVM.TrueTeacherDatas = High_from_High_Newtral;
            High_Newtral_SVM.FalseTeacherDatas = Newtral_from_High_Newtral;
            High_Newtral_SVM.DoTrainingOnly();

            Newtral_Low_SVM = new twoClassSVMUtil();
            Newtral_Low_SVM.TrueTeacherDatas = Newtral_from_Newtral_Low;
            Newtral_Low_SVM.FalseTeacherDatas = Low_from_Newtral_Low;
            Newtral_Low_SVM.DoTrainingOnly();
        }

        /// <summary>
        /// 結果の出力を行います。Highのときは1，Neutralのときは0，Lowのときは-1を返します。
        /// </summary>
        /// <returns></returns>
        public int PlotandGetResult(Vector TestData)
        {
            EigenSystem EigenHN = PCAData_High_Newtral.EigenSystem;
            ColumnVector DataforPCASpace_High_Newtral = new ColumnVector(5);
            for (int i = 0; i < DataforPCASpace_High_Newtral.Length; i++)
            {
                DataforPCASpace_High_Newtral[i] = EigenHN.GetEigenVectors().GetColVector(i).InnerProduct(TestData - PCAData_High_Newtral.Average);
            }

            EigenSystem EigenNL = PCAData_Newtral_Low.EigenSystem;
            ColumnVector DataforPCASpace_Newtral_Low = new ColumnVector(5);
            for (int i = 0; i < DataforPCASpace_Newtral_Low.Length; i++)
            {
                DataforPCASpace_Newtral_Low[i] = EigenNL.GetEigenVectors().GetColVector(i).InnerProduct(TestData - PCAData_Newtral_Low.Average);
            }
            

            High_Newtral_SVM.UnknownData = DataforPCASpace_High_Newtral;
            High_Newtral_SVM.setUnknownData_Node();
            High_Newtral_SVM.RunSVM();
            High_Newtral_SVM.PrintResult();
            double[] a = High_Newtral_SVM.Problem;//結果チェック用

            Newtral_Low_SVM.UnknownData = DataforPCASpace_Newtral_Low;
            Newtral_Low_SVM.setUnknownData_Node();
            Newtral_Low_SVM.RunSVM();
            Newtral_Low_SVM.PrintResult();
            double[] b = Newtral_Low_SVM.Problem;//結果チェック用

            if ((High_Newtral_SVM.Problem[0] < Newtral_Low_SVM.Problem[0])&&(Newtral_Low_SVM.Problem[0] > Newtral_Low_SVM.Problem[1])) return -1;
            if ((High_Newtral_SVM.Problem[1] > Newtral_Low_SVM.Problem[1])&&(High_Newtral_SVM.Problem[0] < High_Newtral_SVM.Problem[1])) return 1;
            else return 0;
        }
    }
}
