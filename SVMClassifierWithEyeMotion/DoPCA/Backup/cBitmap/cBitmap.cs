using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace cBitmap
{
    /// <summary>
    /// Bitmap処理関係をまとめたクラスです。
    /// </summary>
    public class cBitmap
    {
        /// <summary>
        /// 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
        /// </summary>
        /// <param name="bmpSent">指定したBitmap画像</param>
        /// <param name="iX">横幅</param>
        /// <param name="iY">高さ</param>
        /// <returns>変形されたBitmap画像</returns>
        static public Bitmap bmpStretchImage(Bitmap bmpSent, int iWidth, int iHeight)
        {
            #region 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
            if (bmpSent.Width == iWidth && bmpSent.Height == iHeight)
                return bmpSent;

            Bitmap bmpWork = new Bitmap(iWidth, iHeight);
            float VRatio, HRatio, Ratio;

            VRatio = (float)iWidth / (float)bmpSent.Width;
            HRatio = (float)iHeight / (float)bmpSent.Height;

            Ratio = Math.Min(VRatio, HRatio);

            int X = (int)(iWidth - bmpSent.Width * Ratio) / 2;
            int Y = (int)(iHeight - bmpSent.Height * Ratio) / 2;

            Graphics gDraw = Graphics.FromImage(bmpWork);
            gDraw.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gDraw.DrawImage(bmpSent, X, Y, bmpSent.Width * Ratio, bmpSent.Height * Ratio);
            gDraw.Flush();
            gDraw.Dispose();

            return bmpWork;
            #endregion
        }


        /// <summary>
        /// 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
        /// </summary>
        /// <param name="bmpSent">指定したBitmap画像</param>
        /// <param name="iX">横幅</param>
        /// <param name="BackColor">背景色</param>
        /// <returns>変形されたBitmap画像</returns>
        static public Bitmap bmpStretchImage(Bitmap bmpSent, int iWidth, int iHeight, Color BackColor)
        {
            #region 指定されたBitmapを指定した大きさに"綺麗に"変形してセンタリングするメソッド
            if (bmpSent.Width == iWidth && bmpSent.Height == iHeight)
                return bmpSent;

            Bitmap bmpWork = new Bitmap(iWidth, iHeight);
            float VRatio, HRatio, Ratio;

            VRatio = (float)iWidth / (float)bmpSent.Width;
            HRatio = (float)iHeight / (float)bmpSent.Height;

            Ratio = Math.Min(VRatio, HRatio);

            int X = (int)(iWidth - bmpSent.Width * Ratio) / 2;
            int Y = (int)(iHeight - bmpSent.Height * Ratio) / 2;

            Graphics gDraw = Graphics.FromImage(bmpWork);
            gDraw.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, iWidth, iHeight));
            gDraw.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gDraw.DrawImage(bmpSent, X, Y, bmpSent.Width * Ratio, bmpSent.Height * Ratio);
            gDraw.Flush();
            gDraw.Dispose();

            return bmpWork;
            #endregion
        }
    }
}
