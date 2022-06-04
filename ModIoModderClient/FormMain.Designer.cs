namespace ModIoModderClient
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMods = new System.Windows.Forms.ComboBox();
            this.btnGetModFiles = new System.Windows.Forms.Button();
            this.dgvModFiles = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModFiles)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cboMods);
            this.flowLayoutPanel1.Controls.Add(this.btnGetModFiles);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1441, 39);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your Mods: ";
            // 
            // cboMods
            // 
            this.cboMods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMods.FormattingEnabled = true;
            this.cboMods.Location = new System.Drawing.Point(116, 3);
            this.cboMods.Name = "cboMods";
            this.cboMods.Size = new System.Drawing.Size(436, 33);
            this.cboMods.TabIndex = 0;
            this.cboMods.SelectedIndexChanged += new System.EventHandler(this.cboMods_SelectedIndexChanged);
            // 
            // btnGetModFiles
            // 
            this.btnGetModFiles.Enabled = false;
            this.btnGetModFiles.Location = new System.Drawing.Point(558, 3);
            this.btnGetModFiles.Name = "btnGetModFiles";
            this.btnGetModFiles.Size = new System.Drawing.Size(150, 34);
            this.btnGetModFiles.TabIndex = 1;
            this.btnGetModFiles.Text = "Get Mod Files";
            this.btnGetModFiles.UseVisualStyleBackColor = true;
            this.btnGetModFiles.Click += new System.EventHandler(this.btnGetModFiles_Click);
            // 
            // dgvModFiles
            // 
            this.dgvModFiles.AllowUserToAddRows = false;
            this.dgvModFiles.AllowUserToDeleteRows = false;
            this.dgvModFiles.AllowUserToOrderColumns = true;
            this.dgvModFiles.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvModFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModFiles.Location = new System.Drawing.Point(0, 39);
            this.dgvModFiles.Name = "dgvModFiles";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvModFiles.RowHeadersVisible = false;
            this.dgvModFiles.RowHeadersWidth = 62;
            this.dgvModFiles.RowTemplate.Height = 33;
            this.dgvModFiles.Size = new System.Drawing.Size(1441, 361);
            this.dgvModFiles.TabIndex = 2;
            this.dgvModFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModFiles_CellContentClick);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnAddFiles);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 400);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1441, 50);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(1326, 3);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(112, 34);
            this.btnAddFiles.TabIndex = 0;
            this.btnAddFiles.Text = "Add Files";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Visible = false;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 450);
            this.Controls.Add(this.dgvModFiles);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod.Io";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModFiles)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FlowLayoutPanel flowLayoutPanel1;
        private ComboBox cboMods;
        private DataGridView dgvModFiles;
        private Button btnGetModFiles;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button btnAddFiles;
        private Label label1;
    }
}