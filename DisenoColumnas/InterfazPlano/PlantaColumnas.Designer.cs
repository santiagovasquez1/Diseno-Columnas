namespace DisenoColumnas.DefinirColumnas
{
    partial class PlantaColumnas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlantaColumnas));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Grafica = new System.Windows.Forms.PictureBox();
            this.Ayudas2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.allReadyColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostrarLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.tamañoDelTextoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).BeginInit();
            this.Ayudas2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::DisenoColumnas.Properties.Resources.Abajo;
            this.button2.Location = new System.Drawing.Point(3, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 23);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::DisenoColumnas.Properties.Resources.Arriba;
            this.button1.Location = new System.Drawing.Point(3, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 23);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Grafica
            // 
            this.Grafica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grafica.BackColor = System.Drawing.Color.White;
            this.Grafica.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Grafica.ContextMenuStrip = this.Ayudas2;
            this.Grafica.Cursor = System.Windows.Forms.Cursors.Default;
            this.Grafica.Location = new System.Drawing.Point(35, 12);
            this.Grafica.Name = "Grafica";
            this.Grafica.Size = new System.Drawing.Size(749, 504);
            this.Grafica.TabIndex = 0;
            this.Grafica.TabStop = false;
            this.Grafica.Paint += new System.Windows.Forms.PaintEventHandler(this.Grafica_Paint);
            this.Grafica.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseDoubleClick);
            this.Grafica.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseDown);
            this.Grafica.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grafica_MouseMove);
            // 
            // Ayudas2
            // 
            this.Ayudas2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allReadyColumnsToolStripMenuItem,
            this.mostrarLabels,
            this.tamañoDelTextoToolStripMenuItem});
            this.Ayudas2.Name = "Ayudas2";
            this.Ayudas2.Size = new System.Drawing.Size(181, 92);
            // 
            // allReadyColumnsToolStripMenuItem
            // 
            this.allReadyColumnsToolStripMenuItem.CheckOnClick = true;
            this.allReadyColumnsToolStripMenuItem.Name = "allReadyColumnsToolStripMenuItem";
            this.allReadyColumnsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.allReadyColumnsToolStripMenuItem.Text = "All Ready Columns";
            this.allReadyColumnsToolStripMenuItem.Click += new System.EventHandler(this.AllReadyColumnsToolStripMenuItem_Click);
            // 
            // mostrarLabels
            // 
            this.mostrarLabels.CheckOnClick = true;
            this.mostrarLabels.Name = "mostrarLabels";
            this.mostrarLabels.Size = new System.Drawing.Size(180, 22);
            this.mostrarLabels.Text = "Mostrar Labels";
            this.mostrarLabels.CheckedChanged += new System.EventHandler(this.MostrarLabels_CheckedChanged);
            // 
            // tamañoDelTextoToolStripMenuItem
            // 
            this.tamañoDelTextoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.tamañoDelTextoToolStripMenuItem.Name = "tamañoDelTextoToolStripMenuItem";
            this.tamañoDelTextoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tamañoDelTextoToolStripMenuItem.Text = "Tamaño del Texto";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.TextChanged += new System.EventHandler(this.toolStripComboBox1_TextChanged);
            // 
            // PlantaColumnas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(796, 528);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Grafica);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PlantaColumnas";
            this.Text = "Planta de Columnas";
            this.Load += new System.EventHandler(this.PlantaColumnas_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PlantaColumnas_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlantaColumnas_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PlantaColumnas_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.Grafica)).EndInit();
            this.Ayudas2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox Grafica;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip Ayudas2;
        private System.Windows.Forms.ToolStripMenuItem allReadyColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostrarLabels;
        private System.Windows.Forms.ToolStripMenuItem tamañoDelTextoToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripComboBox1;
    }
}