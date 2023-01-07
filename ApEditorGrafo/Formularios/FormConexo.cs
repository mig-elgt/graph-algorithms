using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApEditorGrafo.Formularios
{
    partial class FormConexo : Form
    {
        private CNodoVertice vO;
        private CNodoVertice vD;
        private List<CNodoVertice> listaVerticesAux;
        private CGrafo grafo;

        public FormConexo()
        {
            InitializeComponent();
        }

        public void setGrafo(CGrafo g)
        {
            grafo = g;
        }
        public void setListaVert(List<CNodoVertice> lista)
        {
            listaVerticesAux = lista;
        }

        private void FormConexo_Load(object sender, EventArgs e)
        {
            foreach (CNodoVertice nV in grafo.getListVert())
            {
                listVertices.Items.Add("Vertice " + nV.getNombre());
            }
        }

        private void listVertices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vertA.Checked == true)
            {
                vO = grafo.getListVert()[listVertices.SelectedIndex];
                vA.Text = vO.getNombre();
            }
            else
            {
                vD = grafo.getListVert()[listVertices.SelectedIndex];
                vB.Text = vD.getNombre();
            }
         }

        private void btChecaConectividad_Click(object sender, EventArgs e)
        {
            if (grafo.ConectividadAB(vO, vD) == true)
                MessageBox.Show("Si hay conectividad entre los vertices", "Caminos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No hay conectividad entre los vertices","Caminos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            foreach (CNodoVertice nV in grafo.getListVert())
                nV.setVisita(false);
        }
    }
}