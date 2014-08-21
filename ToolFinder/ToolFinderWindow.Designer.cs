namespace ToolFinder
{
    partial class ToolFinderWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TableLayoutPanel tlpMain;
            this.txtSearchTerm = new System.Windows.Forms.WatermarkTextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.lvwResults = new System.Windows.Forms.ListView();
            tlpMain = new System.Windows.Forms.TableLayoutPanel();
            tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpMain.Controls.Add(this.txtSearchTerm, 0, 0);
            tlpMain.Controls.Add(this.btnFind, 1, 0);
            tlpMain.Controls.Add(this.lvwResults, 0, 1);
            tlpMain.Location = new System.Drawing.Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpMain.Size = new System.Drawing.Size(256, 480);
            tlpMain.TabIndex = 0;
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchTerm.Location = new System.Drawing.Point(3, 3);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.PromptText = "Enter the keyword to search for";
            this.txtSearchTerm.Size = new System.Drawing.Size(221, 20);
            this.txtSearchTerm.TabIndex = 0;
            this.txtSearchTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerm_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.Image = global::ToolFinder.Properties.Resources.Find16;
            this.btnFind.Location = new System.Drawing.Point(230, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(23, 23);
            this.btnFind.TabIndex = 1;
            this.btnFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lvwResults
            // 
            tlpMain.SetColumnSpan(this.lvwResults, 2);
            this.lvwResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwResults.Location = new System.Drawing.Point(3, 32);
            this.lvwResults.Name = "lvwResults";
            this.lvwResults.Size = new System.Drawing.Size(250, 445);
            this.lvwResults.TabIndex = 2;
            this.lvwResults.UseCompatibleStateImageBehavior = false;
            this.lvwResults.View = System.Windows.Forms.View.List;
            this.lvwResults.DoubleClick += new System.EventHandler(this.lvwResults_DoubleClick);
            this.lvwResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwResults_KeyDown);
            // 
            // ToolFinderWindow
            // 
            this.Controls.Add(tlpMain);
            this.Name = "ToolFinderWindow";
            this.Size = new System.Drawing.Size(256, 480);
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WatermarkTextBox txtSearchTerm;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListView lvwResults;

    }
}
