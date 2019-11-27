namespace DisenoColumnas.Diseño
{
    partial class ColSimilares
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
            this.D_ColSim = new System.Windows.Forms.DataGridView();
            this.Muros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Similars = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ListadeMuros = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label9 = new System.Windows.Forms.Label();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D_ColSim)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.Button2);
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Controls.Add(this.D_ColSim);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(0, 23);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(388, 392);
            this.Panel2.TabIndex = 15;
            this.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseDown);
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Location = new System.Drawing.Point(205, 357);
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
            this.Button1.Location = new System.Drawing.Point(125, 357);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(59, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Ok";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // D_ColSim
            // 
            this.D_ColSim.AllowUserToAddRows = false;
            this.D_ColSim.AllowUserToDeleteRows = false;
            this.D_ColSim.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.D_ColSim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_ColSim.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Muros,
            this.Similars,
            this.ListadeMuros});
            this.D_ColSim.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.D_ColSim.GridColor = System.Drawing.SystemColors.Control;
            this.D_ColSim.Location = new System.Drawing.Point(12, 5);
            this.D_ColSim.Name = "D_ColSim";
            this.D_ColSim.Size = new System.Drawing.Size(365, 343);
            this.D_ColSim.TabIndex = 0;
            this.D_ColSim.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.D_ColSim_CellBeginEdit);
            this.D_ColSim.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.D_ColSim_CellClick);
            this.D_ColSim.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.D_ColSim_CellEndEdit);
            // 
            // Muros
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Muros.DefaultCellStyle = dataGridViewCellStyle1;
            this.Muros.HeaderText = "Columna";
            this.Muros.Name = "Muros";
            this.Muros.ReadOnly = true;
            // 
            // Similars
            // 
            this.Similars.HeaderText = "Maestro";
            this.Similars.Name = "Similars";
            this.Similars.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Similars.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ListadeMuros
            // 
            this.ListadeMuros.HeaderText = "Igual a:";
            this.ListadeMuros.Name = "ListadeMuros";
            this.ListadeMuros.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ListadeMuros.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(388, 23);
            this.Panel1.TabIndex = 14;
            this.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Label9.Location = new System.Drawing.Point(8, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(100, 15);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "Columnas Iguales";
            this.Label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label9_MouseDown);
            // 
            // ColSimilares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 415);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ColSimilares";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColSimilares";
            this.Load += new System.EventHandler(this.ColSimilares_Load);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.D_ColSim)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DataGridView D_ColSim;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Muros;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Similars;
        private System.Windows.Forms.DataGridViewComboBoxColumn ListadeMuros;
    }
}