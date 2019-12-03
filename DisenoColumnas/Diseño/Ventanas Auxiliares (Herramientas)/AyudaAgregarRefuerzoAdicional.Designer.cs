namespace DisenoColumnas.Diseño.Ventanas_Auxiliares__Herramientas_
{
    partial class AyudaAgregarRefuerzoAdicional
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AyudaAgregarRefuerzoAdicional));
            this.DrawAyuda = new System.Windows.Forms.PictureBox();
            this.ConvecionesRefuerzoAdicional = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_Aceptar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NoBarra = new System.Windows.Forms.ComboBox();
            this.CantBarras = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AlzadoAagregar = new System.Windows.Forms.Label();
            this.PisoAagregar = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Nom = new System.Windows.Forms.Panel();
            this.TextNomPanel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DrawAyuda)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Nom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawAyuda
            // 
            this.DrawAyuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawAyuda.Location = new System.Drawing.Point(261, 81);
            this.DrawAyuda.Name = "DrawAyuda";
            this.DrawAyuda.Size = new System.Drawing.Size(263, 296);
            this.DrawAyuda.TabIndex = 0;
            this.DrawAyuda.TabStop = false;
            this.DrawAyuda.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            // 
            // ConvecionesRefuerzoAdicional
            // 
            this.ConvecionesRefuerzoAdicional.FormattingEnabled = true;
            this.ConvecionesRefuerzoAdicional.ItemHeight = 15;
            this.ConvecionesRefuerzoAdicional.Items.AddRange(new object[] {
            "4#5A",
            "+4#5A",
            "-4#5A",
            "-4#3",
            "4#5A-4#5"});
            this.ConvecionesRefuerzoAdicional.Location = new System.Drawing.Point(11, 20);
            this.ConvecionesRefuerzoAdicional.Name = "ConvecionesRefuerzoAdicional";
            this.ConvecionesRefuerzoAdicional.Size = new System.Drawing.Size(214, 109);
            this.ConvecionesRefuerzoAdicional.TabIndex = 1;
            this.ConvecionesRefuerzoAdicional.SelectedIndexChanged += new System.EventHandler(this.ConvecionesRefuerzoAdicional_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(131, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 30;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cb_Aceptar
            // 
            this.cb_Aceptar.BackColor = System.Drawing.Color.White;
            this.cb_Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_Aceptar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Aceptar.Location = new System.Drawing.Point(30, 328);
            this.cb_Aceptar.Name = "cb_Aceptar";
            this.cb_Aceptar.Size = new System.Drawing.Size(86, 26);
            this.cb_Aceptar.TabIndex = 25;
            this.cb_Aceptar.Text = "Agregar";
            this.cb_Aceptar.UseVisualStyleBackColor = false;
            this.cb_Aceptar.Click += new System.EventHandler(this.Cb_Aceptar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ConvecionesRefuerzoAdicional);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(10, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 137);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Convenciones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Cantidad de Barras:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(283, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "# Barra";
            // 
            // NoBarra
            // 
            this.NoBarra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NoBarra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NoBarra.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoBarra.FormattingEnabled = true;
            this.NoBarra.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8",
            "10"});
            this.NoBarra.Location = new System.Drawing.Point(343, 35);
            this.NoBarra.Name = "NoBarra";
            this.NoBarra.Size = new System.Drawing.Size(103, 23);
            this.NoBarra.TabIndex = 32;
            this.NoBarra.SelectedIndexChanged += new System.EventHandler(this.NoBarra_SelectedIndexChanged);
            // 
            // CantBarras
            // 
            this.CantBarras.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CantBarras.Location = new System.Drawing.Point(176, 36);
            this.CantBarras.Name = "CantBarras";
            this.CantBarras.Size = new System.Drawing.Size(78, 23);
            this.CantBarras.TabIndex = 33;
            this.CantBarras.TextChanged += new System.EventHandler(this.CantBarras_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AlzadoAagregar);
            this.groupBox1.Controls.Add(this.PisoAagregar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Nom);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DrawAyuda);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.NoBarra);
            this.groupBox1.Controls.Add(this.cb_Aceptar);
            this.groupBox1.Controls.Add(this.CantBarras);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 382);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nomenclatura de Refuerzo Adicional ";
            this.groupBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // AlzadoAagregar
            // 
            this.AlzadoAagregar.AutoSize = true;
            this.AlzadoAagregar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlzadoAagregar.Location = new System.Drawing.Point(71, 246);
            this.AlzadoAagregar.Name = "AlzadoAagregar";
            this.AlzadoAagregar.Size = new System.Drawing.Size(14, 15);
            this.AlzadoAagregar.TabIndex = 44;
            this.AlzadoAagregar.Text = "0";
            // 
            // PisoAagregar
            // 
            this.PisoAagregar.AutoSize = true;
            this.PisoAagregar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PisoAagregar.Location = new System.Drawing.Point(61, 221);
            this.PisoAagregar.Name = "PisoAagregar";
            this.PisoAagregar.Size = new System.Drawing.Size(37, 15);
            this.PisoAagregar.TabIndex = 43;
            this.PisoAagregar.Text = "xxxxx";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "Alzado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Piso: ";
            // 
            // Nom
            // 
            this.Nom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nom.Controls.Add(this.TextNomPanel);
            this.Nom.Location = new System.Drawing.Point(380, 383);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(299, 52);
            this.Nom.TabIndex = 39;
            this.Nom.Visible = false;
            // 
            // TextNomPanel
            // 
            this.TextNomPanel.AutoSize = true;
            this.TextNomPanel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextNomPanel.Location = new System.Drawing.Point(3, 5);
            this.TextNomPanel.Name = "TextNomPanel";
            this.TextNomPanel.Size = new System.Drawing.Size(298, 45);
            this.TextNomPanel.TabIndex = 36;
            this.TextNomPanel.Text = "Aporta acero en el Bottom del piso en el cual está \r\nposicionado (Nomenclatura re" +
    "comendada solo para \r\nel primer piso)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DisenoColumnas.Properties.Resources.info;
            this.pictureBox1.Location = new System.Drawing.Point(107, 270);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "Nomenclatura:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(131, 269);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 23);
            this.textBox1.TabIndex = 36;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 408);
            this.panel1.TabIndex = 33;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // AyudaAgregarRefuerzoAdicional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(561, 408);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AyudaAgregarRefuerzoAdicional";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Refuerzo Adicional";
            this.Load += new System.EventHandler(this.AyudaAgregarRefuerzoAdicional_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AyudaAgregarRefuerzoAdicional_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.DrawAyuda)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Nom.ResumeLayout(false);
            this.Nom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawAyuda;
        private System.Windows.Forms.ListBox ConvecionesRefuerzoAdicional;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button cb_Aceptar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox NoBarra;
        private System.Windows.Forms.TextBox CantBarras;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Nom;
        private System.Windows.Forms.Label TextNomPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label AlzadoAagregar;
        private System.Windows.Forms.Label PisoAagregar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}