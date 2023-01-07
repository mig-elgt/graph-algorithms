using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApEditorGrafo.Formularios
{
    public partial class PropiedadesGrafo : Form
    {
        public int tipoDeGrafo;
        public int[][] matriAdy;
        public string[] nameVert;
        
        public PropiedadesGrafo()
        {
            InitializeComponent();
        }

        private void PropiedadesGrafo_Load(object sender, EventArgs e)
        {
            this.Location = new Point(500, 150);

            switch (tipoDeGrafo)
            {
                case 1:
                    etTipoGrafo.Text = "Grafo";
                break;
                case 2:
                    etTipoGrafo.Text = "No Dirigido";
                break;
                case 3:
                    etTipoGrafo.Text = "Dirigido";
                break;
            }
            
            int acum = 0;
            for(int i = 0; i < matriAdy.Length; i++)
                for(int j = 0; j < matriAdy.Length; j++)
                    acum += matriAdy[i][j];

            if (tipoDeGrafo == 2)
                acum /= 2;

            etNoAristas.Text = acum.ToString();

            DataGridViewTextBoxColumn col;
             
            for (int i = 0; i < matriAdy.Length; i++)
            {
                col = new DataGridViewTextBoxColumn();
                col.HeaderText = nameVert[i];     
                col.Width = 25;
                col.ReadOnly = true;
                col.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tablaBinaria.Columns.Add(col);
            }

            for (int i = 0; i < matriAdy.Length; i++)
            {
                tablaBinaria.Rows.Add();
                tablaBinaria.Rows[i].HeaderCell.Value = nameVert[i];

                for (int j = 0; j < matriAdy.Length; j++)
                {
                    if (matriAdy[i][j] != 0)
                    {
                        tablaBinaria.Rows[i].Cells[j].Style.Font = new Font("Arial", 12, FontStyle.Bold);
                        tablaBinaria.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                    }
                    tablaBinaria.Rows[i].Cells[j].Value = matriAdy[i][j].ToString();
                }
            }
        }
    }
}