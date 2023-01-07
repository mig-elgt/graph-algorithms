using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ApEditorGrafo
{
    [Serializable]
    class CNodoVertice
    {
        private List<CNodoArista> listaArista;
        private string nombre;
        private int grado;
        private int gradoInt;
        private int gradoExt;
        private Point centro;
        private int radio;
        private Rectangle rect;
        private bool visita;
        private int color;
        private bool pintado;
        private int numProfundidad; //Este atributo solo se ultiliza para digrafos
        private Color colorVert;
        private int numDescencientes; //Este atributo es aplicado en los algoritmos de Busqueda en profundidad
        private int numBajo;

        public CNodoVertice() { }

        public void setNumBajo(int nB)
        {
            numBajo = nB;
        }

        public int getNumBajo()
        {
            return (numBajo);
        }

        public void setNumDesc(int n)
        {
            numDescencientes = n;
        }

        public int getNumDesc()
        {
            return (numDescencientes);
        }

        public void setColor(int c)
        {
            color = c;
        }

        public void setColorVert(Color c)
        {
            colorVert = c;
        }

        public Color getColorVert()
        {
            return (colorVert);
        }

        public void setNP(int nP)
        {
            numProfundidad = nP;
        }

        public int getNP()
        {
            return (numProfundidad);
        }

        public int getColor()
        {
            return (color);
        }

        public CNodoVertice(string name) { nombre = name; colorVert = Color.Blue; }

        public CNodoVertice( string name, Point p, int r, Rectangle re )
        {
           nombre = name;
           centro = p;
           listaArista = new List<CNodoArista>();
           radio = r;
           rect = re;
           gradoInt = gradoExt = grado = 0;
           visita = false;
           colorVert = Color.Blue;
        }

        public void setVisita(bool v) { visita = v; }
        public bool getVisita() { return (visita); }
        public void incGradoInt() { gradoInt++; }
        public void decGradoInt() { gradoInt--; }
        public void incGradoExt() { gradoExt++; }
        public void decGradoExt() { gradoExt--; }
        public int getGradoInt(){return(gradoInt);}
        public int getGradoExt() { return (gradoExt); }
        public void setPintado(bool res)
        {
            pintado = res;
        }
        public bool getPintado()
        {
            return (pintado);
        }
        public void aumentaGrado() { grado++; }
        public void decrementaGrado() { grado--; }
        public int getGrado() { return (grado); }

        public string getNombre()
        { 
            return (nombre); 
        }
        
        public void setPunto(Point p){ 
            centro = p; 
        }
        
        public Point getPunto(){ 
            return (centro); 
        }

        public void setRect(Rectangle r){
            rect = r;
        }

        public Rectangle getRect() 
        {
            return (rect); 
        }
        
        public List<CNodoArista> getListaArista()
        { 
            return (listaArista); 
        }

        public bool descendiente(CNodoVertice v)
        {
            return (v.getNP() <= this.getNP() && this.getNP() <= v.getNP() + v.getNumDesc());     
        }

        public void setDescendientes(int d)
        {
            numDescencientes = d;
        }
     }
}
