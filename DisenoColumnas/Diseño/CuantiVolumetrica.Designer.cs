namespace DisenoColumnas.Diseño
{
    partial class CuantiaVolumetrica
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
            this.NameColum = new System.Windows.Forms.Label();
            this.Info_Es_Col = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoEstribo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.S_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoRamasV_1 = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.NoRamasV_2 = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.NoRamas_H1 = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.NoRamas_H2 = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.Info_Es_Col)).BeginInit();
            this.SuspendLayout();
            // 
            // NameColum
            // 
            this.NameColum.AutoSize = true;
            this.NameColum.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameColum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.NameColum.Location = new System.Drawing.Point(12, 11);
            this.NameColum.Name = "NameColum";
            this.NameColum.Size = new System.Drawing.Size(56, 14);
            this.NameColum.TabIndex = 2;
            this.NameColum.Text = "Columna: ";
            // 
            // Info_Es_Col
            // 
            this.Info_Es_Col.AllowUserToAddRows = false;
            this.Info_Es_Col.AllowUserToDeleteRows = false;
            this.Info_Es_Col.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Info_Es_Col.BackgroundColor = System.Drawing.Color.White;
            this.Info_Es_Col.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Info_Es_Col.ColumnHeadersVisible = false;
            this.Info_Es_Col.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.B,
            this.H,
            this.TW,
            this.TF,
            this.NoEstribo,
            this.S_value,
            this.NoRamasV_1,
            this.NoRamasV_2,
            this.NoRamas_H1,
            this.NoRamas_H2});
            this.Info_Es_Col.GridColor = System.Drawing.Color.DarkGray;
            this.Info_Es_Col.Location = new System.Drawing.Point(12, 32);
            this.Info_Es_Col.Name = "Info_Es_Col";
            this.Info_Es_Col.Size = new System.Drawing.Size(1085, 377);
            this.Info_Es_Col.TabIndex = 1;
            this.Info_Es_Col.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Info_Es_Col_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Story";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "F\'c";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // B
            // 
            this.B.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.B.HeaderText = "B";
            this.B.Name = "B";
            this.B.ReadOnly = true;
            this.B.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.B.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // H
            // 
            this.H.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.H.HeaderText = "H";
            this.H.Name = "H";
            this.H.ReadOnly = true;
            this.H.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.H.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TW
            // 
            this.TW.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TW.HeaderText = "Tw";
            this.TW.Name = "TW";
            this.TW.ReadOnly = true;
            this.TW.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TF
            // 
            this.TF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TF.HeaderText = "Tf";
            this.TF.Name = "TF";
            this.TF.ReadOnly = true;
            this.TF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoEstribo
            // 
            this.NoEstribo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoEstribo.HeaderText = "# Estribo";
            this.NoEstribo.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6"});
            this.NoEstribo.Name = "NoEstribo";
            this.NoEstribo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // S_value
            // 
            this.S_value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.S_value.HeaderText = "Separación [cm²]";
            this.S_value.Name = "S_value";
            this.S_value.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.S_value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoRamasV_1
            // 
            this.NoRamasV_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoRamasV_1.HeaderText = "No. Ramas Vertical (Aleta)";
            this.NoRamasV_1.Name = "NoRamasV_1";
            this.NoRamasV_1.ReadOnly = true;
            this.NoRamasV_1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // NoRamasV_2
            // 
            this.NoRamasV_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoRamasV_2.HeaderText = "No. Ramas Vertical (Alma)";
            this.NoRamasV_2.Name = "NoRamasV_2";
            this.NoRamasV_2.ReadOnly = true;
            this.NoRamasV_2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // NoRamas_H1
            // 
            this.NoRamas_H1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoRamas_H1.HeaderText = "No. Ramas Horizontal (Aleta)";
            this.NoRamas_H1.Name = "NoRamas_H1";
            this.NoRamas_H1.ReadOnly = true;
            this.NoRamas_H1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // NoRamas_H2
            // 
            this.NoRamas_H2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoRamas_H2.HeaderText = "No. Ramas Horizontal (Alma)";
            this.NoRamas_H2.Name = "NoRamas_H2";
            this.NoRamas_H2.ReadOnly = true;
            this.NoRamas_H2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // CuantiaVolumetrica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1109, 417);
            this.Controls.Add(this.NameColum);
            this.Controls.Add(this.Info_Es_Col);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CuantiaVolumetrica";
            this.Text = "Cuantía Volumétrica";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CuantiaVolumetrica_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.Info_Es_Col)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label NameColum;
        private System.Windows.Forms.DataGridViewComboBoxColumn Diametro__;
        private System.Windows.Forms.DataGridView Info_Es_Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn B;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn TW;
        private System.Windows.Forms.DataGridViewTextBoxColumn TF;
        private System.Windows.Forms.DataGridViewComboBoxColumn NoEstribo;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_value;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx NoRamasV_1;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx NoRamasV_2;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx NoRamas_H1;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx NoRamas_H2;
    }
}