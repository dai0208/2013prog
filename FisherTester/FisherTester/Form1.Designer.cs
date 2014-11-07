namespace FisherTester
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.WeightController = new System.Windows.Forms.TrackBar();
            this.WeightValueBox = new System.Windows.Forms.TextBox();
            this.TransformBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAscFile = new System.Windows.Forms.ToolStripMenuItem();
            this.runBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WeightController)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(48, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(333, 16);
            this.listBox1.TabIndex = 1;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            // 
            // listBox2
            // 
            this.listBox2.AllowDrop = true;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(48, 53);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(333, 52);
            this.listBox2.Sorted = true;
            this.listBox2.TabIndex = 3;
            this.listBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
            this.listBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox2_DragEnter);
            // 
            // listBox3
            // 
            this.listBox3.AllowDrop = true;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.HorizontalScrollbar = true;
            this.listBox3.ItemHeight = 12;
            this.listBox3.Location = new System.Drawing.Point(48, 111);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(333, 52);
            this.listBox3.Sorted = true;
            this.listBox3.TabIndex = 4;
            this.listBox3.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox3_DragDrop);
            this.listBox3.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox3_DragEnter);
            // 
            // WeightController
            // 
            this.WeightController.Location = new System.Drawing.Point(48, 169);
            this.WeightController.Minimum = -10;
            this.WeightController.Name = "WeightController";
            this.WeightController.Size = new System.Drawing.Size(333, 45);
            this.WeightController.TabIndex = 13;
            this.WeightController.Scroll += new System.EventHandler(this.WeightController_Scroll);
            // 
            // WeightValueBox
            // 
            this.WeightValueBox.Location = new System.Drawing.Point(48, 212);
            this.WeightValueBox.Name = "WeightValueBox";
            this.WeightValueBox.ReadOnly = true;
            this.WeightValueBox.Size = new System.Drawing.Size(333, 19);
            this.WeightValueBox.TabIndex = 15;
            this.WeightValueBox.Text = "0";
            // 
            // TransformBtn
            // 
            this.TransformBtn.Enabled = false;
            this.TransformBtn.Location = new System.Drawing.Point(216, 237);
            this.TransformBtn.Name = "TransformBtn";
            this.TransformBtn.Size = new System.Drawing.Size(165, 23);
            this.TransformBtn.TabIndex = 16;
            this.TransformBtn.Text = "TransForm";
            this.TransformBtn.UseVisualStyleBackColor = true;
            this.TransformBtn.Click += new System.EventHandler(this.TransformBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.出力ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(393, 26);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 出力ToolStripMenuItem
            // 
            this.出力ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveAscFile});
            this.出力ToolStripMenuItem.Name = "出力ToolStripMenuItem";
            this.出力ToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.出力ToolStripMenuItem.Text = "出力";
            // 
            // SaveAscFile
            // 
            this.SaveAscFile.Name = "SaveAscFile";
            this.SaveAscFile.Size = new System.Drawing.Size(191, 22);
            this.SaveAscFile.Text = "変換後ascパラメータ";
            this.SaveAscFile.Click += new System.EventHandler(this.SaveAscFile_Click);
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(48, 237);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(165, 23);
            this.runBtn.TabIndex = 18;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "Cls1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "Cls2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "Weight";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "Value";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 270);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.TransformBtn);
            this.Controls.Add(this.WeightValueBox);
            this.Controls.Add(this.WeightController);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.WeightController)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.TrackBar WeightController;
        private System.Windows.Forms.TextBox WeightValueBox;
        private System.Windows.Forms.Button TransformBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 出力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAscFile;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

