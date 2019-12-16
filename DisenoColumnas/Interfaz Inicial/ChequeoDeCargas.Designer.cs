namespace DisenoColumnas.Interfaz_Inicial
{
    partial class ChequeoDeCargas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChequeoDeCargas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Analizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CASOSCARGA = new System.Windows.Forms.ListBox();
            this.DataInfo = new System.Windows.Forms.DataGridView();
            this.Columna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sección = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Piso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Combionación = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FcAg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnas_List = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ReporteColumnas = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.B_Salir = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Analizar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CASOSCARGA);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(19, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Combinaciones";
            this.groupBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // Analizar
            // 
            this.Analizar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Analizar.Location = new System.Drawing.Point(116, 179);
            this.Analizar.Name = "Analizar";
            this.Analizar.Size = new System.Drawing.Size(75, 23);
            this.Analizar.TabIndex = 5;
            this.Analizar.Text = "Analizar";
            this.Analizar.UseVisualStyleBackColor = true;
            this.Analizar.Click += new System.EventHandler(this.Button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "El programa continuara hasta que la carga sea \r\nmenor a 0.4*Ag*f´c de las combina" +
    "ciones elegidas.\r\n";
            // 
            // CASOSCARGA
            // 
            this.CASOSCARGA.FormattingEnabled = true;
            this.CASOSCARGA.ItemHeight = 14;
            this.CASOSCARGA.Location = new System.Drawing.Point(12, 55);
            this.CASOSCARGA.Name = "CASOSCARGA";
            this.CASOSCARGA.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.CASOSCARGA.Size = new System.Drawing.Size(284, 116);
            this.CASOSCARGA.TabIndex = 0;
            // 
            // DataInfo
            // 
            this.DataInfo.AllowUserToAddRows = false;
            this.DataInfo.AllowUserToDeleteRows = false;
            this.DataInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DataInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columna,
            this.Sección,
            this.Piso,
            this.Combionación,
            this.FcAg,
            this.P});
            this.DataInfo.Location = new System.Drawing.Point(19, 248);
            this.DataInfo.Name = "DataInfo";
            this.DataInfo.ReadOnly = true;
            this.DataInfo.Size = new System.Drawing.Size(723, 332);
            this.DataInfo.TabIndex = 1;
            // 
            // Columna
            // 
            this.Columna.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columna.HeaderText = "Columna";
            this.Columna.Name = "Columna";
            this.Columna.ReadOnly = true;
            this.Columna.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Columna.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Sección
            // 
            this.Sección.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sección.HeaderText = "Sección";
            this.Sección.Name = "Sección";
            this.Sección.ReadOnly = true;
            this.Sección.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Sección.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Piso
            // 
            this.Piso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Piso.HeaderText = "Story";
            this.Piso.Name = "Piso";
            this.Piso.ReadOnly = true;
            this.Piso.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Piso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Combionación
            // 
            this.Combionación.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Combionación.HeaderText = "Combinación";
            this.Combionación.Name = "Combionación";
            this.Combionación.ReadOnly = true;
            this.Combionación.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Combionación.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FcAg
            // 
            this.FcAg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FcAg.HeaderText = "0.4*f\'c*Ag [Ton]";
            this.FcAg.Name = "FcAg";
            this.FcAg.ReadOnly = true;
            this.FcAg.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FcAg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // P
            // 
            this.P.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.P.HeaderText = "P [Ton]";
            this.P.Name = "P";
            this.P.ReadOnly = true;
            this.P.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.P.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Columnas_List
            // 
            this.Columnas_List.FormattingEnabled = true;
            this.Columnas_List.Location = new System.Drawing.Point(658, 219);
            this.Columnas_List.Name = "Columnas_List";
            this.Columnas_List.Size = new System.Drawing.Size(83, 22);
            this.Columnas_List.TabIndex = 2;
            this.Columnas_List.SelectedIndexChanged += new System.EventHandler(this.Columnas_List_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(584, 586);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Button_OK.Location = new System.Drawing.Point(665, 586);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 4;
            this.Button_OK.Text = "Ok";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(372, 584);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Mostrar Reporte";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(591, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Columna:";
            // 
            // ReporteColumnas
            // 
            this.ReporteColumnas.FormattingEnabled = true;
            this.ReporteColumnas.ItemHeight = 14;
            this.ReporteColumnas.Location = new System.Drawing.Point(9, 24);
            this.ReporteColumnas.Name = "ReporteColumnas";
            this.ReporteColumnas.Size = new System.Drawing.Size(376, 172);
            this.ReporteColumnas.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Analizar Cargas\r\n";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.B_Salir);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Columnas_List);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 620);
            this.panel1.TabIndex = 8;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // B_Salir
            // 
            this.B_Salir.Enabled = false;
            this.B_Salir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.B_Salir.Location = new System.Drawing.Point(502, 585);
            this.B_Salir.Name = "B_Salir";
            this.B_Salir.Size = new System.Drawing.Size(75, 23);
            this.B_Salir.TabIndex = 9;
            this.B_Salir.Text = "Salir";
            this.B_Salir.UseVisualStyleBackColor = true;
            this.B_Salir.Click += new System.EventHandler(this.Button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ReporteColumnas);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox2.Location = new System.Drawing.Point(343, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 202);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Columnas que NO Cumplen.";
            this.groupBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // ChequeoDeCargas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(760, 620);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DataInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChequeoDeCargas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chequear Cargas";
            this.Load += new System.EventHandler(this.ChequeoDeCargas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox CASOSCARGA;
        private System.Windows.Forms.DataGridView DataInfo;
        private System.Windows.Forms.ComboBox Columnas_List;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Analizar;
        private System.Windows.Forms.ListBox ReporteColumnas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sección;
        private System.Windows.Forms.DataGridViewTextBoxColumn Piso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Combionación;
        private System.Windows.Forms.DataGridViewTextBoxColumn FcAg;
        private System.Windows.Forms.DataGridViewTextBoxColumn P;
        private System.Windows.Forms.Button B_Salir;
    }
}