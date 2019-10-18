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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BSeleccionar_columna = new System.Windows.Forms.ToolStripButton();
            this.BSeleccion = new System.Windows.Forms.ToolStripButton();
            this.Mover = new System.Windows.Forms.ToolStripButton();
            this.BAcercar = new System.Windows.Forms.ToolStripButton();
            this.BAlejar = new System.Windows.Forms.ToolStripButton();
            this.BReestablecer = new System.Windows.Forms.ToolStripButton();
            this.Grafica = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BSeleccionar_columna,
            this.BSeleccion,
            this.Mover,
            this.BAcercar,
            this.BAlejar,
            this.BReestablecer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 528);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BSeleccionar_columna
            // 
            this.BSeleccionar_columna.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BSeleccionar_columna.Image = global::DisenoColumnas.Properties.Resources.anadir;
            this.BSeleccionar_columna.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BSeleccionar_columna.Name = "BSeleccionar_columna";
            this.BSeleccionar_columna.Size = new System.Drawing.Size(21, 20);
            this.BSeleccionar_columna.Text = "Selecciona la columna para el dibujo de la seccion";
            this.BSeleccionar_columna.Click += new System.EventHandler(this.BSeleccionar_columna_Click);
            // 
            // BSeleccion
            // 
            this.BSeleccion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BSeleccion.Image = global::DisenoColumnas.Properties.Resources.cursor;
            this.BSeleccion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BSeleccion.Name = "BSeleccion";
            this.BSeleccion.Size = new System.Drawing.Size(21, 20);
            this.BSeleccion.Text = "toolStripButton1";
            this.BSeleccion.ToolTipText = "Seleccion";
            this.BSeleccion.Click += new System.EventHandler(this.BSeleccion_Click);
            // 
            // Mover
            // 
            this.Mover.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Mover.Image = global::DisenoColumnas.Properties.Resources.move;
            this.Mover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Mover.Name = "Mover";
            this.Mover.Size = new System.Drawing.Size(21, 20);
            this.Mover.Text = "toolStripButton1";
            this.Mover.ToolTipText = "Mover";
            this.Mover.Click += new System.EventHandler(this.Mover_Click);
            // 
            // BAcercar
            // 
            this.BAcercar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BAcercar.Image = global::DisenoColumnas.Properties.Resources.zoom_increasing_symbol;
            this.BAcercar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BAcercar.Name = "BAcercar";
            this.BAcercar.Size = new System.Drawing.Size(21, 20);
            this.BAcercar.Text = "Zoom +";
            this.BAcercar.Click += new System.EventHandler(this.BAcercar_Click);
            // 
            // BAlejar
            // 
            this.BAlejar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BAlejar.Image = global::DisenoColumnas.Properties.Resources.zoom_out;
            this.BAlejar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BAlejar.Name = "BAlejar";
            this.BAlejar.Size = new System.Drawing.Size(21, 20);
            this.BAlejar.Text = "Zoom -";
            // 
            // BReestablecer
            // 
            this.BReestablecer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BReestablecer.Image = global::DisenoColumnas.Properties.Resources.search;
            this.BReestablecer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BReestablecer.Name = "BReestablecer";
            this.BReestablecer.Size = new System.Drawing.Size(21, 20);
            this.BReestablecer.Text = "toolStripButton1";
            this.BReestablecer.ToolTipText = "Reestablecer";
            // 
            // Grafica
            // 
            this.Grafica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grafica.BackColor = System.Drawing.Color.White;
            this.Grafica.Location = new System.Drawing.Point(27, 12);
            this.Grafica.Name = "Grafica";
            this.Grafica.Size = new System.Drawing.Size(765, 487);
            this.Grafica.TabIndex = 1;
            this.Grafica.TabStop = false;
            this.Grafica.Click += new System.EventHandler(this.Grafica_Click);
            this.Grafica.Paint += new System.Windows.Forms.PaintEventHandler(this.Grafica_Paint);
            this.Grafica.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseMove);
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
            // FInterfaz_Seccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(796, 528);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Grafica);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FInterfaz_Seccion";
            this.Text = "Interfaz_Seccion";
            this.Load += new System.EventHandler(this.Interfaz_Seccion_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FInterfaz_Seccion_Paint);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.PictureBox Grafica;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BSeleccionar_columna;
        private System.Windows.Forms.ToolStripButton BSeleccion;
        private System.Windows.Forms.ToolStripButton Mover;
        private System.Windows.Forms.ToolStripButton BAcercar;
        private System.Windows.Forms.ToolStripButton BAlejar;
        private System.Windows.Forms.ToolStripButton BReestablecer;
        private System.Windows.Forms.Label label1;
    }
}