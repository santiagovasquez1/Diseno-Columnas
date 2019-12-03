namespace DisenoColumnas.Diseño
{
    partial class AgregarRefuerzoBase
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
            this.button1 = new System.Windows.Forms.Button();
            this.BarraPersonalizada = new System.Windows.Forms.Panel();
            this.Label_BarraProgreso = new System.Windows.Forms.Label();
            this.BarraPersonalizada2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.TP2 = new System.Windows.Forms.RadioButton();
            this.TP1 = new System.Windows.Forms.RadioButton();
            this.cb_Aceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NoBarra = new System.Windows.Forms.ComboBox();
            this.CantBarras = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.BarraPersonalizada.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 190);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.BarraPersonalizada);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TP2);
            this.groupBox1.Controls.Add(this.TP1);
            this.groupBox1.Controls.Add(this.cb_Aceptar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.NoBarra);
            this.groupBox1.Controls.Add(this.CantBarras);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 177);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Refuerzo Base";
            this.groupBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GroupBox1_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(196, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 30;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BarraPersonalizada
            // 
            this.BarraPersonalizada.BackColor = System.Drawing.Color.Transparent;
            this.BarraPersonalizada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarraPersonalizada.Controls.Add(this.Label_BarraProgreso);
            this.BarraPersonalizada.Controls.Add(this.BarraPersonalizada2);
            this.BarraPersonalizada.Location = new System.Drawing.Point(6, 157);
            this.BarraPersonalizada.Name = "BarraPersonalizada";
            this.BarraPersonalizada.Size = new System.Drawing.Size(347, 14);
            this.BarraPersonalizada.TabIndex = 29;
            this.BarraPersonalizada.Visible = false;
            // 
            // Label_BarraProgreso
            // 
            this.Label_BarraProgreso.AutoSize = true;
            this.Label_BarraProgreso.BackColor = System.Drawing.Color.Transparent;
            this.Label_BarraProgreso.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_BarraProgreso.ForeColor = System.Drawing.Color.Black;
            this.Label_BarraProgreso.Location = new System.Drawing.Point(167, -1);
            this.Label_BarraProgreso.Name = "Label_BarraProgreso";
            this.Label_BarraProgreso.Size = new System.Drawing.Size(21, 13);
            this.Label_BarraProgreso.TabIndex = 26;
            this.Label_BarraProgreso.Text = "0%";
            this.Label_BarraProgreso.Visible = false;
            // 
            // BarraPersonalizada2
            // 
            this.BarraPersonalizada2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(150)))), ((int)(((byte)(21)))));
            this.BarraPersonalizada2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarraPersonalizada2.Location = new System.Drawing.Point(0, 0);
            this.BarraPersonalizada2.Name = "BarraPersonalizada2";
            this.BarraPersonalizada2.Size = new System.Drawing.Size(0, 12);
            this.BarraPersonalizada2.TabIndex = 24;
            this.BarraPersonalizada2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(54, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Tipo:";
            this.label3.Visible = false;
            // 
            // TP2
            // 
            this.TP2.AutoSize = true;
            this.TP2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TP2.Location = new System.Drawing.Point(241, 29);
            this.TP2.Name = "TP2";
            this.TP2.Size = new System.Drawing.Size(59, 19);
            this.TP2.TabIndex = 27;
            this.TP2.TabStop = true;
            this.TP2.Text = "Tipo 2";
            this.TP2.UseVisualStyleBackColor = true;
            this.TP2.Visible = false;
            // 
            // TP1
            // 
            this.TP1.AutoSize = true;
            this.TP1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TP1.Location = new System.Drawing.Point(147, 29);
            this.TP1.Name = "TP1";
            this.TP1.Size = new System.Drawing.Size(59, 19);
            this.TP1.TabIndex = 26;
            this.TP1.TabStop = true;
            this.TP1.Text = "Tipo 1";
            this.TP1.UseVisualStyleBackColor = true;
            this.TP1.Visible = false;
            // 
            // cb_Aceptar
            // 
            this.cb_Aceptar.BackColor = System.Drawing.Color.White;
            this.cb_Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_Aceptar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Aceptar.Location = new System.Drawing.Point(95, 123);
            this.cb_Aceptar.Name = "cb_Aceptar";
            this.cb_Aceptar.Size = new System.Drawing.Size(86, 26);
            this.cb_Aceptar.TabIndex = 25;
            this.cb_Aceptar.Text = "Aceptar";
            this.cb_Aceptar.UseVisualStyleBackColor = false;
            this.cb_Aceptar.Click += new System.EventHandler(this.Cb_Aceptar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "Cantidad de Barras:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(75, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 23;
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
            this.NoBarra.Location = new System.Drawing.Point(196, 83);
            this.NoBarra.Name = "NoBarra";
            this.NoBarra.Size = new System.Drawing.Size(103, 23);
            this.NoBarra.TabIndex = 0;
            // 
            // CantBarras
            // 
            this.CantBarras.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CantBarras.Location = new System.Drawing.Point(196, 54);
            this.CantBarras.Name = "CantBarras";
            this.CantBarras.Size = new System.Drawing.Size(103, 23);
            this.CantBarras.TabIndex = 22;
            // 
            // AgregarRefuerzoBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 190);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgregarRefuerzoBase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AgregarRefuerzoBase";
            this.Load += new System.EventHandler(this.AgregarRefuerzoBase_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AgregarRefuerzoBase_MouseDown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BarraPersonalizada.ResumeLayout(false);
            this.BarraPersonalizada.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox CantBarras;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox NoBarra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button cb_Aceptar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton TP2;
        private System.Windows.Forms.RadioButton TP1;
        internal System.Windows.Forms.Panel BarraPersonalizada;
        internal System.Windows.Forms.Label Label_BarraProgreso;
        internal System.Windows.Forms.Panel BarraPersonalizada2;
        internal System.Windows.Forms.Button button1;
    }
}