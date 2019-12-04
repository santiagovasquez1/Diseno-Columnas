namespace DisenoColumnas.Diseño.Ventanas_Auxiliares__Herramientas_
{
    partial class CambiarNombreColumna
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
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.D_ColLabel = new System.Windows.Forms.DataGridView();
            this.Muros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label9 = new System.Windows.Forms.Label();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D_ColLabel)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.Button2);
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Controls.Add(this.D_ColLabel);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(0, 23);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(306, 391);
            this.Panel2.TabIndex = 17;
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Location = new System.Drawing.Point(164, 355);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(65, 23);
            this.Button2.TabIndex = 2;
            this.Button2.Text = "Cancelar";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(84, 355);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(59, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Ok";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // D_ColLabel
            // 
            this.D_ColLabel.AllowUserToAddRows = false;
            this.D_ColLabel.AllowUserToDeleteRows = false;
            this.D_ColLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D_ColLabel.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.D_ColLabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_ColLabel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Muros,
            this.Label});
            this.D_ColLabel.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.D_ColLabel.GridColor = System.Drawing.SystemColors.Control;
            this.D_ColLabel.Location = new System.Drawing.Point(11, 8);
            this.D_ColLabel.Name = "D_ColLabel";
            this.D_ColLabel.Size = new System.Drawing.Size(282, 343);
            this.D_ColLabel.TabIndex = 0;
            this.D_ColLabel.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.D_ColLabel_CellEndEdit);
            // 
            // Muros
            // 
            this.Muros.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Muros.DefaultCellStyle = dataGridViewCellStyle1;
            this.Muros.HeaderText = "Columna";
            this.Muros.Name = "Muros";
            this.Muros.ReadOnly = true;
            // 
            // Label
            // 
            this.Label.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Label.HeaderText = "Label";
            this.Label.Name = "Label";
            this.Label.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(306, 23);
            this.Panel1.TabIndex = 16;
            this.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Label9.Location = new System.Drawing.Point(8, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(161, 15);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "Editar Nombre de Columnas";
            this.Label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label9_MouseDown);
            // 
            // CambiarNombreColumna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 414);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CambiarNombreColumna";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CambiarNombreColumna";
            this.Load += new System.EventHandler(this.CambiarNombreColumna_Load);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.D_ColLabel)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DataGridView D_ColLabel;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Muros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Label;
    }
}