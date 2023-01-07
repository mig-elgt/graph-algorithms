using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ApEditorGrafo.Clases;

namespace ApEditorGrafo
{
    public partial class FormMatrizCostos : Form
    {
        internal CGrafoDirigido gD;
        
        
        public FormMatrizCostos()
        {
            InitializeComponent();

        }

        private void FormMatrizCostos_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn col;
            List<CNodoVertice> listaVert;
            int[][] mAdy;

            listaVert = gD.getListVert();

            //Creacion de columnas
            foreach (CNodoVertice nV in gD.getListVert())
            {
                col = new DataGridViewTextBoxColumn();
                col.HeaderText = nV.getNombre();
                col.Width = 45;
                col.ReadOnly = false;
                col.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                matrizDeCostos.Columns.Add(col);
            }

            //Creacion de los renglones
            for (int i = 0; i < listaVert.Count; i++)
            {
                matrizDeCostos.Rows.Add();
                matrizDeCostos.Rows[i].HeaderCell.Value = listaVert[i].getNombre();
            }

            gD.crearMatrizCostos();
            mAdy = gD.generaMatrizAdyacencia();
            //Inicializacion de la tabla
            for (int i = 0; i < listaVert.Count; i++)
                for (int j = 0; j < listaVert.Count; j++)
                {
                    matrizDeCostos.Rows[i].Cells[j].ReadOnly = true;
                    if (mAdy[i][j] > 0)
                    {
                      //  matrizDeCostos.Rows[i].Cells[j].Style.Font = new Font("Arial", 12, FontStyle.Bold);
                        matrizDeCostos.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                        matrizDeCostos.Rows[i].Cells[j].ReadOnly = false;
                    }

                    matrizDeCostos.Rows[i].Cells[j].Value = gD.getAtCosto(i, j);
                }
        }
    }
}