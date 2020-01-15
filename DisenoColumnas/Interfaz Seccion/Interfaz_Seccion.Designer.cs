namespace DisenoColumnas.Interfaz_Seccion
{
    partial class FInterfaz_Seccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInterfaz_Seccion));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbPisos = new System.Windows.Forms.ListBox();
            this.gbSecciones = new System.Windows.Forms.GroupBox();
            this.cbSecciones = new System.Windows.Forms.ComboBox();
            this.cmEditar_Ref = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarRefuerzoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarRefuerzoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Radio_Des = new System.Windows.Forms.RadioButton();
            this.Radio_Dmo = new System.Windows.Forms.RadioButton();
            this.Button_Diagrama = new System.Windows.Forms.Button();
            this.cmSecciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.agregarSecciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarSecciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Analizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CASOSCARGA = new System.Windows.Forms.ListBox();
            this.Grafica = new DisenoColumnas.Interfaz_Seccion.Controles.DoubleBufferedPictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CreateSection = new System.Windows.Forms.ToolStripButton();
            this.SaveSection = new System.Windows.Forms.ToolStripButton();
            this.tbSeleccionar = new System.Windows.Forms.ToolStripButton();
            this.tsbAddRefuerzo = new System.Windows.Forms.ToolStripDropDownButton();
            this.barraIndiviudalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barraMultipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.gbSecciones.SuspendLayout();
            this.cmEditar_Ref.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cmSecciones.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 516);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "X, Y";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbPisos);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(688, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 393);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de pisos";
            // 
            // lbPisos
            // 
            this.lbPisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPisos.FormattingEnabled = true;
            this.lbPisos.ItemHeight = 15;
            this.lbPisos.Location = new System.Drawing.Point(6, 22);
            this.lbPisos.Name = "lbPisos";
            this.lbPisos.Size = new System.Drawing.Size(154, 364);
            this.lbPisos.TabIndex = 0;
            this.lbPisos.SelectedIndexChanged += new System.EventHandler(this.lbPisos_SelectedIndexChanged);
            // 
            // gbSecciones
            // 
            this.gbSecciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSecciones.Controls.Add(this.cbSecciones);
            this.gbSecciones.Enabled = false;
            this.gbSecciones.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSecciones.ForeColor = System.Drawing.Color.White;
            this.gbSecciones.Location = new System.Drawing.Point(686, 64);
            this.gbSecciones.Name = "gbSecciones";
            this.gbSecciones.Size = new System.Drawing.Size(166, 47);
            this.gbSecciones.TabIndex = 6;
            this.gbSecciones.TabStop = false;
            this.gbSecciones.Text = "Fc secciones";
            // 
            // cbSecciones
            // 
            this.cbSecciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSecciones.FormattingEnabled = true;
            this.cbSecciones.Location = new System.Drawing.Point(6, 18);
            this.cbSecciones.Name = "cbSecciones";
            this.cbSecciones.Size = new System.Drawing.Size(154, 23);
            this.cbSecciones.TabIndex = 0;
            this.cbSecciones.SelectedIndexChanged += new System.EventHandler(this.cbSecciones_SelectedIndexChanged);
            // 
            // cmEditar_Ref
            // 
            this.cmEditar_Ref.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarRefuerzoToolStripMenuItem,
            this.eliminarRefuerzoToolStripMenuItem});
            this.cmEditar_Ref.Name = "cmEditar_Ref";
            this.cmEditar_Ref.Size = new System.Drawing.Size(164, 48);
            // 
            // editarRefuerzoToolStripMenuItem
            // 
            this.editarRefuerzoToolStripMenuItem.Name = "editarRefuerzoToolStripMenuItem";
            this.editarRefuerzoToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.editarRefuerzoToolStripMenuItem.Text = "Editar refuerzo";
            this.editarRefuerzoToolStripMenuItem.Click += new System.EventHandler(this.editarRefuerzoToolStripMenuItem_Click);
            // 
            // eliminarRefuerzoToolStripMenuItem
            // 
            this.eliminarRefuerzoToolStripMenuItem.Name = "eliminarRefuerzoToolStripMenuItem";
            this.eliminarRefuerzoToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.eliminarRefuerzoToolStripMenuItem.Text = "Eliminar refuerzo";
            this.eliminarRefuerzoToolStripMenuItem.Click += new System.EventHandler(this.eliminarRefuerzoToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Radio_Des);
            this.groupBox2.Controls.Add(this.Radio_Dmo);
            this.groupBox2.Enabled = false;
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(688, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 47);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grado de disipación: ";
            this.groupBox2.Visible = false;
            // 
            // Radio_Des
            // 
            this.Radio_Des.AutoSize = true;
            this.Radio_Des.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Des.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Radio_Des.ForeColor = System.Drawing.Color.White;
            this.Radio_Des.Location = new System.Drawing.Point(75, 22);
            this.Radio_Des.Name = "Radio_Des";
            this.Radio_Des.Size = new System.Drawing.Size(45, 19);
            this.Radio_Des.TabIndex = 3;
            this.Radio_Des.Text = "DES";
            this.Radio_Des.UseVisualStyleBackColor = false;
            this.Radio_Des.CheckedChanged += new System.EventHandler(this.Radio_Des_CheckedChanged);
            // 
            // Radio_Dmo
            // 
            this.Radio_Dmo.AutoSize = true;
            this.Radio_Dmo.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Dmo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Radio_Dmo.ForeColor = System.Drawing.Color.White;
            this.Radio_Dmo.Location = new System.Drawing.Point(16, 22);
            this.Radio_Dmo.Name = "Radio_Dmo";
            this.Radio_Dmo.Size = new System.Drawing.Size(53, 19);
            this.Radio_Dmo.TabIndex = 2;
            this.Radio_Dmo.Text = "DMO";
            this.Radio_Dmo.UseVisualStyleBackColor = false;
            this.Radio_Dmo.CheckedChanged += new System.EventHandler(this.Radio_Dmo_CheckedChanged);
            // 
            // Button_Diagrama
            // 
            this.Button_Diagrama.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Diagrama.BackColor = System.Drawing.Color.Gray;
            this.Button_Diagrama.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Diagrama.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_Diagrama.Location = new System.Drawing.Point(691, 504);
            this.Button_Diagrama.Name = "Button_Diagrama";
            this.Button_Diagrama.Size = new System.Drawing.Size(157, 26);
            this.Button_Diagrama.TabIndex = 7;
            this.Button_Diagrama.Text = "Diagrama de Interacción";
            this.Button_Diagrama.UseVisualStyleBackColor = false;
            this.Button_Diagrama.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cmSecciones
            // 
            this.cmSecciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarSecciónToolStripMenuItem,
            this.eliminarSecciónToolStripMenuItem});
            this.cmSecciones.Name = "cmSecciones";
            this.cmSecciones.Size = new System.Drawing.Size(162, 48);
            // 
            // agregarSecciónToolStripMenuItem
            // 
            this.agregarSecciónToolStripMenuItem.Name = "agregarSecciónToolStripMenuItem";
            this.agregarSecciónToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.agregarSecciónToolStripMenuItem.Text = "Agregar Sección";
            this.agregarSecciónToolStripMenuItem.Click += new System.EventHandler(this.agregarSecciónToolStripMenuItem_Click);
            // 
            // eliminarSecciónToolStripMenuItem
            // 
            this.eliminarSecciónToolStripMenuItem.Name = "eliminarSecciónToolStripMenuItem";
            this.eliminarSecciónToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.eliminarSecciónToolStripMenuItem.Text = "Eliminar Sección";
            this.eliminarSecciónToolStripMenuItem.Click += new System.EventHandler(this.eliminarSecciónToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Button_Diagrama);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.gbSecciones);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.Grafica);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(30, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(870, 540);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Location = new System.Drawing.Point(332, 550);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 216);
            this.panel2.TabIndex = 13;
            this.panel2.Visible = false;
            this.panel2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Analizar_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.Analizar);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.CASOSCARGA);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Location = new System.Drawing.Point(6, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 205);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Combinaciones";
            this.groupBox3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Analizar_KeyUp);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(165, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Analizar
            // 
            this.Analizar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Analizar.Location = new System.Drawing.Point(84, 179);
            this.Analizar.Name = "Analizar";
            this.Analizar.Size = new System.Drawing.Size(75, 23);
            this.Analizar.TabIndex = 5;
            this.Analizar.Text = "Continuar";
            this.Analizar.UseVisualStyleBackColor = true;
            this.Analizar.Click += new System.EventHandler(this.Analizar_Click);
            this.Analizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Analizar_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Elija las combinaciones que desea analizar en el diagrama \r\nde interacción.\r\n";
            // 
            // CASOSCARGA
            // 
            this.CASOSCARGA.FormattingEnabled = true;
            this.CASOSCARGA.Location = new System.Drawing.Point(12, 55);
            this.CASOSCARGA.Name = "CASOSCARGA";
            this.CASOSCARGA.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.CASOSCARGA.Size = new System.Drawing.Size(284, 121);
            this.CASOSCARGA.TabIndex = 0;
            this.CASOSCARGA.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Analizar_KeyUp);
            // 
            // Grafica
            // 
            this.Grafica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grafica.BackColor = System.Drawing.Color.White;
            this.Grafica.Location = new System.Drawing.Point(4, 16);
            this.Grafica.Name = "Grafica";
            this.Grafica.Size = new System.Drawing.Size(677, 494);
            this.Grafica.TabIndex = 9;
            this.Grafica.TabStop = false;
            this.Grafica.Paint += new System.Windows.Forms.PaintEventHandler(this.Grafica_Paint);
            this.Grafica.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseDown);
            this.Grafica.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseMove);
            this.Grafica.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FInterfaz_Seccion_Scroll);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateSection,
            this.SaveSection,
            this.tbSeleccionar,
            this.tsbAddRefuerzo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(30, 540);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CreateSection
            // 
            this.CreateSection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CreateSection.Image = global::DisenoColumnas.Properties.Resources.AgregarCol2;
            this.CreateSection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateSection.Name = "CreateSection";
            this.CreateSection.Size = new System.Drawing.Size(27, 20);
            this.CreateSection.Text = "Agregar Sección";
            this.CreateSection.Visible = false;
            this.CreateSection.Click += new System.EventHandler(this.ToolStripButton7_Click);
            // 
            // SaveSection
            // 
            this.SaveSection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveSection.Image = global::DisenoColumnas.Properties.Resources.SaveSection;
            this.SaveSection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveSection.Name = "SaveSection";
            this.SaveSection.Size = new System.Drawing.Size(27, 20);
            this.SaveSection.Text = "Guardar Secciones";
            this.SaveSection.Visible = false;
            this.SaveSection.Click += new System.EventHandler(this.ToolStripButton8_Click);
            // 
            // tbSeleccionar
            // 
            this.tbSeleccionar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbSeleccionar.Image = global::DisenoColumnas.Properties.Resources.cursor;
            this.tbSeleccionar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSeleccionar.Name = "tbSeleccionar";
            this.tbSeleccionar.Size = new System.Drawing.Size(27, 20);
            this.tbSeleccionar.Text = "Seleccionar";
            this.tbSeleccionar.ToolTipText = "Seleccionar sección";
            this.tbSeleccionar.Click += new System.EventHandler(this.tbSeleccionar_Click);
            // 
            // tsbAddRefuerzo
            // 
            this.tsbAddRefuerzo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddRefuerzo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barraIndiviudalToolStripMenuItem,
            this.barraMultipleToolStripMenuItem,
            this.toolStripMenuItem2});
            this.tsbAddRefuerzo.Image = global::DisenoColumnas.Properties.Resources.anadir;
            this.tsbAddRefuerzo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddRefuerzo.Name = "tsbAddRefuerzo";
            this.tsbAddRefuerzo.Size = new System.Drawing.Size(27, 20);
            this.tsbAddRefuerzo.Text = "Agregar refuerzo";
            this.tsbAddRefuerzo.ToolTipText = "Agregar refuerzo a la sección";
            // 
            // barraIndiviudalToolStripMenuItem
            // 
            this.barraIndiviudalToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.BarraIndividual1;
            this.barraIndiviudalToolStripMenuItem.Name = "barraIndiviudalToolStripMenuItem";
            this.barraIndiviudalToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.barraIndiviudalToolStripMenuItem.Text = "Dibujar Barra Individual";
            this.barraIndiviudalToolStripMenuItem.Click += new System.EventHandler(this.BarraIndiviudalToolStripMenuItem_Click);
            // 
            // barraMultipleToolStripMenuItem
            // 
            this.barraMultipleToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.BarraMultiple;
            this.barraMultipleToolStripMenuItem.Name = "barraMultipleToolStripMenuItem";
            this.barraMultipleToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.barraMultipleToolStripMenuItem.Text = "Dibujar Línea de Barras ";
            this.barraMultipleToolStripMenuItem.Click += new System.EventHandler(this.BarraMultipleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::DisenoColumnas.Properties.Resources.BarrasCuadro;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenuItem2.Text = "Dibujar Rectangulo de Barras";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // FInterfaz_Seccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FInterfaz_Seccion";
            this.Text = "Sección";
            this.Load += new System.EventHandler(this.Interfaz_Seccion_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FInterfaz_Seccion_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FInterfaz_Seccion_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.gbSecciones.ResumeLayout(false);
            this.cmEditar_Ref.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.cmSecciones.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbPisos;
        private System.Windows.Forms.GroupBox gbSecciones;
        private System.Windows.Forms.ComboBox cbSecciones;
        private System.Windows.Forms.ContextMenuStrip cmEditar_Ref;
        private System.Windows.Forms.ToolStripMenuItem eliminarRefuerzoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarRefuerzoToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.RadioButton Radio_Dmo;
        internal System.Windows.Forms.RadioButton Radio_Des;
        private System.Windows.Forms.Button Button_Diagrama;
        private System.Windows.Forms.ContextMenuStrip cmSecciones;
        private System.Windows.Forms.ToolStripMenuItem agregarSecciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarSecciónToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CreateSection;
        private System.Windows.Forms.ToolStripButton SaveSection;
        private System.Windows.Forms.ToolStripDropDownButton tsbAddRefuerzo;
        private System.Windows.Forms.ToolStripMenuItem barraIndiviudalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barraMultipleToolStripMenuItem;
        internal Controles.DoubleBufferedPictureBox Grafica;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Analizar;
        private System.Windows.Forms.ListBox CASOSCARGA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton tbSeleccionar;
    }
}
