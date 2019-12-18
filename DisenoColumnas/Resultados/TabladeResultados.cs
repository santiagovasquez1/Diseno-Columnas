using ClosedXML.Excel;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Resultados
{
    public partial class TabladeResultados : Form
    {
        public static DataSet DataVirtual;
        private int NoTabla = 0;

        public TabladeResultados()
        {
            InitializeComponent();
        }

        private void TabladeResultados_Load(object sender, EventArgs e)
        {
            CrearResultados();
        }

        private void CreateDataGrid()
        {
            Resultados.VirtualMode = true;
            Resultados.DataSource = DataVirtual.Tables[NoTabla];

            EstiloDatGridView(Resultados);
        }

        private void CrearResultados()
        {
            toolStripTextBox1.Text = (NoTabla + 1).ToString();
            toolStripLabel1.Text = "de {" + DataVirtual.Tables.Count.ToString() + "}";
            Text = "DMC - Resultados - " + DataVirtual.Tables[NoTabla].TableName;
            CreateDataGrid();
        }

        private void EstiloDatGridView(DataGridView dataGridView)
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Bold);

            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.ReadOnly = true;
                column.HeaderCell.Style = StyleC;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            NoTabla += 1;
            if (NoTabla == DataVirtual.Tables.Count)
            {
                NoTabla = 0;
            }
            CrearResultados();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            NoTabla -= 1;
            if (NoTabla < 0)
            {
                NoTabla = DataVirtual.Tables.Count - 1;
            }
            CrearResultados();
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            NoTabla = DataVirtual.Tables.Count - 1;
            CrearResultados();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            NoTabla = 0;
            CrearResultados();
        }

        private void ToolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            int NoT;

            if (Int32.TryParse(toolStripTextBox1.Text, out NoT))
            {
                if (NoT <= DataVirtual.Tables.Count && NoT > 0)
                {
                    NoTabla = NoT - 1;
                }
                else
                {
                    toolStripTextBox1.Text = (NoTabla + 1).ToString();
                }
            }
            else
            {
                toolStripTextBox1.Text = (NoTabla + 1).ToString();
            }
            CrearResultados();
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            CreateExcel();
        }

        private void CreateExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Title = "Exportar Resultados", Filter = "Resultados |*.xlsx" };
            saveFileDialog.ShowDialog();
            string Ruta = saveFileDialog.FileName;

            if (Ruta != "")
            {
                using (var workbook = new XLWorkbook())
                {
                    foreach (DataTable dataTable in DataVirtual.Tables)
                    {
                        var worksheet = workbook.Worksheets.Add(dataTable.TableName);
                        worksheet.Cell(1, 1).InsertTable(dataTable);
                    }

                    workbook.SaveAs(Ruta);
                }
                Process Proc = new Process();
                Proc.StartInfo.FileName = Ruta;
                Proc.Start();
            }
        }
    }
}