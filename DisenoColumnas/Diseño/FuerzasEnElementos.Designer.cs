namespace DisenoColumnas.Diseño
{
    partial class FuerzasEnElementos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuerzasEnElementos));
            this.D_Fuerzas = new System.Windows.Forms.DataGridView();
            this.NameColum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.D_Fuerzas)).BeginInit();
            this.SuspendLayout();
            // 
            // D_Fuerzas
            // 
            this.D_Fuerzas.AllowUserToAddRows = false;
            this.D_Fuerzas.AllowUserToDeleteRows = false;
            this.D_Fuerzas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D_Fuerzas.BackgroundColor = System.Drawing.Color.White;
            this.D_Fuerzas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_Fuerzas.GridColor = System.Drawing.Color.DarkGray;
            this.D_Fuerzas.Location = new System.Drawing.Point(12, 26);
            this.D_Fuerzas.Name = "D_Fuerzas";
            this.D_Fuerzas.ReadOnly = true;
            this.D_Fuerzas.Size = new System.Drawing.Size(975, 597);
            this.D_Fuerzas.TabIndex = 1;
            // 
            // NameColum
            // 
            this.NameColum.AutoSize = true;
            this.NameColum.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameColum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.NameColum.Location = new System.Drawing.Point(12, 9);
            this.NameColum.Name = "NameColum";
            this.NameColum.Size = new System.Drawing.Size(56, 14);
            this.NameColum.TabIndex = 2;
            this.NameColum.Text = "Columna: ";
            // 
            // FuerzasEnElementos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(999, 635);
            this.Controls.Add(this.NameColum);
            this.Controls.Add(this.D_Fuerzas);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FuerzasEnElementos";
            this.Text = "Fuerzas en la Columna";
            this.Load += new System.EventHandler(this.FuerzasEnElementos_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FuerzasEnElementos_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.D_Fuerzas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView D_Fuerzas;
        public System.Windows.Forms.Label NameColum;
    }
}