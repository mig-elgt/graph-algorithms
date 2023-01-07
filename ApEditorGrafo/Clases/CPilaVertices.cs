using System;
using System.Collections.Generic;
using System.Text;

namespace ApEditorGrafo.Clases
{
    class CPilaVertices
    {
        private CNodo cab;
        private CNodo tope;

        public CPilaVertices(){}

        public void Push(CNodoVertice n)
        {
            CNodo nuevo = new CNodo(n);

            if (cab == null)
                cab = nuevo;
            else
                tope.setSigNodo(nuevo);

            tope = nuevo;
        }

        public CNodo getTope()
        {
            return (tope);
        }

        public CNodoVertice Pop()
        {
            CNodo aux, ant = null;
            CNodoVertice ver;
            
            aux = cab;

            if (tope == null)
                return (null);
            else
            {
                while (aux != tope)
                {
                    ant = aux;
                    aux = aux.getSigNodo();
                }

                if (tope == cab)
                    cab = null;
                else
                    ant.setSigNodo(null);

                ver = tope.getInfo();
                tope = ant;
            }

            return (ver);
        }
    }
}



















