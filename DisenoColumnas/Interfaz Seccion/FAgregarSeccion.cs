﻿using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using DisenoColumnas.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FAgregarSeccion : Form
    {
        public GDE gde { get; set; }
        public ListBox Lista_Secciones { get; set; }

        public FAgregarSeccion(GDE pgde, ListBox pLista_secciones)
        {
            gde = pgde;
            Lista_Secciones = pLista_secciones;
            InitializeComponent();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Label6_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Button_Cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FAgregarSeccion_Load(object sender, EventArgs e)
        {
            cbSeccion.Items.Clear();
            cbSeccion.Items.AddRange(new string[] { TipodeSeccion.Rectangular.ToString(), TipodeSeccion.Circle.ToString(), TipodeSeccion.L.ToString(), TipodeSeccion.Tee.ToString() });
            cbSeccion.Text = cbSeccion.Items[0].ToString();
        }

        private void cbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSeccion.Text == TipodeSeccion.Rectangular.ToString())
            {
                LAncho.Text = "B (cm) :";
                LAncho.Update();
                tbAlto.Enabled = true;
                tbTf.Enabled = false;
                tbTw.Enabled = false;

                tbTf.Text = "0";
                tbTw.Text = "0";
            }

            if (cbSeccion.Text == TipodeSeccion.Tee.ToString() | cbSeccion.Text == TipodeSeccion.L.ToString())
            {
                LAncho.Text = "B (cm) :";
                LAncho.Update();
                tbAlto.Enabled = true;
                tbTf.Enabled = true;
                tbTw.Enabled = true;
            }

            if (cbSeccion.Text == TipodeSeccion.Circle.ToString())
            {
                LAncho.Text = "D (cm) :";
                LAncho.Update();
                tbAlto.Enabled = false;
                tbTf.Enabled = false;
                tbTw.Enabled = false;

                tbAlto.Text = "0";
                tbTf.Text = "0";
                tbTw.Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Tipo_seccion;
            float b, h, tw, tf, r;

            Tipo_seccion = cbSeccion.Text;
            b = Convert.ToSingle(tbAncho.Text);
            h = Convert.ToSingle(tbAlto.Text);
            tw = Convert.ToSingle(tbTw.Text);
            tf = Convert.ToSingle(tbTf.Text);
            r = Convert.ToSingle(tb_r.Text);

            if (Tipo_seccion != TipodeSeccion.L.ToString() & Tipo_seccion != TipodeSeccion.Tee.ToString())
            {
                Crear_Seccion(Tipo_seccion, b, h, tw, tf, r);
                Actualizar_Lista();
                Close();
            }
            else
            {
                if (b <= h)
                {
                    Crear_Seccion(Tipo_seccion, b, h, tw, tf, r);
                    Actualizar_Lista();
                    Close();
                }
                else
                    MessageBox.Show("H debe ser mayor que B", "efe Prima Ce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Crear_Seccion(string Tipo_Seccion, float b, float h, float tw, float tf, float r)
        {
            ISeccion N_Seccion = null;
            string Nombre_Seccion = "";
            GDE gde = GDE.DMO;
            List<float[]> Vertices_Temp = new List<float[]>();
            List<float[]> Vertices = new List<float[]>();
            double Xc, Yc;
            double Numerador, Denominador;

            MAT_CONCRETE material = new MAT_CONCRETE()
            {
                Name = "H" + tbFc.Text,
                FC = Convert.ToSingle(tbFc.Text)
            };

            if (Radio_Dmo.Checked)
            {
                gde = GDE.DMO;
            }

            if (Radio_Des.Checked)
            {
                gde = GDE.DES;
            }

            if (Tipo_Seccion == TipodeSeccion.Rectangular.ToString())
            {
                Nombre_Seccion = $"C{b}X{h}{material.Name}";
                N_Seccion = new CRectangulo(Nombre_Seccion, b / 100, h / 100, material, TipodeSeccion.Rectangular, null);
                N_Seccion.Calc_vol_inex(r / 100, 4220, gde);
                N_Seccion.Refuerzo_Base(r);
            }

            if (Tipo_Seccion == TipodeSeccion.Circle.ToString())
            {
                Nombre_Seccion = $"C{b}{material.Name}";
                N_Seccion = new CCirculo(Nombre_Seccion, b / 200, new double[] { 0, 0 }, material, TipodeSeccion.Circle, null);
                N_Seccion.Calc_vol_inex(r / 100, 4220, gde);
                N_Seccion.Refuerzo_Base(r);
            }

            if (Tipo_Seccion == TipodeSeccion.Tee.ToString() | Tipo_Seccion == TipodeSeccion.L.ToString())
            {
                Nombre_Seccion = $"C{b}X{h}X{tw}X{tf}{Tipo_Seccion}{material.Name}";

                if (Tipo_Seccion == TipodeSeccion.Tee.ToString())
                {
                    Vertices_Temp.Add(new float[] { 0, h / 200 });
                    Vertices_Temp.Add(new float[] { 0, (h - tw) / 200 });
                    Vertices_Temp.Add(new float[] { (b - tf) / 400, (h - tw) / 200 });
                    Vertices_Temp.Add(new float[] { (b - tf) / 400, 0 });
                    Vertices_Temp.Add(new float[] { (b + tf) / 400, 0 });
                    Vertices_Temp.Add(new float[] { (b + tf) / 400, (h - tw) / 200 });
                    Vertices_Temp.Add(new float[] { b / 200, (h - tw) / 200 });
                    Vertices_Temp.Add(new float[] { b / 200, h / 200 });

                    Numerador = ((b / 2) * b * tw) + ((b / 2) * (tf * (h - tw)));
                    Denominador = (b * tw) + (tf * (h - tw));

                    Xc = b / 200;

                    double y1 = (h - tw) * (h - tw) * tf / 2;
                    double y2 = (h - (tw / 2)) * b * tw;

                    Numerador = y1 + y2;
                    Denominador = (b * tw) + (tf * (h - tw));

                    Yc = h / 200;

                    for (int i = 0; i < Vertices_Temp.Count; i++)
                    {
                        var Aux = B_Operaciones_Matricialesl.Operaciones.Traslacion(Vertices_Temp[i][0] - Xc, Vertices_Temp[i][1] - Yc, Vertices_Temp[i][0], Vertices_Temp[i][1]);
                        Vertices.Add(new float[] { (float)Aux[0], (float)Aux[1] });
                    }

                    N_Seccion = new CSD(Nombre_Seccion, b / 100, h / 100, tw / 100, tf / 100, material, TipodeSeccion.Tee, Vertices);
                }

                if (Tipo_Seccion == TipodeSeccion.L.ToString())
                {
                    Vertices_Temp.Add(new float[] { 0, 0 });
                    Vertices_Temp.Add(new float[] { b / 200, 0 });
                    Vertices_Temp.Add(new float[] { b / 200, tw / 200 });
                    Vertices_Temp.Add(new float[] { tf / 200, tw / 200 });
                    Vertices_Temp.Add(new float[] { tf / 200, h / 200 });
                    Vertices_Temp.Add(new float[] { 0, h / 200 });

                    Numerador = ((b / 2) * b * tw) + ((tf / 2) * (tf * (h - tw)));
                    Denominador = (b * tw) + (tf * (h - tw));

                    Xc = b / 200;

                    Numerador = ((tw / 2) * b * tw) + ((h + tf / 2) * (tf * (h - tw)));
                    Denominador = (b * tw) + (tf * (h - tw));

                    Yc = h / 200;

                    for (int i = 0; i < Vertices_Temp.Count; i++)
                    {
                        var Aux = B_Operaciones_Matricialesl.Operaciones.Traslacion(Vertices_Temp[i][0] - Xc, Vertices_Temp[i][1] - Yc, Vertices_Temp[i][0], Vertices_Temp[i][1]);
                        Vertices.Add(new float[] { (float)Aux[0], (float)Aux[1] });
                    }

                    N_Seccion = new CSD(Nombre_Seccion, b / 100, h / 100, tw / 100, tf / 100, material, TipodeSeccion.L, Vertices);
                }

                N_Seccion.Calc_vol_inex(r / 100, 4220, gde);
                N_Seccion.Refuerzo_Base(r);
            }

            if (N_Seccion != null)
            {
                if (Radio_Dmo.Checked)
                {
                    if (Form1.secciones_predef.Secciones_DMO.Exists(x => x.Equals(N_Seccion)) == false)
                    {
                        Form1.secciones_predef.Secciones_DMO.Add(N_Seccion);
                    }
                }
                if (Radio_Des.Checked)
                {
                    if (Form1.secciones_predef.Secciones_DES.Exists(x => x.Equals(N_Seccion)) == false)
                    {
                        Form1.secciones_predef.Secciones_DES.Add(N_Seccion);
                    }
                }
            }
        }

        private void Actualizar_Lista()
        {
            ISeccion[] Secciones = { };
            string Fc_secciones = "H" + tbFc.Text;

            if (gde == GDE.DMO)
            {
                Secciones = Form1.secciones_predef.Secciones_DMO.FindAll(x => x.Material.Name == Fc_secciones).ToArray();
            }
            else
            {
                Secciones = Form1.secciones_predef.Secciones_DES.FindAll(x => x.Material.Name == Fc_secciones).ToArray();
            }

            Lista_Secciones.Items.Clear();
            Lista_Secciones.Items.AddRange(Secciones);
            Lista_Secciones.SelectedItem = Lista_Secciones.Items[0];
        }
    }
}