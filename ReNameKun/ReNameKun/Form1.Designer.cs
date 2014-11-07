namespace ReNameKun
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.rdbAttachHead = new System.Windows.Forms.RadioButton();
            this.rdbAttachTale = new System.Windows.Forms.RadioButton();
            this.tbxAttachInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstFile = new System.Windows.Forms.ListBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.rdbDelete = new System.Windows.Forms.RadioButton();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.rdbPermutation = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rdbAttachHead
            // 
            this.rdbAttachHead.AutoSize = true;
            this.rdbAttachHead.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAttachHead.Location = new System.Drawing.Point(12, 12);
            this.rdbAttachHead.Name = "rdbAttachHead";
            this.rdbAttachHead.Size = new System.Drawing.Size(113, 25);
            this.rdbAttachHead.TabIndex = 0;
            this.rdbAttachHead.TabStop = true;
            this.rdbAttachHead.Text = "まえにつける";
            this.rdbAttachHead.UseVisualStyleBackColor = true;
            this.rdbAttachHead.CheckedChanged += new System.EventHandler(this.rdbAttachHead_CheckedChanged);
            // 
            // rdbAttachTale
            // 
            this.rdbAttachTale.AutoSize = true;
            this.rdbAttachTale.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAttachTale.Location = new System.Drawing.Point(131, 12);
            this.rdbAttachTale.Name = "rdbAttachTale";
            this.rdbAttachTale.Size = new System.Drawing.Size(123, 25);
            this.rdbAttachTale.TabIndex = 1;
            this.rdbAttachTale.TabStop = true;
            this.rdbAttachTale.Text = "うしろにつける";
            this.rdbAttachTale.UseVisualStyleBackColor = true;
            this.rdbAttachTale.CheckedChanged += new System.EventHandler(this.rdbAttachTale_CheckedChanged);
            // 
            // tbxAttachInfo
            // 
            this.tbxAttachInfo.Location = new System.Drawing.Point(12, 185);
            this.tbxAttachInfo.Name = "tbxAttachInfo";
            this.tbxAttachInfo.Size = new System.Drawing.Size(465, 19);
            this.tbxAttachInfo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "文字列(置換のときは半角スペースの後に差し替え文字列を記入)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "対象ファイル群";
            // 
            // lstFile
            // 
            this.lstFile.AllowDrop = true;
            this.lstFile.FormattingEnabled = true;
            this.lstFile.ItemHeight = 12;
            this.lstFile.Location = new System.Drawing.Point(12, 241);
            this.lstFile.Name = "lstFile";
            this.lstFile.Size = new System.Drawing.Size(465, 76);
            this.lstFile.TabIndex = 5;
            this.lstFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFile_DragDrop);
            this.lstFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFile_DragEnter);
            // 
            // runBtn
            // 
            this.runBtn.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBtn.Location = new System.Drawing.Point(12, 323);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(465, 31);
            this.runBtn.TabIndex = 6;
            this.runBtn.Text = "実行";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // rdbDelete
            // 
            this.rdbDelete.AutoSize = true;
            this.rdbDelete.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDelete.Location = new System.Drawing.Point(260, 12);
            this.rdbDelete.Name = "rdbDelete";
            this.rdbDelete.Size = new System.Drawing.Size(115, 25);
            this.rdbDelete.TabIndex = 7;
            this.rdbDelete.TabStop = true;
            this.rdbDelete.Text = "文字の消去";
            this.rdbDelete.UseVisualStyleBackColor = true;
            // 
            // tbxMessage
            // 
            this.tbxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMessage.Location = new System.Drawing.Point(12, 44);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ReadOnly = true;
            this.tbxMessage.Size = new System.Drawing.Size(465, 112);
            this.tbxMessage.TabIndex = 8;
            // 
            // rdbPermutation
            // 
            this.rdbPermutation.AutoSize = true;
            this.rdbPermutation.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbPermutation.Location = new System.Drawing.Point(381, 12);
            this.rdbPermutation.Name = "rdbPermutation";
            this.rdbPermutation.Size = new System.Drawing.Size(64, 25);
            this.rdbPermutation.TabIndex = 9;
            this.rdbPermutation.TabStop = true;
            this.rdbPermutation.Text = "置換";
            this.rdbPermutation.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 368);
            this.Controls.Add(this.rdbPermutation);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.rdbDelete);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.lstFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxAttachInfo);
            this.Controls.Add(this.rdbAttachTale);
            this.Controls.Add(this.rdbAttachHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "ReNameKun";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbAttachHead;
        private System.Windows.Forms.RadioButton rdbAttachTale;
        private System.Windows.Forms.TextBox tbxAttachInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstFile;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.RadioButton rdbDelete;
        protected System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.RadioButton rdbPermutation;
    }
}

