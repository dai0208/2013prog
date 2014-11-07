namespace SVMClassifierWithEyeMotion
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxTestDatas = new System.Windows.Forms.ListBox();
            this.classifier_btn = new System.Windows.Forms.Button();
            this.learning_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nmcSizeN = new System.Windows.Forms.NumericUpDown();
            this.nmcSizeH = new System.Windows.Forms.NumericUpDown();
            this.lbxNeuLow = new System.Windows.Forms.ListBox();
            this.lbxHighNeu = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Result_Text_Box = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcSizeN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcSizeH)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbxTestDatas);
            this.panel1.Controls.Add(this.classifier_btn);
            this.panel1.Location = new System.Drawing.Point(9, 262);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 159);
            this.panel1.TabIndex = 1;
            // 
            // lbxTestDatas
            // 
            this.lbxTestDatas.AllowDrop = true;
            this.lbxTestDatas.FormattingEnabled = true;
            this.lbxTestDatas.ItemHeight = 12;
            this.lbxTestDatas.Location = new System.Drawing.Point(5, 4);
            this.lbxTestDatas.Name = "lbxTestDatas";
            this.lbxTestDatas.Size = new System.Drawing.Size(429, 124);
            this.lbxTestDatas.TabIndex = 12;
            this.lbxTestDatas.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxTestDatas_DragDrop);
            this.lbxTestDatas.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxTestDatas_DragEnter);
            // 
            // classifier_btn
            // 
            this.classifier_btn.Location = new System.Drawing.Point(5, 130);
            this.classifier_btn.Name = "classifier_btn";
            this.classifier_btn.Size = new System.Drawing.Size(246, 23);
            this.classifier_btn.TabIndex = 11;
            this.classifier_btn.Text = "しきべつ";
            this.classifier_btn.UseVisualStyleBackColor = true;
            this.classifier_btn.Click += new System.EventHandler(this.classifier_btn_Click);
            // 
            // learning_btn
            // 
            this.learning_btn.Location = new System.Drawing.Point(5, 196);
            this.learning_btn.Name = "learning_btn";
            this.learning_btn.Size = new System.Drawing.Size(246, 23);
            this.learning_btn.TabIndex = 10;
            this.learning_btn.Text = "がくしゅう";
            this.learning_btn.UseVisualStyleBackColor = true;
            this.learning_btn.Click += new System.EventHandler(this.learning_btn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nmcSizeN);
            this.panel2.Controls.Add(this.nmcSizeH);
            this.panel2.Controls.Add(this.lbxNeuLow);
            this.panel2.Controls.Add(this.lbxHighNeu);
            this.panel2.Controls.Add(this.learning_btn);
            this.panel2.Location = new System.Drawing.Point(9, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 232);
            this.panel2.TabIndex = 11;
            // 
            // nmcSizeN
            // 
            this.nmcSizeN.Location = new System.Drawing.Point(131, 171);
            this.nmcSizeN.Name = "nmcSizeN";
            this.nmcSizeN.Size = new System.Drawing.Size(120, 19);
            this.nmcSizeN.TabIndex = 14;
            // 
            // nmcSizeH
            // 
            this.nmcSizeH.Location = new System.Drawing.Point(5, 171);
            this.nmcSizeH.Name = "nmcSizeH";
            this.nmcSizeH.Size = new System.Drawing.Size(120, 19);
            this.nmcSizeH.TabIndex = 13;
            // 
            // lbxNeuLow
            // 
            this.lbxNeuLow.AllowDrop = true;
            this.lbxNeuLow.FormattingEnabled = true;
            this.lbxNeuLow.HorizontalScrollbar = true;
            this.lbxNeuLow.ItemHeight = 12;
            this.lbxNeuLow.Location = new System.Drawing.Point(131, 4);
            this.lbxNeuLow.Name = "lbxNeuLow";
            this.lbxNeuLow.Size = new System.Drawing.Size(120, 160);
            this.lbxNeuLow.TabIndex = 12;
            this.lbxNeuLow.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxNeuLow_DragDrop);
            this.lbxNeuLow.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxNeuLow_DragEnter);
            // 
            // lbxHighNeu
            // 
            this.lbxHighNeu.AllowDrop = true;
            this.lbxHighNeu.FormattingEnabled = true;
            this.lbxHighNeu.HorizontalScrollbar = true;
            this.lbxHighNeu.ItemHeight = 12;
            this.lbxHighNeu.Location = new System.Drawing.Point(5, 4);
            this.lbxHighNeu.Name = "lbxHighNeu";
            this.lbxHighNeu.Size = new System.Drawing.Size(120, 160);
            this.lbxHighNeu.TabIndex = 11;
            this.lbxHighNeu.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxHighNeu_DragDrop);
            this.lbxHighNeu.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxHighNeu_DragEnter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(278, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "Result";
            // 
            // Result_Text_Box
            // 
            this.Result_Text_Box.Location = new System.Drawing.Point(278, 24);
            this.Result_Text_Box.Name = "Result_Text_Box";
            this.Result_Text_Box.Size = new System.Drawing.Size(168, 232);
            this.Result_Text_Box.TabIndex = 13;
            this.Result_Text_Box.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 433);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Result_Text_Box);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmcSizeN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcSizeH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox Result_Text_Box;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button classifier_btn;
        private System.Windows.Forms.Button learning_btn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbxNeuLow;
        private System.Windows.Forms.ListBox lbxHighNeu;
        private System.Windows.Forms.NumericUpDown nmcSizeN;
        private System.Windows.Forms.NumericUpDown nmcSizeH;
        private System.Windows.Forms.ListBox lbxTestDatas;

    }
}

