using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ApEditorGrafo
{
    [Serializable]
    class CNodoArista
    {
        private int peso;
        private CNodoVertice verRel;
        private PointF p3;
        private PointF p4;
        private CNodoVertice vO;
        private CNodoVertice vD;
        private float altura;
        private Color color;
        private int tipoArco; // 1.- Arbol, 2.- Avance 3.- Retroceso, 4.- Cruzados

        public CNodoArista() { color = Color.Red; }

        public CNodoArista(CNodoVertice vRel) { verRel = vRel; color = Color.Black; }

        public void setTipoArco(int type)
        {
            tipoArco = type;
        }

        public int getTipoArco()
        {
            return (tipoArco);
        }
        
        public void setColor(Color nuevo)
        {
            color = nuevo;
        }

        public Color getColor()
        {
            return (color);
        }

        public CNodoArista(int p, CNodoVertice vRel, Color c)
        {
            peso = p;
            verRel = vRel;
            color = c;
        }

        public void setAltura(float nueva)
        {
            altura = nueva;
        }

        public float getAltura()
        {
            return (altura);
        }
        public void setVO(CNodoVertice nO)
        {
            this.vO = nO;
        }

        public CNodoVertice getVO()
        {
            return (vO);
        }

        public void setVD(CNodoVertice nD)
        {
            this.vD = nD;
        }

        public CNodoVertice getVD()
        {
            return (vD);
        }

        public void setPoint3Ctrl(PointF p)
        {
            p3 = p;
        }

        public PointF getPoint3Ctrl()
        {
            return (p3);
        }

        public void setPoint4Ctrl(PointF p)
        {
            p4 = p;
        }

        public PointF getPoint4Ctrl()
        {
            return (p4);
        }
    
        public void setVertRel(CNodoVertice vR)
        { 
            verRel = vR; 
        }

        public CNodoVertice getVertRel()
        {
            return (verRel);
        }

        public void setPeso(int p)
        {
            peso = p;
        }
        public int getPeso()
        {
            return (peso);
        }
    }
}
