namespace DisenoColumnas.Interfaz_Seccion
{
    partial class DiagramaInteraccion
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramaInteraccion));
            this.gl = new OpenTK.GLControl();
            this.Redraw = new System.Windows.Forms.Timer(this.components);
            this.CharMomentos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.GroupBox_Grafica_Diagrama1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MostrarSolicita = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.D_MnPn = new System.Windows.Forms.DataGridView();
            this.P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.My = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verSolicitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.ChangeAngulo_Abajo = new System.Windows.Forms.Button();
            this.ChangeAngulo_Arriba = new System.Windows.Forms.Button();
            this.X_mas = new System.Windows.Forms.ToolStripButton();
            this.X_menos = new System.Windows.Forms.ToolStripButton();
            this.Y_mas = new System.Windows.Forms.ToolStripButton();
            this.Y_menos = new System.Windows.Forms.ToolStripButton();
            this.Z_mas = new System.Windows.Forms.ToolStripButton();
            this.Z_menos = new System.Windows.Forms.ToolStripButton();
            this.sinSolicitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.CharMomentos)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.GroupBox_Grafica_Diagrama1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D_MnPn)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gl
            // 
            this.gl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gl.Location = new System.Drawing.Point(11, 17);
            this.gl.Name = "gl";
            this.gl.Size = new System.Drawing.Size(336, 273);
            this.gl.TabIndex = 9;
            this.gl.VSync = false;
            this.gl.Load += new System.EventHandler(this.Gl_Load);
            this.gl.Paint += new System.Windows.Forms.PaintEventHandler(this.Gl_Paint);
            // 
            // Redraw
            // 
            this.Redraw.Enabled = true;
            this.Redraw.Tick += new System.EventHandler(this.Redraw_Tick);
            // 
            // CharMomentos
            // 
            this.CharMomentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CharMomentos.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.CharMomentos.ChartAreas.Add(chartArea1);
            this.CharMomentos.ContextMenuStrip = this.contextMenuStrip1;
            this.CharMomentos.Location = new System.Drawing.Point(22, 21);
            this.CharMomentos.Name = "CharMomentos";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            this.CharMomentos.Series.Add(series1);
            this.CharMomentos.Size = new System.Drawing.Size(346, 235);
            this.CharMomentos.TabIndex = 16;
            this.CharMomentos.Text = "chart1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.X_mas,
            this.X_menos,
            this.Y_mas,
            this.Y_menos,
            this.Z_mas,
            this.Z_menos});
            this.toolStrip1.Location = new System.Drawing.Point(362, 18);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 279);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // GroupBox_Grafica_Diagrama1
            // 
            this.GroupBox_Grafica_Diagrama1.Controls.Add(this.CharMomentos);
            this.GroupBox_Grafica_Diagrama1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GroupBox_Grafica_Diagrama1.Location = new System.Drawing.Point(580, 66);
            this.GroupBox_Grafica_Diagrama1.Name = "GroupBox_Grafica_Diagrama1";
            this.GroupBox_Grafica_Diagrama1.Size = new System.Drawing.Size(386, 266);
            this.GroupBox_Grafica_Diagrama1.TabIndex = 19;
            this.GroupBox_Grafica_Diagrama1.TabStop = false;
            this.GroupBox_Grafica_Diagrama1.Text = "Angulo de 60°";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Controls.Add(this.gl);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(577, 338);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 300);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.Title);
            this.panel5.Controls.Add(this.PictureBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(991, 26);
            this.panel5.TabIndex = 25;
            this.panel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel5_MouseDown);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.Black;
            this.Title.Location = new System.Drawing.Point(9, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(159, 15);
            this.Title.TabIndex = 24;
            this.Title.Text = "Diagrama de Interacción - 0°";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MostrarSolicita);
            this.panel1.Controls.Add(this.ChangeAngulo_Abajo);
            this.panel1.Controls.Add(this.ChangeAngulo_Arriba);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.D_MnPn);
            this.panel1.Controls.Add(this.GroupBox_Grafica_Diagrama1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 685);
            this.panel1.TabIndex = 26;
            // 
            // MostrarSolicita
            // 
            this.MostrarSolicita.AutoSize = true;
            this.MostrarSolicita.Location = new System.Drawing.Point(24, 649);
            this.MostrarSolicita.Name = "MostrarSolicita";
            this.MostrarSolicita.Size = new System.Drawing.Size(148, 18);
            this.MostrarSolicita.TabIndex = 29;
            this.MostrarSolicita.Text = "Mostrar Solicitaciones";
            this.MostrarSolicita.UseVisualStyleBackColor = true;
            this.MostrarSolicita.CheckedChanged += new System.EventHandler(this.MostrarSolicita_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(150, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 19);
            this.label2.TabIndex = 25;
            this.label2.Text = "f";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(61, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(18, 19);
            this.Label1.TabIndex = 24;
            this.Label1.Text = "f";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(108, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(45, 18);
            this.radioButton2.TabIndex = 23;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Con";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(42, 18);
            this.radioButton1.TabIndex = 22;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Sin";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // D_MnPn
            // 
            this.D_MnPn.AllowUserToAddRows = false;
            this.D_MnPn.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.D_MnPn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_MnPn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.P,
            this.Mx,
            this.My});
            this.D_MnPn.Location = new System.Drawing.Point(24, 66);
            this.D_MnPn.Name = "D_MnPn";
            this.D_MnPn.ReadOnly = true;
            this.D_MnPn.Size = new System.Drawing.Size(531, 572);
            this.D_MnPn.TabIndex = 21;
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
            // Mx
            // 
            this.Mx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Mx.HeaderText = "Mx [Ton-m]";
            this.Mx.Name = "Mx";
            this.Mx.ReadOnly = true;
            this.Mx.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Mx.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // My
            // 
            this.My.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.My.HeaderText = "My [Ton-m]";
            this.My.Name = "My";
            this.My.ReadOnly = true;
            this.My.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.My.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verSolicitacionesToolStripMenuItem,
            this.sinSolicitacionesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // verSolicitacionesToolStripMenuItem
            // 
            this.verSolicitacionesToolStripMenuItem.Name = "verSolicitacionesToolStripMenuItem";
            this.verSolicitacionesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verSolicitacionesToolStripMenuItem.Text = "Ver Solicitaciones";
            this.verSolicitacionesToolStripMenuItem.Click += new System.EventHandler(this.VerSolicitacionesToolStripMenuItem_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBox1.Image = global::DisenoColumnas.Properties.Resources.close_button1;
            this.PictureBox1.Location = new System.Drawing.Point(967, 9);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(10, 10);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 12;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            this.PictureBox1.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // ChangeAngulo_Abajo
            // 
            this.ChangeAngulo_Abajo.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.ChangeAngulo_Abajo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChangeAngulo_Abajo.Image = global::DisenoColumnas.Properties.Resources.AnguloIzquierda;
            this.ChangeAngulo_Abajo.Location = new System.Drawing.Point(455, 644);
            this.ChangeAngulo_Abajo.Name = "ChangeAngulo_Abajo";
            this.ChangeAngulo_Abajo.Size = new System.Drawing.Size(37, 23);
            this.ChangeAngulo_Abajo.TabIndex = 27;
            this.ChangeAngulo_Abajo.UseVisualStyleBackColor = true;
            this.ChangeAngulo_Abajo.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ChangeAngulo_Arriba
            // 
            this.ChangeAngulo_Arriba.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.ChangeAngulo_Arriba.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChangeAngulo_Arriba.Image = global::DisenoColumnas.Properties.Resources.angulo_de_flecha_derecha;
            this.ChangeAngulo_Arriba.Location = new System.Drawing.Point(508, 644);
            this.ChangeAngulo_Arriba.Name = "ChangeAngulo_Arriba";
            this.ChangeAngulo_Arriba.Size = new System.Drawing.Size(37, 23);
            this.ChangeAngulo_Arriba.TabIndex = 26;
            this.ChangeAngulo_Arriba.UseVisualStyleBackColor = true;
            this.ChangeAngulo_Arriba.Click += new System.EventHandler(this.Button1_Click);
            // 
            // X_mas
            // 
            this.X_mas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.X_mas.Image = ((System.Drawing.Image)(resources.GetObject("X_mas.Image")));
            this.X_mas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.X_mas.Name = "X_mas";
            this.X_mas.Size = new System.Drawing.Size(21, 20);
            this.X_mas.Text = "X+";
            this.X_mas.Click += new System.EventHandler(this.X_mas_Click);
            // 
            // X_menos
            // 
            this.X_menos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.X_menos.Image = ((System.Drawing.Image)(resources.GetObject("X_menos.Image")));
            this.X_menos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.X_menos.Name = "X_menos";
            this.X_menos.Size = new System.Drawing.Size(21, 20);
            this.X_menos.Text = "X-";
            this.X_menos.Click += new System.EventHandler(this.X_menos_Click);
            // 
            // Y_mas
            // 
            this.Y_mas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Y_mas.Image = ((System.Drawing.Image)(resources.GetObject("Y_mas.Image")));
            this.Y_mas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Y_mas.Name = "Y_mas";
            this.Y_mas.Size = new System.Drawing.Size(21, 20);
            this.Y_mas.Text = "Y+";
            this.Y_mas.Click += new System.EventHandler(this.Y_mas_Click);
            // 
            // Y_menos
            // 
            this.Y_menos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Y_menos.Image = ((System.Drawing.Image)(resources.GetObject("Y_menos.Image")));
            this.Y_menos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Y_menos.Name = "Y_menos";
            this.Y_menos.Size = new System.Drawing.Size(21, 20);
            this.Y_menos.Text = "Y-";
            this.Y_menos.Click += new System.EventHandler(this.Y_menos_Click);
            // 
            // Z_mas
            // 
            this.Z_mas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Z_mas.Image = ((System.Drawing.Image)(resources.GetObject("Z_mas.Image")));
            this.Z_mas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Z_mas.Name = "Z_mas";
            this.Z_mas.Size = new System.Drawing.Size(21, 20);
            this.Z_mas.Text = "Z+";
            this.Z_mas.Click += new System.EventHandler(this.Z_mas_Click);
            // 
            // Z_menos
            // 
            this.Z_menos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Z_menos.Image = ((System.Drawing.Image)(resources.GetObject("Z_menos.Image")));
            this.Z_menos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Z_menos.Name = "Z_menos";
            this.Z_menos.Size = new System.Drawing.Size(21, 20);
            this.Z_menos.Text = "Z-";
            this.Z_menos.Click += new System.EventHandler(this.Z_menos_Click);
            // 
            // sinSolicitacionesToolStripMenuItem
            // 
            this.sinSolicitacionesToolStripMenuItem.Name = "sinSolicitacionesToolStripMenuItem";
            this.sinSolicitacionesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sinSolicitacionesToolStripMenuItem.Text = "Sin Solicitaciones";
            this.sinSolicitacionesToolStripMenuItem.Click += new System.EventHandler(this.SinSolicitacionesToolStripMenuItem_Click);
            // 
            // DiagramaInteraccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(991, 685);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagramaInteraccion";
            this.Text = "Diagrama de Interacción";
            this.Load += new System.EventHandler(this.DiagramaInteraccion_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DiagramaInteraccion_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.CharMomentos)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.GroupBox_Grafica_Diagrama1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D_MnPn)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl gl;
        private System.Windows.Forms.Timer Redraw;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton X_mas;
        private System.Windows.Forms.ToolStripButton X_menos;
        private System.Windows.Forms.ToolStripButton Y_mas;
        private System.Windows.Forms.ToolStripButton Y_menos;
        private System.Windows.Forms.ToolStripButton Z_mas;
        private System.Windows.Forms.ToolStripButton Z_menos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridView D_MnPn;
        private System.Windows.Forms.Button ChangeAngulo_Abajo;
        private System.Windows.Forms.Button ChangeAngulo_Arriba;
        private System.Windows.Forms.CheckBox MostrarSolicita;
        private System.Windows.Forms.DataGridViewTextBoxColumn P;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mx;
        private System.Windows.Forms.DataGridViewTextBoxColumn My;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem verSolicitacionesToolStripMenuItem;
        internal System.Windows.Forms.GroupBox GroupBox_Grafica_Diagrama1;
        internal System.Windows.Forms.DataVisualization.Charting.Chart CharMomentos;
        private System.Windows.Forms.ToolStripMenuItem sinSolicitacionesToolStripMenuItem;
    }
}