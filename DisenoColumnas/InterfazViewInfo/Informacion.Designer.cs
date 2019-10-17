namespace DisenoColumnas.InterfazViewInfo
{
    partial class Informacion
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
            this.Info_D = new System.Windows.Forms.DataGridView();
            this.Column1 = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.Column2 = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.B = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.H = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.TW = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.TF = new SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx();
            this.Locali = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AceroR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asasign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColum = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.Info_D)).BeginInit();
            this.SuspendLayout();
            // 
            // Info_D
            // 
            this.Info_D.AllowUserToAddRows = false;
            this.Info_D.AllowUserToDeleteRows = false;
            this.Info_D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Info_D.BackgroundColor = System.Drawing.Color.White;
            this.Info_D.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Info_D.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.B,
            this.H,
            this.TW,
            this.TF,
            this.Locali,
            this.AceroR,
            this.Asasign});
            this.Info_D.GridColor = System.Drawing.Color.DarkGray;
            this.Info_D.Location = new System.Drawing.Point(12, 53);
            this.Info_D.Name = "Info_D";
            this.Info_D.ReadOnly = true;
            this.Info_D.Size = new System.Drawing.Size(771, 476);
            this.Info_D.TabIndex = 0;
            this.Info_D.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGridView1_Paint);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Story";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "F\'c";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // B
            // 
            this.B.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.B.HeaderText = "B";
            this.B.Name = "B";
            this.B.ReadOnly = true;
            this.B.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // H
            // 
            this.H.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.H.HeaderText = "H";
            this.H.Name = "H";
            this.H.ReadOnly = true;
            this.H.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TW
            // 
            this.TW.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TW.HeaderText = "Tw";
            this.TW.Name = "TW";
            this.TW.ReadOnly = true;
            this.TW.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TF
            // 
            this.TF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TF.HeaderText = "Tf";
            this.TF.Name = "TF";
            this.TF.ReadOnly = true;
            this.TF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Locali
            // 
            this.Locali.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Locali.HeaderText = "Top / Medium / Button";
            this.Locali.Name = "Locali";
            this.Locali.ReadOnly = true;
            this.Locali.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Locali.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AceroR
            // 
            this.AceroR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AceroR.HeaderText = "Acero Requerido [cm²]";
            this.AceroR.Name = "AceroR";
            this.AceroR.ReadOnly = true;
            this.AceroR.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AceroR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Asasign
            // 
            this.Asasign.HeaderText = "Acero Asignado [cm²]";
            this.Asasign.Name = "Asasign";
            this.Asasign.ReadOnly = true;
            // 
            // NameColum
            // 
            this.NameColum.AutoSize = true;
            this.NameColum.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameColum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.NameColum.Location = new System.Drawing.Point(22, 24);
            this.NameColum.Name = "NameColum";
            this.NameColum.Size = new System.Drawing.Size(56, 14);
            this.NameColum.TabIndex = 0;
            this.NameColum.Text = "Columna: ";
            // 
            // Informacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1066, 535);
            this.Controls.Add(this.NameColum);
            this.Controls.Add(this.Info_D);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Informacion";
            this.Text = "Informacion";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Informacion_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.Info_D)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Info_D;
        public  System.Windows.Forms.Label NameColum;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx Column1;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx Column2;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx B;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx H;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx TW;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx TF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locali;
        private System.Windows.Forms.DataGridViewTextBoxColumn AceroR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asasign;
    }
}