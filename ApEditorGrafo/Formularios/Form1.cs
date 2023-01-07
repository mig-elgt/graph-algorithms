using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ApEditorGrafo.Formularios;

namespace ApEditorGrafo
{
    public partial class FormPadre : Form
    {
        List<FormHijo> listaForm;

        public FormPadre()
        {
            InitializeComponent();
            listaForm = new List<FormHijo>();
        }

        private void ArchivoNuevo_Click(object sender, EventArgs e)
        {
            FormHijo nuevoFormHijo = new FormHijo();

            nuevoFormHijo.Text = "Grafo " + MdiChildren.Length.ToString();
            nuevoFormHijo.MdiParent = this;
            /*listaForm.Add(nuevoFormHijo);
            
            if( listaForm.Count > 1)
               OperaIsomorfismo.Enabled = true;
            */

            nuevoFormHijo.Show();
        }

        private void ArchivoCerrar_Click(object sender, EventArgs e)
        {
            FormHijo formHijoActivo = (FormHijo)ActiveMdiChild;

            if (formHijoActivo != null)
            {
                formHijoActivo.Close();
                listaForm.Remove(formHijoActivo);

                if (listaForm.Count < 2)
                    OperaIsomorfismo.Enabled = false;
            }
        }

        private void VentanaCascada_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void VentanaHorizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void VentanaVert_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void FormPadre_MdiChildActivate(object sender, EventArgs e)
        {
            ToolStripManager.RevertMerge(this.BarraHerramientasFormPadre);

            FormHijo vHijaAc = (FormHijo)this.ActiveMdiChild;

            if (vHijaAc != null)
                ToolStripManager.Merge(vHijaAc.BarraDeHerra, this.BarraHerramientasFormPadre);
        }

        private void OperaIsomorfismo_Click(object sender, EventArgs e)
        {
            FormIsomorfismo dlgIsomor = new FormIsomorfismo();
            Form[] formHijo = MdiChildren;

            for (int i = 0; i < formHijo.Length; i++)
                listaForm.Add((FormHijo)formHijo[i]);

            dlgIsomor.setListaForm(listaForm);
            dlgIsomor.ShowDialog();
            listaForm.Clear();
        }
    }
}