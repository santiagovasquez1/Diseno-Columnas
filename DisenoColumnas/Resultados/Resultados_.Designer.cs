namespace DisenoColumnas.Resultados
{
    partial class Resultados_
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resultados_));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGV_Estribos = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Estribos)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGV_Estribos);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(767, 397);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "No. Estribos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DGV_Estribos
            // 
            this.DGV_Estribos.AllowUserToAddRows = false;
            this.DGV_Estribos.AllowUserToDeleteRows = false;
            this.DGV_Estribos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DGV_Estribos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Estribos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Estribos.Location = new System.Drawing.Point(3, 3);
            this.DGV_Estribos.Name = "DGV_Estribos";
            this.DGV_Estribos.ReadOnly = true;
            this.DGV_Estribos.Size = new System.Drawing.Size(761, 391);
            this.DGV_Estribos.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 423);
            this.tabControl1.TabIndex = 0;
            // 
            // Resultados_
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(803, 457);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(819, 496);
            this.Name = "Resultados_";
            this.Text = "DMC- Resultados - Cantidad de Estribos";
            this.Load += new System.EventHandler(this.Resultados__Load);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Estribos)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGV_Estribos;
        private System.Windows.Forms.TabControl tabControl1;
    }
}