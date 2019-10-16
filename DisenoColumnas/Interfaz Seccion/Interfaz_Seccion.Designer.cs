namespace DisenoColumnas.Interfaz_Seccion
{
    partial class Interfaz_Seccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interfaz_Seccion));
            this.Grafica = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BSelect = new System.Windows.Forms.Button();
            this.BZplus = new System.Windows.Forms.Button();
            this.BZminus = new System.Windows.Forms.Button();
            this.BZenc = new System.Windows.Forms.Button();
            this.BMove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grafica
            // 
            this.Grafica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grafica.BackColor = System.Drawing.Color.White;
            this.Grafica.Location = new System.Drawing.Point(48, 12);
            this.Grafica.Name = "Grafica";
            this.Grafica.Size = new System.Drawing.Size(736, 504);
            this.Grafica.TabIndex = 1;
            this.Grafica.TabStop = false;
            this.Grafica.Paint += new System.Windows.Forms.PaintEventHandler(this.Grafica_Paint);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.BMove);
            this.panel1.Controls.Add(this.BZenc);
            this.panel1.Controls.Add(this.BZminus);
            this.panel1.Controls.Add(this.BZplus);
            this.panel1.Controls.Add(this.BSelect);
            this.panel1.Location = new System.Drawing.Point(5, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(37, 504);
            this.panel1.TabIndex = 2;
            // 
            // BSelect
            // 
            this.BSelect.BackColor = System.Drawing.Color.Transparent;
            this.BSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BSelect.Cursor = System.Windows.Forms.Cursors.Default;
            this.BSelect.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BSelect.FlatAppearance.BorderSize = 0;
            this.BSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BSelect.Image = ((System.Drawing.Image)(resources.GetObject("BSelect.Image")));
            this.BSelect.Location = new System.Drawing.Point(3, 3);
            this.BSelect.Name = "BSelect";
            this.BSelect.Size = new System.Drawing.Size(31, 35);
            this.BSelect.TabIndex = 0;
            this.BSelect.UseVisualStyleBackColor = false;
            // 
            // BZplus
            // 
            this.BZplus.BackColor = System.Drawing.Color.Transparent;
            this.BZplus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BZplus.Cursor = System.Windows.Forms.Cursors.Default;
            this.BZplus.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BZplus.FlatAppearance.BorderSize = 0;
            this.BZplus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BZplus.Image = ((System.Drawing.Image)(resources.GetObject("BZplus.Image")));
            this.BZplus.Location = new System.Drawing.Point(3, 85);
            this.BZplus.Name = "BZplus";
            this.BZplus.Size = new System.Drawing.Size(31, 35);
            this.BZplus.TabIndex = 1;
            this.BZplus.UseVisualStyleBackColor = false;
            // 
            // BZminus
            // 
            this.BZminus.BackColor = System.Drawing.Color.Transparent;
            this.BZminus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BZminus.Cursor = System.Windows.Forms.Cursors.Default;
            this.BZminus.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BZminus.FlatAppearance.BorderSize = 0;
            this.BZminus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BZminus.Image = ((System.Drawing.Image)(resources.GetObject("BZminus.Image")));
            this.BZminus.Location = new System.Drawing.Point(3, 126);
            this.BZminus.Name = "BZminus";
            this.BZminus.Size = new System.Drawing.Size(31, 35);
            this.BZminus.TabIndex = 2;
            this.BZminus.UseVisualStyleBackColor = false;
            // 
            // BZenc
            // 
            this.BZenc.BackColor = System.Drawing.Color.Transparent;
            this.BZenc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BZenc.Cursor = System.Windows.Forms.Cursors.Default;
            this.BZenc.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BZenc.FlatAppearance.BorderSize = 0;
            this.BZenc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BZenc.Image = ((System.Drawing.Image)(resources.GetObject("BZenc.Image")));
            this.BZenc.Location = new System.Drawing.Point(3, 167);
            this.BZenc.Name = "BZenc";
            this.BZenc.Size = new System.Drawing.Size(31, 35);
            this.BZenc.TabIndex = 3;
            this.BZenc.UseVisualStyleBackColor = false;
            // 
            // BMove
            // 
            this.BMove.BackColor = System.Drawing.Color.Transparent;
            this.BMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BMove.Cursor = System.Windows.Forms.Cursors.Default;
            this.BMove.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BMove.FlatAppearance.BorderSize = 0;
            this.BMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BMove.Image = ((System.Drawing.Image)(resources.GetObject("BMove.Image")));
            this.BMove.Location = new System.Drawing.Point(3, 44);
            this.BMove.Name = "BMove";
            this.BMove.Size = new System.Drawing.Size(31, 35);
            this.BMove.TabIndex = 4;
            this.BMove.UseVisualStyleBackColor = false;
            // 
            // Interfaz_Seccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(796, 528);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Grafica);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Interfaz_Seccion";
            this.Text = "Interfaz_Seccion";
            this.Load += new System.EventHandler(this.Interfaz_Seccion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Grafica;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BSelect;
        private System.Windows.Forms.Button BZminus;
        private System.Windows.Forms.Button BZplus;
        private System.Windows.Forms.Button BMove;
        private System.Windows.Forms.Button BZenc;
    }
}