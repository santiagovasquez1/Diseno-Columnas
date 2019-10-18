namespace DisenoColumnas.Diseño
{
    partial class AgregarAlzado
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
            this.D_Alzado = new System.Windows.Forms.DataGridView();
            this.NameColum = new System.Windows.Forms.Label();
            this.story = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.D_Alzado)).BeginInit();
            this.SuspendLayout();
            // 
            // D_Alzado
            // 
            this.D_Alzado.AllowUserToAddRows = false;
            this.D_Alzado.AllowUserToDeleteRows = false;
            this.D_Alzado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D_Alzado.BackgroundColor = System.Drawing.Color.White;
            this.D_Alzado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_Alzado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.story});
            this.D_Alzado.GridColor = System.Drawing.Color.DarkGray;
            this.D_Alzado.Location = new System.Drawing.Point(12, 30);
            this.D_Alzado.Name = "D_Alzado";
            this.D_Alzado.Size = new System.Drawing.Size(620, 441);
            this.D_Alzado.TabIndex = 1;
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
            // story
            // 
            this.story.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.story.HeaderText = "Story";
            this.story.Name = "story";
            this.story.ReadOnly = true;
            this.story.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AgregarAlzado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(644, 482);
            this.Controls.Add(this.NameColum);
            this.Controls.Add(this.D_Alzado);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgregarAlzado";
            this.Text = "Agregar Alzado";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AgregarAlzado_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.D_Alzado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView D_Alzado;
        private SpannedDataGridViewNet2.DataGridViewTextBoxColumnEx Column1;
        public System.Windows.Forms.Label NameColum;
        private System.Windows.Forms.DataGridViewImageColumn story;
    }
}