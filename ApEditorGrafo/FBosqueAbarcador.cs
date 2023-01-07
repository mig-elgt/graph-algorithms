using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApEditorGrafo
{
    public partial class FBosqueAbarcador : Form
    {
        internal List<CNodoArista>[] listaDeArcos;

        public FBosqueAbarcador()
        {
            InitializeComponent();
        }

        private void cBtiposArcos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoArco, cont;
            string cadArco;

            tipoArco = cBtiposArcos.SelectedIndex;

            lBArcos.Items.Clear();
            cont = 0;
            foreach (CNodoArista nA in listaDeArcos[tipoArco])
            {
                cadArco = " " + ++cont + ".-  " + nA.getVO().getNombre() + " --> " + nA.getVD().getNombre();
                lBArcos.Items.Add(cadArco);
            }

        }
    }
}