namespace DisenoColumnas.Diseño
{
    partial class ColumnasaGraficar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.D_ColGraficar = new System.Windows.Forms.DataGridView();
            this.Columnas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Disenar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.D_ColGraficar)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Button3
            // 
            this.Button3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button3.Location = new System.Drawing.Point(16, 380);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(115, 23);
            this.Button3.TabIndex = 23;
            this.Button3.Text = "Seleccionar todo";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Location = new System.Drawing.Point(202, 380);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(65, 23);
            this.Button2.TabIndex = 22;
            this.Button2.Text = "Cancelar";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(137, 380);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(59, 23);
            this.Button1.TabIndex = 21;
            this.Button1.Text = "Ok";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // D_ColGraficar
            // 
            this.D_ColGraficar.AllowUserToAddRows = false;
            this.D_ColGraficar.AllowUserToDeleteRows = false;
            this.D_ColGraficar.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.D_ColGraficar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_ColGraficar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnas,
            this.Disenar});
            this.D_ColGraficar.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.D_ColGraficar.GridColor = System.Drawing.SystemColors.Control;
            this.D_ColGraficar.Location = new System.Drawing.Point(6, 29);
            this.D_ColGraficar.Name = "D_ColGraficar";
            this.D_ColGraficar.Size = new System.Drawing.Size(267, 343);
            this.D_ColGraficar.TabIndex = 20;
            // 
            // Columnas
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Columnas.DefaultCellStyle = dataGridViewCellStyle1;
            this.Columnas.HeaderText = "Columna";
            this.Columnas.Name = "Columnas";
            this.Columnas.ReadOnly = true;
            // 
            // Disenar
            // 
            this.Disenar.HeaderText = "Graficar";
            this.Disenar.Name = "Disenar";
            this.Disenar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Disenar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.PictureBox2);
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(285, 23);
            this.Panel1.TabIndex = 19;
            this.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBox2.Image = global::DisenoColumnas.Properties.Resources.close_button;
            this.PictureBox2.Location = new System.Drawing.Point(269, 3);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(10, 10);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox2.TabIndex = 13;
            this.PictureBox2.TabStop = false;
            this.PictureBox2.Click += new System.EventHandler(this.PictureBox2_Click);
            this.PictureBox2.MouseLeave += new System.EventHandler(this.PictureBox2_MouseLeave);
            this.PictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox2_MouseMove);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Label9.Location = new System.Drawing.Point(8, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(153, 15);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "Columnas a Graficar Alzado";
            this.Label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label9_MouseDown);
            // 
            // ColumnasaGraficar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 410);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.D_ColGraficar);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ColumnasaGraficar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColumnasaGraficar";
            this.Load += new System.EventHandler(this.ColumnasaGraficar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.D_ColGraficar)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DataGridView D_ColGraficar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Disenar;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.PictureBox PictureBox2;
        internal System.Windows.Forms.Label Label9;
    }
}