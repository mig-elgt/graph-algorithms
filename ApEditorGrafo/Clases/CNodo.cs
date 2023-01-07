using System;
using System.Collections.Generic;
using System.Text;

namespace ApEditorGrafo.Clases
{
    class CNodo
    {
        private CNodoVertice vertice;
        private CNodo sigNodo;

        public CNodo(CNodoVertice nuevo)
        {
            vertice = nuevo;
        }

        public void setSigNodo(CNodo n)
        {
            sigNodo = n;
        }

        public CNodo getSigNodo()
        {
            return (sigNodo);
        }

        public CNodoVertice getInfo()
        {
            return (vertice);
        }

    }
}
