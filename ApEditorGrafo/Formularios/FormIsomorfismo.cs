using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ApEditorGrafo.Formularios;

namespace ApEditorGrafo.Formularios
{
    partial class FormIsomorfismo : Form
    {
        private List<FormHijo> listaForm;
        private int indexA, indexB;

        public FormIsomorfismo()
        {
            InitializeComponent();
        }

        public void setListaForm(List<FormHijo> nueva)
        {
            listaForm = nueva;
        }

        private void FormIsomorfismo_Load(object sender, EventArgs e)
        {
            listGrafos.Items.Clear();

            this.Location = new Point(545, 270);

            for (int i = 0; i < listaForm.Count; i++)
            {
                if (listaForm[i].getTipoGrafo() == 1)
                    listGrafos.Items.Add(listaForm[i].Text + "\t:ST");
                else
                  if(listaForm[i].getTipoGrafo()== 2)
                     listGrafos.Items.Add(listaForm[i].Text + "\t:NoDir");
                  else
                     listGrafos.Items.Add(listaForm[i].Text + "\t:Dir");
            }
        }

        private void listGrafos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cad = (string)listGrafos.SelectedItem;

            if (opcA.Checked == true)
            {
                grafoA.Text = cad;
                indexA = listGrafos.SelectedIndex;
            }
            else
            {
                grafoB.Text = cad;
                indexB = listGrafos.SelectedIndex;
            }
        }

        private void btComparaGrafos_Click(object sender, EventArgs e)
        {
            if (grafoA.Text == grafoB.Text)
                MessageBox.Show("Los grafos tienen que ser distintos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                comparaGrafos();
        }

        private void comparaGrafos()
        {
            int [][] mA;
            int [][] mB;

            mA = listaForm[indexA].entregaMatriz();
            mB = listaForm[indexB].entregaMatriz();

            if (mA.Length == mB.Length)
            {
                if (compGrafos(mA, mB) == true || determinaIsomorfismo(mA, mB) == true)
                    MessageBox.Show("Los grafos son Isomorfos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Los grafos no son Isomorficos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El numero de vertices de los dos grafos\n tiene que ser igual", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);

        }

        private bool determinaIsomorfismo(int[][] mA, int[][] mB)
        {
            bool res = false; ;
            bool band;
            int indexB, nVert;
            List<int> lA, lB;

            nVert = mA.Length;

            for (int col = 0; col < nVert && ( res != true ); col++)
            {
                lA = new List<int>();
                calculaGrado(mA, col, ref lA);
                indexB = col + 1;

                if (indexB == nVert)
                    indexB = 0;

                band = false;

                while (indexB != col && band != true)
                {
                    lB = new List<int>();
                    calculaGrado(mB, indexB, ref lB);

                    if (comparaGrados(lA, lB) == true)
                        band = true;
                    else
                    {
                        indexB++;
                        if (indexB == nVert)
                            indexB = 0;
                    }
                }

                if (band == true)
                {
                    intercambiaRenCol(ref mB, col, indexB);
                    res = compGrafos(mA, mB);
                }
                else
                {
                    res = false;
                    break;
                }
            }
            
            return (res);
        }

        private void calculaGrado(int[][] m, int col, ref List<int> lista)
        {
            int pesoCol = 0;
            int acum;

            for (int i = 0; i < m.Length; i++)
                if (m[i][col] != 0)
                {
                    pesoCol += m[i][col];
                    acum = 0;

                    for(int j = 0; j < m.Length; j++)
                        acum += m[i][j];

                    lista.Add(acum);
                }

            lista.Add(pesoCol);
        }

        private bool comparaGrados(List<int> lA, List<int> lB)
        {
            bool res;
            bool band;

            if (lA.Count == lB.Count)
            {
                foreach (int g in lA)
                {
                    if (lB.Count > 0)
                    {
                        band = false;
                        foreach (int h in lB)
                            if (g == h)
                            {
                                band = true;
                                break;
                            }

                        if (band == true)
                            lB.Remove(g);
                    }
                    else
                    {
                        res = false;
                        break;
                    }
                }

                if (lB.Count > 0)
                    res = false;
                else
                    res = true;
            }
            else
                res = false;
               

            return res;   
        }

        private void intercambiaRenCol(ref int[][] mB, int col1, int col2)
        {
            int aux;

            //Intercambio de Columnas
            for (int i = 0; i < mB.Length; i++)
            {
                 aux = mB[i][col1];
                 mB[i][col1] = mB[i][col2];
                 mB[i][col2] = aux;
            }

            //Intercambio de renglones
            for (int j = 0; j < mB.Length; j++)
            {
               aux = mB[col1][j];
               mB[col1][j] = mB[col2][j];
               mB[col2][j] = aux;
            }
        }

        private bool compGrafos(int[][] mA, int[][] mB)
        {
            bool res = true;

            for (int i = 0; i < mA.Length; i++)
                for (int j = 0; j < mB.Length; j++)
                    if (mA[i][j] != mB[i][j])
                    {
                        res = false;
                        i = j = mB.Length;
                    }

            return (res);
         }
   }
}

























