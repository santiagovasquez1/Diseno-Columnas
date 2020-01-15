namespace DisenoColumnas.Interfaz_Seccion
{
    partial class FAgregarRefMultiple
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CantRad = new System.Windows.Forms.RadioButton();
            this.SepRadio = new System.Windows.Forms.RadioButton();
            this.CantBarrasBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Yf = new System.Windows.Forms.TextBox();
            this.Xf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Se = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bCancelar = new System.Windows.Forms.Button();
            this.bAceptar = new System.Windows.Forms.Button();
            this.Yi = new System.Windows.Forms.TextBox();
            this.Xi = new System.Windows.Forms.TextBox();
            this.cbDiametros = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 331);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CantRad);
            this.groupBox1.Controls.Add(this.SepRadio);
            this.groupBox1.Controls.Add(this.CantBarrasBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Yf);
            this.groupBox1.Controls.Add(this.Xf);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Se);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bCancelar);
            this.groupBox1.Controls.Add(this.bAceptar);
            this.groupBox1.Controls.Add(this.Yi);
            this.groupBox1.Controls.Add(this.Xi);
            this.groupBox1.Controls.Add(this.cbDiametros);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 307);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Linea de Barras";
            this.groupBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // CantRad
            // 
            this.CantRad.AutoSize = true;
            this.CantRad.Location = new System.Drawing.Point(134, 22);
            this.CantRad.Name = "CantRad";
            this.CantRad.Size = new System.Drawing.Size(73, 19);
            this.CantRad.TabIndex = 25;
            this.CantRad.Text = "Cantidad";
            this.CantRad.UseVisualStyleBackColor = true;
            // 
            // SepRadio
            // 
            this.SepRadio.AutoSize = true;
            this.SepRadio.Location = new System.Drawing.Point(21, 22);
            this.SepRadio.Name = "SepRadio";
            this.SepRadio.Size = new System.Drawing.Size(84, 19);
            this.SepRadio.TabIndex = 24;
            this.SepRadio.Text = "Separación";
            this.SepRadio.UseVisualStyleBackColor = true;
            this.SepRadio.CheckedChanged += new System.EventHandler(this.SepRadio_CheckedChanged);
            // 
            // CantBarrasBox
            // 
            this.CantBarrasBox.Location = new System.Drawing.Point(149, 112);
            this.CantBarrasBox.Name = "CantBarrasBox";
            this.CantBarrasBox.Size = new System.Drawing.Size(84, 23);
            this.CantBarrasBox.TabIndex = 23;
            this.CantBarrasBox.Text = "0";
            this.CantBarrasBox.TextChanged += new System.EventHandler(this.CantBarrasBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "Cantidad:";
            // 
            // Yf
            // 
            this.Yf.Location = new System.Drawing.Point(149, 241);
            this.Yf.Name = "Yf";
            this.Yf.Size = new System.Drawing.Size(84, 23);
            this.Yf.TabIndex = 5;
            // 
            // Xf
            // 
            this.Xf.Location = new System.Drawing.Point(149, 209);
            this.Xf.Name = "Xf";
            this.Xf.Size = new System.Drawing.Size(84, 23);
            this.Xf.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Y Final (cm) : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "X Final (cm) : ";
            // 
            // Se
            // 
            this.Se.Location = new System.Drawing.Point(149, 83);
            this.Se.Name = "Se";
            this.Se.Size = new System.Drawing.Size(84, 23);
            this.Se.TabIndex = 1;
            this.Se.Text = "0";
            this.Se.TextChanged += new System.EventHandler(this.Se_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Separación [cm] :";
            // 
            // bCancelar
            // 
            this.bCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bCancelar.Location = new System.Drawing.Point(134, 282);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(72, 26);
            this.bCancelar.TabIndex = 7;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = true;
            this.bCancelar.Click += new System.EventHandler(this.BCancelar_Click);
            // 
            // bAceptar
            // 
            this.bAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAceptar.Location = new System.Drawing.Point(47, 282);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(81, 26);
            this.bAceptar.TabIndex = 6;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.BAceptar_Click);
            // 
            // Yi
            // 
            this.Yi.Location = new System.Drawing.Point(149, 176);
            this.Yi.Name = "Yi";
            this.Yi.Size = new System.Drawing.Size(84, 23);
            this.Yi.TabIndex = 3;
            // 
            // Xi
            // 
            this.Xi.Location = new System.Drawing.Point(149, 144);
            this.Xi.Name = "Xi";
            this.Xi.Size = new System.Drawing.Size(84, 23);
            this.Xi.TabIndex = 2;
            // 
            // cbDiametros
            // 
            this.cbDiametros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiametros.FormattingEnabled = true;
            this.cbDiametros.Items.AddRange(new object[] {
            "#4",
            "#5",
            "#6",
            "#7",
            "#8"});
            this.cbDiametros.Location = new System.Drawing.Point(149, 52);
            this.cbDiametros.Name = "cbDiametros";
            this.cbDiametros.Size = new System.Drawing.Size(84, 23);
            this.cbDiametros.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Y Inicial (cm) : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "X Inicial (cm) : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Diámetro de la Barra:";
            // 
            // FAgregarRefMultiple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 331);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(289, 331);
            this.Name = "FAgregarRefMultiple";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FAgregarRefMultiple";
            this.Load += new System.EventHandler(this.FAgregarRefMultiple_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bCancelar;
        private System.Windows.Forms.Button bAceptar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox Yi;
        internal System.Windows.Forms.TextBox Xi;
        internal System.Windows.Forms.TextBox Yf;
        internal System.Windows.Forms.TextBox Xf;
        internal System.Windows.Forms.TextBox Se;
        internal System.Windows.Forms.ComboBox cbDiametros;
        internal System.Windows.Forms.TextBox CantBarrasBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton CantRad;
        private System.Windows.Forms.RadioButton SepRadio;
    }
}