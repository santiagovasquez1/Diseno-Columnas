namespace DisenoColumnas.Diseño
{
    partial class AgregarAlzado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarAlzado));
            this.D_Alzado = new System.Windows.Forms.DataGridView();
            this.story = new System.Windows.Forms.DataGridViewImageColumn();
            this.NameColum = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cortarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.D_Alzado)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // D_Alzado
            // 
            this.D_Alzado.AllowUserToAddRows = false;
            this.D_Alzado.AllowUserToDeleteRows = false;
            this.D_Alzado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D_Alzado.BackgroundColor = System.Drawing.Color.White;
            this.D_Alzado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D_Alzado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.story});
            this.D_Alzado.GridColor = System.Drawing.Color.DarkGray;
            this.D_Alzado.Location = new System.Drawing.Point(12, 30);
            this.D_Alzado.Name = "D_Alzado";
            this.D_Alzado.Size = new System.Drawing.Size(620, 441);
            this.D_Alzado.TabIndex = 1;
            this.D_Alzado.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.D_Alzado_CellClick);
            this.D_Alzado.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.D_Alzado_CellEndEdit);
            this.D_Alzado.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.D_Alzado_CellMouseClick);
            // 
            // story
            // 
            this.story.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.story.HeaderText = "Story";
            this.story.Name = "story";
            this.story.ReadOnly = true;
            this.story.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // NameColum
            // 
            this.NameColum.AutoSize = true;
            this.NameColum.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameColum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.NameColum.Location = new System.Drawing.Point(12, 9);
            this.NameColum.Name = "NameColum";
            this.NameColum.Size = new System.Drawing.Size(56, 14);
            this.NameColum.TabIndex = 2;
            this.NameColum.Text = "Columna: ";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copiarToolStripMenuItem,
            this.cortarToolStripMenuItem,
            this.pegarToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(152, 70);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.copiarToolStripMenuItem.Text = "Copiar";
            this.copiarToolStripMenuItem.Click += new System.EventHandler(this.CopiarToolStripMenuItem_Click);
            // 
            // cortarToolStripMenuItem
            // 
            this.cortarToolStripMenuItem.Name = "cortarToolStripMenuItem";
            this.cortarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cortarToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.cortarToolStripMenuItem.Text = "Cortar";
            this.cortarToolStripMenuItem.Click += new System.EventHandler(this.CortarToolStripMenuItem_Click);
            // 
            // pegarToolStripMenuItem
            // 
            this.pegarToolStripMenuItem.Name = "pegarToolStripMenuItem";
            this.pegarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pegarToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.pegarToolStripMenuItem.Text = "Pegar";
            this.pegarToolStripMenuItem.Click += new System.EventHandler(this.PegarToolStripMenuItem_Click);
            // 
            // AgregarAlzado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(644, 482);
            this.Controls.Add(this.NameColum);
            this.Controls.Add(this.D_Alzado);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AgregarAlzado";
            this.Text = "Agregar Alzado";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AgregarAlzado_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.D_Alzado)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private WeifenLuo.DataGridViewTextBoxColumnEx Column1;
        public System.Windows.Forms.Label NameColum;
        private System.Windows.Forms.DataGridViewImageColumn story;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cortarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pegarToolStripMenuItem;
        internal System.Windows.Forms.DataGridView D_Alzado;
    }
}