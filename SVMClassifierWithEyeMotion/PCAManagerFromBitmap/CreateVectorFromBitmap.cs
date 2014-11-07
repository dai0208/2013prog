using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MatrixVector;

namespace DoPCA
{
    public class CreateVectorFromBitmap
    {
        public CreateVectorFromBitmap()
        {
        }

        /// <summary>
        /// 指定したファイルのXYZデータからベクトルを作成します
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>XYZベクトルデータ</returns>
        public static Vector GetVectorFromBitmap(string FileName)
        {
            double[] Data;
            Bitmap bmpData;
            try
            {
                bmpData = new Bitmap(FileName);
                Data = new double[bmpData.Width * bmpData.Height * 3];
                int iStride = bmpData.Width;
                for(int iHeight=0;iHeight< bmpData.Height;iHeight++)
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

            Vector ReVector = new Vector(Data);
            ReVector.TagData = bmpData.Size;

            return ReVector;
        }


    }
}
