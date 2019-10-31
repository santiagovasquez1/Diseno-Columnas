namespace DisenoColumnas
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel5 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarAlzadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PanelContenedor = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.label3 = new System.Windows.Forms.Label();
            this.LColumna = new System.Windows.Forms.ComboBox();
            this.La_Column = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Disenar = new System.Windows.Forms.Button();
            this.Button_Agregar = new System.Windows.Forms.Button();
            this.SaveAs_B = new System.Windows.Forms.Button();
            this.Save_B = new System.Windows.Forms.Button();
            this.Cuantia_Vol_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.Button_Minimize = new System.Windows.Forms.Button();
            this.Button_MaxRest = new System.Windows.Forms.Button();
            this.Button_Cerrar = new System.Windows.Forms.Button();
            this.variablesDeEntradaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnasIgualesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarSeccionesPredeterminadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plantaDeColumnasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infromaciónDeColumnasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dibujoSecciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estribosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.panel5.Controls.Add(this.Button_Minimize);
            this.panel5.Controls.Add(this.Button_MaxRest);
            this.panel5.Controls.Add(this.Button_Cerrar);
            this.panel5.Controls.Add(this.menuStrip1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1069, 26);
            this.panel5.TabIndex = 24;
            this.panel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel5_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.verToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 2);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(205, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.archivoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.NuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.GuardarComoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variablesDeEntradaToolStripMenuItem,
            this.columnasIgualesToolStripMenuItem,
            this.editarSeccionesPredeterminadasToolStripMenuItem});
            this.editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plantaDeColumnasToolStripMenuItem,
            this.infromaciónDeColumnasToolStripMenuItem,
            this.dibujoSecciónToolStripMenuItem,
            this.estribosToolStripMenuItem,
            this.agregarAlzadoToolStripMenuItem});
            this.verToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.verToolStripMenuItem.Text = "Ver";
            // 
            // agregarAlzadoToolStripMenuItem
            // 
            this.agregarAlzadoToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.vcsadded_93506;
            this.agregarAlzadoToolStripMenuItem.Name = "agregarAlzadoToolStripMenuItem";
            this.agregarAlzadoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.agregarAlzadoToolStripMenuItem.Text = "Agregar Alzado";
            this.agregarAlzadoToolStripMenuItem.Click += new System.EventHandler(this.AgregarAlzadoToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 660);
            this.panel1.TabIndex = 26;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.statusStrip1.Location = new System.Drawing.Point(0, 711);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1069, 22);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PanelContenedor
            // 
            this.PanelContenedor.BackColor = System.Drawing.Color.Gray;
            this.PanelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenedor.Location = new System.Drawing.Point(205, 51);
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(864, 660);
            this.PanelContenedor.TabIndex = 28;
            this.PanelContenedor.ActivePaneChanged += new System.EventHandler(this.PanelContenedor_ActivePaneChanged);
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1});
            this.toolBar.Location = new System.Drawing.Point(0, 26);
            this.toolBar.Name = "toolBar";
            this.toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolBar.Size = new System.Drawing.Size(1069, 25);
            this.toolBar.TabIndex = 30;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(342, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "|";
            // 
            // LColumna
            // 
            this.LColumna.Enabled = false;
            this.LColumna.FormattingEnabled = true;
            this.LColumna.Location = new System.Drawing.Point(271, 26);
            this.LColumna.Name = "LColumna";
            this.LColumna.Size = new System.Drawing.Size(68, 21);
            this.LColumna.TabIndex = 39;
            this.LColumna.Tag = "1";
            this.LColumna.SelectedIndexChanged += new System.EventHandler(this.LColumna_SelectedIndexChanged);
            // 
            // La_Column
            // 
            this.La_Column.AutoSize = true;
            this.La_Column.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.La_Column.Enabled = false;
            this.La_Column.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.La_Column.ForeColor = System.Drawing.Color.White;
            this.La_Column.Location = new System.Drawing.Point(208, 29);
            this.La_Column.Name = "La_Column";
            this.La_Column.Size = new System.Drawing.Size(59, 15);
            this.La_Column.TabIndex = 40;
            this.La_Column.Text = "Columna:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(509, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 19);
            this.label1.TabIndex = 44;
            this.label1.Text = "|";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(605, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 19);
            this.label2.TabIndex = 48;
            this.label2.Text = "|";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de Column Disigner";
            // 
            // Disenar
            // 
            this.Disenar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Disenar.Enabled = false;
            this.Disenar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Disenar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(247)))));
            this.Disenar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.Disenar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Disenar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Disenar.ForeColor = System.Drawing.Color.White;
            this.Disenar.Image = global::DisenoColumnas.Properties.Resources.cOLUMNAiNVERTIDAX16;
            this.Disenar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Disenar.Location = new System.Drawing.Point(627, 26);
            this.Disenar.Name = "Disenar";
            this.Disenar.Size = new System.Drawing.Size(82, 24);
            this.Disenar.TabIndex = 47;
            this.Disenar.Text = "     Diseñar";
            this.Disenar.UseVisualStyleBackColor = false;
            this.Disenar.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button_Agregar
            // 
            this.Button_Agregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Button_Agregar.Enabled = false;
            this.Button_Agregar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Button_Agregar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(247)))));
            this.Button_Agregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.Button_Agregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Agregar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Agregar.ForeColor = System.Drawing.Color.White;
            this.Button_Agregar.Image = global::DisenoColumnas.Properties.Resources.vcsadded_93506x16;
            this.Button_Agregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_Agregar.Location = new System.Drawing.Point(523, 26);
            this.Button_Agregar.Name = "Button_Agregar";
            this.Button_Agregar.Size = new System.Drawing.Size(76, 24);
            this.Button_Agregar.TabIndex = 43;
            this.Button_Agregar.Text = "     Agregar";
            this.Button_Agregar.UseVisualStyleBackColor = false;
            this.Button_Agregar.Click += new System.EventHandler(this.Button_Agregar_Click);
            // 
            // SaveAs_B
            // 
            this.SaveAs_B.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.SaveAs_B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(247)))));
            this.SaveAs_B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.SaveAs_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveAs_B.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveAs_B.ForeColor = System.Drawing.Color.Black;
            this.SaveAs_B.Image = global::DisenoColumnas.Properties.Resources.SaveAllx13;
            this.SaveAs_B.Location = new System.Drawing.Point(96, 25);
            this.SaveAs_B.Name = "SaveAs_B";
            this.SaveAs_B.Size = new System.Drawing.Size(24, 24);
            this.SaveAs_B.TabIndex = 37;
            this.SaveAs_B.UseVisualStyleBackColor = true;
            this.SaveAs_B.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Save_B
            // 
            this.Save_B.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Save_B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(247)))));
            this.Save_B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.Save_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_B.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_B.ForeColor = System.Drawing.Color.Black;
            this.Save_B.Image = global::DisenoColumnas.Properties.Resources.SaveX13;
            this.Save_B.Location = new System.Drawing.Point(68, 25);
            this.Save_B.Name = "Save_B";
            this.Save_B.Size = new System.Drawing.Size(24, 24);
            this.Save_B.TabIndex = 38;
            this.Save_B.UseVisualStyleBackColor = true;
            this.Save_B.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Cuantia_Vol_Button
            // 
            this.Cuantia_Vol_Button.Enabled = false;
            this.Cuantia_Vol_Button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Cuantia_Vol_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(247)))));
            this.Cuantia_Vol_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(222)))), ((int)(((byte)(245)))));
            this.Cuantia_Vol_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cuantia_Vol_Button.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cuantia_Vol_Button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Cuantia_Vol_Button.Image = global::DisenoColumnas.Properties.Resources.image;
            this.Cuantia_Vol_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cuantia_Vol_Button.Location = new System.Drawing.Point(356, 25);
            this.Cuantia_Vol_Button.Name = "Cuantia_Vol_Button";
            this.Cuantia_Vol_Button.Size = new System.Drawing.Size(152, 23);
            this.Cuantia_Vol_Button.TabIndex = 35;
            this.Cuantia_Vol_Button.Text = "       Cuantia Volumetrica";
            this.Cuantia_Vol_Button.UseVisualStyleBackColor = true;
            this.Cuantia_Vol_Button.Click += new System.EventHandler(this.Cb_cuantiavol_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DisenoColumnas.Properties.Resources.efe_Prima_Ce_Pixelado1;
            this.pictureBox1.Location = new System.Drawing.Point(11, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 179);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 70;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::DisenoColumnas.Properties.Resources.Newx16;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Nuevo (Ctrl + N)";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::DisenoColumnas.Properties.Resources.Openx16;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.ToolStripButton2_Click);
            // 
            // Button_Minimize
            // 
            this.Button_Minimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_Minimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Button_Minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Button_Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.Button_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Minimize.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Minimize.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_Minimize.Image = global::DisenoColumnas.Properties.Resources.Minimizex16;
            this.Button_Minimize.Location = new System.Drawing.Point(949, 0);
            this.Button_Minimize.Name = "Button_Minimize";
            this.Button_Minimize.Size = new System.Drawing.Size(40, 26);
            this.Button_Minimize.TabIndex = 27;
            this.Button_Minimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_Minimize.UseVisualStyleBackColor = true;
            this.Button_Minimize.Click += new System.EventHandler(this.Minimized_Click);
            // 
            // Button_MaxRest
            // 
            this.Button_MaxRest.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_MaxRest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Button_MaxRest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Button_MaxRest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.Button_MaxRest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_MaxRest.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_MaxRest.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_MaxRest.Image = global::DisenoColumnas.Properties.Resources.Maximizar14X11;
            this.Button_MaxRest.Location = new System.Drawing.Point(989, 0);
            this.Button_MaxRest.Name = "Button_MaxRest";
            this.Button_MaxRest.Size = new System.Drawing.Size(40, 26);
            this.Button_MaxRest.TabIndex = 28;
            this.Button_MaxRest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_MaxRest.UseVisualStyleBackColor = true;
            this.Button_MaxRest.Click += new System.EventHandler(this.Button_MaxRest_Click);
            // 
            // Button_Cerrar
            // 
            this.Button_Cerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_Cerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.Button_Cerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(101)))), ((int)(((byte)(113)))));
            this.Button_Cerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.Button_Cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Cerrar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Cerrar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_Cerrar.Image = global::DisenoColumnas.Properties.Resources.x16Blanca;
            this.Button_Cerrar.Location = new System.Drawing.Point(1029, 0);
            this.Button_Cerrar.Name = "Button_Cerrar";
            this.Button_Cerrar.Size = new System.Drawing.Size(40, 26);
            this.Button_Cerrar.TabIndex = 26;
            this.Button_Cerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_Cerrar.UseVisualStyleBackColor = true;
            this.Button_Cerrar.Click += new System.EventHandler(this.Close_Click);
            // 
            // variablesDeEntradaToolStripMenuItem
            // 
            this.variablesDeEntradaToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.VarEntrada;
            this.variablesDeEntradaToolStripMenuItem.Name = "variablesDeEntradaToolStripMenuItem";
            this.variablesDeEntradaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.variablesDeEntradaToolStripMenuItem.Text = "Variables de Entrada";
            this.variablesDeEntradaToolStripMenuItem.Click += new System.EventHandler(this.VariablesDeEntradaToolStripMenuItem_Click);
            // 
            // columnasIgualesToolStripMenuItem
            // 
            this.columnasIgualesToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.SameCol1;
            this.columnasIgualesToolStripMenuItem.Name = "columnasIgualesToolStripMenuItem";
            this.columnasIgualesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.columnasIgualesToolStripMenuItem.Text = "Columnas Iguales";
            this.columnasIgualesToolStripMenuItem.Click += new System.EventHandler(this.ColumnasIgualesToolStripMenuItem_Click);
            // 
            // editarSeccionesPredeterminadasToolStripMenuItem
            // 
            this.editarSeccionesPredeterminadasToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.bloquear;
            this.editarSeccionesPredeterminadasToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.editarSeccionesPredeterminadasToolStripMenuItem.Name = "editarSeccionesPredeterminadasToolStripMenuItem";
            this.editarSeccionesPredeterminadasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editarSeccionesPredeterminadasToolStripMenuItem.Text = "Editar Secciones";
            this.editarSeccionesPredeterminadasToolStripMenuItem.Click += new System.EventHandler(this.EditarSeccionesPredeterminadasToolStripMenuItem_Click);
            // 
            // plantaDeColumnasToolStripMenuItem
            // 
            this.plantaDeColumnasToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.anteproyecto;
            this.plantaDeColumnasToolStripMenuItem.Name = "plantaDeColumnasToolStripMenuItem";
            this.plantaDeColumnasToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.plantaDeColumnasToolStripMenuItem.Text = "Planta de Columnas";
            this.plantaDeColumnasToolStripMenuItem.Click += new System.EventHandler(this.PlantaDeColumnasToolStripMenuItem_Click);
            // 
            // infromaciónDeColumnasToolStripMenuItem
            // 
            this.infromaciónDeColumnasToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.info;
            this.infromaciónDeColumnasToolStripMenuItem.Name = "infromaciónDeColumnasToolStripMenuItem";
            this.infromaciónDeColumnasToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.infromaciónDeColumnasToolStripMenuItem.Text = "Infromación de Columnas";
            this.infromaciónDeColumnasToolStripMenuItem.Click += new System.EventHandler(this.InfromaciónDeColumnasToolStripMenuItem_Click);
            // 
            // dibujoSecciónToolStripMenuItem
            // 
            this.dibujoSecciónToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.columna;
            this.dibujoSecciónToolStripMenuItem.Name = "dibujoSecciónToolStripMenuItem";
            this.dibujoSecciónToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.dibujoSecciónToolStripMenuItem.Text = "Dibujo Sección";
            this.dibujoSecciónToolStripMenuItem.Click += new System.EventHandler(this.dibujoSecciónToolStripMenuItem_Click);
            // 
            // estribosToolStripMenuItem
            // 
            this.estribosToolStripMenuItem.Image = global::DisenoColumnas.Properties.Resources.image;
            this.estribosToolStripMenuItem.Name = "estribosToolStripMenuItem";
            this.estribosToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.estribosToolStripMenuItem.Text = "Cuantía Volumétrica";
            this.estribosToolStripMenuItem.Click += new System.EventHandler(this.EstribosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(112)))), ((int)(((byte)(113)))));
            this.ClientSize = new System.Drawing.Size(1069, 733);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Disenar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_Agregar);
            this.Controls.Add(this.La_Column);
            this.Controls.Add(this.LColumna);
            this.Controls.Add(this.SaveAs_B);
            this.Controls.Add(this.Save_B);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cuantia_Vol_Button);
            this.Controls.Add(this.PanelContenedor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button Button_Minimize;
        private System.Windows.Forms.Button Button_Cerrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Button_MaxRest;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel PanelContenedor;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripMenuItem plantaDeColumnasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infromaciónDeColumnasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variablesDeEntradaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button Cuantia_Vol_Button;
        private System.Windows.Forms.Button SaveAs_B;
        private System.Windows.Forms.Button Save_B;
        internal System.Windows.Forms.ComboBox LColumna;
        internal System.Windows.Forms.Label La_Column;
        private System.Windows.Forms.ToolStripMenuItem dibujoSecciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estribosToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_Agregar;
        private System.Windows.Forms.ToolStripMenuItem agregarAlzadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem columnasIgualesToolStripMenuItem;
        private System.Windows.Forms.Button Disenar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem editarSeccionesPredeterminadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
    }
}
