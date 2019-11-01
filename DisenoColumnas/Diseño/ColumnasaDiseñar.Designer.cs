namespace DisenoColumnas.Diseño
{
    partial class ColumnasaDiseñar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.D_ColDiseno = new System.Windows.Forms.DataGridView();
            this.Columnas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Disenar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D_ColDiseno)).BeginInit();
            this.SuspendLayout();
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
            this.Panel1.Size = new System.Drawing.Size(288, 23);
            this.Panel1.TabIndex = 14;
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
            this.Label9.Size = new System.Drawing.Size(113, 15);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "Columnas a Diseñar";
            this.Label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label9_MouseDown);
            // 
            // D_ColDiseno
            // 
            this.D_ColDiseno.AllowUserToAddRows = false;
            this.D_ColDiseno.AllowUserToDeleteRows = false;
            this.D_ColDiseno.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.D_ColDiseno.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_ColDiseno.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnas,
            this.Disenar});
            this.D_ColDiseno.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.D_ColDiseno.GridColor = System.Drawing.SystemColors.Control;
            this.D_ColDiseno.Location = new System.Drawing.Point(12, 38);
            this.D_ColDiseno.Name = "D_ColDiseno";
            this.D_ColDiseno.Size = new System.Drawing.Size(263, 343);
            this.D_ColDiseno.TabIndex = 15;
            // 
            // Columnas
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Columnas.DefaultCellStyle = dataGridViewCellStyle3;
            this.Columnas.HeaderText = "Columna";
            this.Columnas.Name = "Columnas";
            this.Columnas.ReadOnly = true;
            // 
            // Disenar
            // 
            this.Disenar.HeaderText = "Diseñar";
            this.Disenar.Name = "Disenar";
            this.Disenar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Disenar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Button3
            // 
            this.Button3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button3.Location = new System.Drawing.Point(15, 391);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(115, 23);
            this.Button3.TabIndex = 18;
            this.Button3.Text = "Seleccionar todo";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Location = new System.Drawing.Point(201, 391);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(65, 23);
            this.Button2.TabIndex = 17;
            this.Button2.Text = "Cancelar";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(136, 391);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(59, 23);
            this.Button1.TabIndex = 16;
            this.Button1.Text = "Ok";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ColumnasaDiseñar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 427);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.D_ColDiseno);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ColumnasaDiseñar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColumnasaDiseñar";
            this.Load += new System.EventHandler(this.ColumnasaDiseñar_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D_ColDiseno)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.PictureBox PictureBox2;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.DataGridView D_ColDiseno;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Disenar;
    }
}