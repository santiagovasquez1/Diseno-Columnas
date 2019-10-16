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
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColum = new System.Windows.Forms.Label();
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
            this.TF});
            this.Info_D.GridColor = System.Drawing.Color.DarkGray;
            this.Info_D.Location = new System.Drawing.Point(12, 53);
            this.Info_D.Name = "Info_D";
            this.Info_D.ReadOnly = true;
            this.Info_D.Size = new System.Drawing.Size(750, 476);
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
            // NameColum
            // 
            this.NameColum.AutoSize = true;
            this.NameColum.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameColum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.NameColum.Location = new System.Drawing.Point(22, 24);
            this.NameColum.Name = "NameColum";
            this.NameColum.Size = new System.Drawing.Size(126, 14);
            this.NameColum.TabIndex = 0;
            this.NameColum.Text = "Columna: XXXXXXXXXX";
            // 
            // Informacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(774, 535);
            this.Controls.Add(this.NameColum);
            this.Controls.Add(this.Info_D);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn B;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn TW;
        private System.Windows.Forms.DataGridViewTextBoxColumn TF;
    }
}