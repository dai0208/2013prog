namespace PairedComparison
{
    partial class fmStart
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
            this.tbxInfo = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectSaveFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSaveFolderName = new System.Windows.Forms.Label();
            this.tbxSaveFolderName = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxInfo
            // 
            this.tbxInfo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbxInfo.Location = new System.Drawing.Point(27, 41);
            this.tbxInfo.Multiline = true;
            this.tbxInfo.Name = "tbxInfo";
            this.tbxInfo.Size = new System.Drawing.Size(378, 163);
            this.tbxInfo.TabIndex = 0;
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(78, 210);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(327, 19);
            this.tbxName.TabIndex = 0;
            this.tbxName.Text = "名前を入力してください。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "名前";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(180, 284);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "OK";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadFolder,
            this.tsmiSelectSaveFolder});
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.設定ToolStripMenuItem.Text = "設定";
            // 
            // tsmiLoadFolder
            // 
            this.tsmiLoadFolder.Name = "tsmiLoadFolder";
            this.tsmiLoadFolder.Size = new System.Drawing.Size(188, 22);
            this.tsmiLoadFolder.Text = "読み込み画像の設定(&L)";
            this.tsmiLoadFolder.Click += new System.EventHandler(this.tsmiLoadFolder_Click);
            // 
            // tsmiSelectSaveFolder
            // 
            this.tsmiSelectSaveFolder.Name = "tsmiSelectSaveFolder";
            this.tsmiSelectSaveFolder.Size = new System.Drawing.Size(188, 22);
            this.tsmiSelectSaveFolder.Text = "セーブフォルダ設定(&S)";
            this.tsmiSelectSaveFolder.Click += new System.EventHandler(this.tsmiSelectSaveFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "保存先";
            // 
            // lblSaveFolderName
            // 
            this.lblSaveFolderName.AutoSize = true;
            this.lblSaveFolderName.Location = new System.Drawing.Point(98, 264);
            this.lblSaveFolderName.Name = "lblSaveFolderName";
            this.lblSaveFolderName.Size = new System.Drawing.Size(319, 12);
            this.lblSaveFolderName.TabIndex = 6;
            this.lblSaveFolderName.Text = "※選択されたフォルダ下に自動的に名前のフォルダが作成されます。";
            // 
            // tbxSaveFolderName
            // 
            this.tbxSaveFolderName.Location = new System.Drawing.Point(78, 242);
            this.tbxSaveFolderName.Name = "tbxSaveFolderName";
            this.tbxSaveFolderName.Size = new System.Drawing.Size(327, 19);
            this.tbxSaveFolderName.TabIndex = 7;
            // 
            // fmStart
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 319);
            this.Controls.Add(this.tbxSaveFolderName);
            this.Controls.Add(this.lblSaveFolderName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.tbxInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmStart";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.fmStart_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxInfo;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectSaveFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSaveFolderName;
        private System.Windows.Forms.TextBox tbxSaveFolderName;

    }
}