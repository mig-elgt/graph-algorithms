using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApEditorGrafo
{
    public partial class FCapturaPesoArista : Form
    {
        internal int posX;
        internal int posY;

        public FCapturaPesoArista()
        {
            InitializeComponent();
        }

        private void FCapturaPesoArista_Load(object sender, EventArgs e)
        {
            this.Location = new Point(posX, posY);
            this.Size = new Size(60, 60);
        }
    }
}