﻿namespace PairedComparison
{
    partial class fmBack
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbxLeft = new System.Windows.Forms.PictureBox();
            this.pbxRight = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblCurrentImage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRight)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxLeft
            // 
            this.pbxLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbxLeft.Location = new System.Drawing.Point(128, 162);
            this.pbxLeft.Name = "pbxLeft";
            this.pbxLeft.Size = new System.Drawing.Size(192, 192);
            this.pbxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLeft.TabIndex = 0;
            this.pbxLeft.TabStop = false;
            this.pbxLeft.Click += new System.EventHandler(this.pbxLeft_Click);
            // 
            // pbxRight
            // 
            this.pbxRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbxRight.Location = new System.Drawing.Point(350, 162);
            this.pbxRight.Name = "pbxRight";
            this.pbxRight.Size = new System.Drawing.Size(192, 192);
            this.pbxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxRight.TabIndex = 1;
            this.pbxRight.TabStop = false;
            this.pbxRight.Click += new System.EventHandler(this.pbxRight_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(51, 21);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 16);
            this.lblMessage.TabIndex = 2;
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Location = new System.Drawing.Point(83, 82);
            this.lblMessage2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(0, 12);
            this.lblMessage2.TabIndex = 3;
            // 
            // lblCurrentImage
            // 
            this.lblCurrentImage.AutoSize = true;
            this.lblCurrentImage.Location = new System.Drawing.Point(328, 126);
            this.lblCurrentImage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrentImage.Name = "lblCurrentImage";
            this.lblCurrentImage.Size = new System.Drawing.Size(0, 12);
            this.lblCurrentImage.TabIndex = 4;
            // 
            // fmBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(668, 516);
            this.Controls.Add(this.lblCurrentImage);
            this.Controls.Add(this.lblMessage2);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pbxRight);
            this.Controls.Add(this.pbxLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "fmBack";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmBack_FormClosing);
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fmBack_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxLeft;
        private System.Windows.Forms.PictureBox pbxRight;
        private System.Windows.Forms.Label lblMessage;
        protected System.Windows.Forms.Label lblMessage2;
        protected System.Windows.Forms.Label lblCurrentImage;
    }
}

