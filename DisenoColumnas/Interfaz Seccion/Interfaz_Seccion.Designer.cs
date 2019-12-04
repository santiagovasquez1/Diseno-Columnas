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
            this.Grafica = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.gbSecciones.SuspendLayout();
            this.cmEditar_Ref.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 506);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbPisos);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(735, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 438);
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
            this.lbPisos.Location = new System.Drawing.Point(6, 24);
            this.lbPisos.Name = "lbPisos";
            this.lbPisos.Size = new System.Drawing.Size(154, 409);
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
            this.gbSecciones.Location = new System.Drawing.Point(735, 8);
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
            this.cmEditar_Ref.Size = new System.Drawing.Size(181, 70);
            // 
            // editarRefuerzoToolStripMenuItem
            // 
            this.editarRefuerzoToolStripMenuItem.Name = "editarRefuerzoToolStripMenuItem";
            this.editarRefuerzoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editarRefuerzoToolStripMenuItem.Text = "Editar refuerzo";
            this.editarRefuerzoToolStripMenuItem.Click += new System.EventHandler(this.editarRefuerzoToolStripMenuItem_Click);
            // 
            // eliminarRefuerzoToolStripMenuItem
            // 
            this.eliminarRefuerzoToolStripMenuItem.Name = "eliminarRefuerzoToolStripMenuItem";
            this.eliminarRefuerzoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eliminarRefuerzoToolStripMenuItem.Text = "Eliminar refuerzo";
            this.eliminarRefuerzoToolStripMenuItem.Click += new System.EventHandler(this.eliminarRefuerzoToolStripMenuItem_Click);
            // 
            // Grafica
            // 
            this.Grafica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grafica.BackColor = System.Drawing.Color.White;
            this.Grafica.Location = new System.Drawing.Point(27, 12);
            this.Grafica.Name = "Grafica";
            this.Grafica.Size = new System.Drawing.Size(702, 487);
            this.Grafica.TabIndex = 1;
            this.Grafica.TabStop = false;
            this.Grafica.Paint += new System.Windows.Forms.PaintEventHandler(this.Grafica_Paint);
            this.Grafica.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseDown);
            this.Grafica.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseMove);
            // 
            // FInterfaz_Seccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(904, 528);
            this.Controls.Add(this.gbSecciones);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Grafica);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FInterfaz_Seccion";
            this.Text = "Sección";
            this.Load += new System.EventHandler(this.Interfaz_Seccion_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FInterfaz_Seccion_Paint);
            this.groupBox1.ResumeLayout(false);
            this.gbSecciones.ResumeLayout(false);
            this.cmEditar_Ref.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.PictureBox Grafica;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbPisos;
        private System.Windows.Forms.GroupBox gbSecciones;
        private System.Windows.Forms.ComboBox cbSecciones;
        private System.Windows.Forms.ContextMenuStrip cmEditar_Ref;
        private System.Windows.Forms.ToolStripMenuItem eliminarRefuerzoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarRefuerzoToolStripMenuItem;
    }
}