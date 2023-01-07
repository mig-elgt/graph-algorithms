using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ApEditorGrafo
{
    [Serializable]
    class CGrafo
    {
        private int tipo;  /* 0: Grafo, 1: Grafo Dirigido, 2: Grafo No dirigido */
        private List<CNodoVertice> listaVertices;
        private int[][] matrizAdyacencia;
        private int numNodos;
        private int[][] matrizCostos;

        public CGrafo()
        {
            listaVertices = new List<CNodoVertice>();
            tipo = 0;
            numNodos = 0;
        }

        public void creaListVert()
        {
            listaVertices = new List<CNodoVertice>();
        }
        public void incNodos() { numNodos++; }
        public void decNodos() { numNodos--; }
        public int getNumNodos() { return (numNodos); }
        public void setNumNodos(int n) { numNodos = n; }

        public int[][] getMatrizCostos()
        {
            return (matrizCostos);
        }

        public void crearMatrizCostos()
        {
            matrizCostos = new int[numNodos][];

            for (int i = 0; i < numNodos; i++)
                matrizCostos[i] = new int[numNodos];

            iniMatrizCostos();
        }

        public void setAtCosto(int reng, int col, int costo)
        {
            matrizCostos[reng][col] = costo;
        }

        public int getAtCosto(int i, int j)
        {
            return (matrizCostos[i][j]);
        }

        private void iniMatrizCostos()
        {
            List<CNodoArista> lA;

            for (int i = 0; i < numNodos; i++)
                for (int j = 0; j < numNodos; j++)
                   if (i != j)
                      matrizCostos[i][j] = 9999;
            
            for (int i = 0; i < listaVertices.Count; i++)
            {
                lA = listaVertices[i].getListaArista();
                for (int j = 0; j < lA.Count; j++)
                    setAtCosto(i, listaVertices.IndexOf(lA[j].getVertRel()), lA[j].getPeso());
            }
       }

        public void setTipo(int t)
        {
            tipo = t;
        }

        public int getTipo()
        {
            return (tipo);
        }

        public List<CNodoVertice> getListVert()
        {
            return (listaVertices);
        }

        public void setListVert(List<CNodoVertice> l)
        {
            listaVertices = l;
        }

        public void insVertice(string nombre, Point p, int r, Rectangle rect)
        {
            listaVertices.Add(new CNodoVertice(nombre, p, r, rect));
        }

        public CNodoVertice getVertice(int pos)
        {
            return (listaVertices[pos]);
        }

        public void eliminaVertice(CNodoVertice nodoVer)
        {
            bool band;
            CNodoArista nAux = null;

            foreach (CNodoVertice nV in listaVertices)
            {
                band = false;

                if (nV != nodoVer)
                    foreach (CNodoArista nA in nV.getListaArista())
                        if (nA.getVertRel() == nodoVer)
                        {
                            nAux = nA;
                            band = true;
                            break;
                        }

                if (band == true)
                {
                    nV.getListaArista().Remove(nAux);
                    nV.decGradoExt();
                }
            }

            foreach (CNodoArista nA in nodoVer.getListaArista())
                nA.getVertRel().decGradoInt();

            listaVertices.Remove(nodoVer);
        }

        public void insertaArista(CNodoVertice vO, CNodoVertice vD, float h)
        {
            CNodoArista nuevo;
            PointF p3, p4;
            p3 = new PointF();
            p4 = new PointF();
            
            //Relacion de vO - vD
            nuevo = new CNodoArista(vD);
            nuevo.setPeso(9999);
            nuevo.setVO(vO );
            nuevo.setVD(vD);
            EstablecePuntosCtrl(ref p3, ref p4, vO.getPunto(), vD.getPunto(),h);
            nuevo.setPoint3Ctrl(p3);
            nuevo.setPoint4Ctrl(p4);
            nuevo.setAltura(h);
            vO.getListaArista().Add(nuevo);
        }

        public void eliminaArista(CNodoVertice vO, CNodoVertice vD, CNodoArista aux)
        {
            bool band = false;
            vO.decGradoExt();
            vD.decGradoInt();

            foreach (CNodoVertice nV in listaVertices)
            {
                if (nV == vO)
                {
                    nV.getListaArista().Remove(aux);
                    break;
                }
            }
        }

        public void eliminate()
        {
            listaVertices.Clear();
            numNodos = 0;
        }

        public int[][] generaMatrizAdyacencia()
        {
            creaMatriz();

            CNodoVertice nV;
            CNodoArista nA;
            int posJ;

            for (int i = 0; i < listaVertices.Count; i++)
            {
                nV = listaVertices[i];

                for (int j = 0; j < nV.getListaArista().Count; j++)
                {
                    nA = nV.getListaArista()[j];
                    posJ = listaVertices.IndexOf(nA.getVertRel());
                    matrizAdyacencia[i][posJ]++;
                }

            }
            
            return (matrizAdyacencia);
        }

        public int getAtRelacion(int i, int j)
        {
           return(matrizAdyacencia[i][j]);
        }

        private void creaMatriz()
        {
            matrizAdyacencia = new int[numNodos][];

            for (int i = 0; i < numNodos; i++)
                matrizAdyacencia[i] = new int[numNodos];
        }

        public bool ConectividadAB(CNodoVertice nO, CNodoVertice nD)
        {
            bool res = false;

            if (nO != null && nO == nD)
                res = true;
            else
                if (nO.getVisita() == false)
                {
                    nO.setVisita(true);

                    foreach (CNodoArista nA in nO.getListaArista())
                    {
                        res = ConectividadAB(nA.getVertRel(), nD);
                        if (res == true)
                            break;
                    }
                }

            return (res);
        }

        public void EstablecePuntosCtrl(ref PointF p3, ref PointF p4, Point p1, Point p2, float h)
        {
            float x3, y3, x1, y1, x2, y2, x4, y4, radio, dx, dY;
            float cA, cO;
            double angTheta, angAlpha = 0;

            x1 = (float)p1.X;
            y1 = (float)p1.Y;
            x2 = (float)p2.X;
            y2 = (float)p2.Y;

            x3 = x1 + (x2 - x1) / 3;
            y3 = y1 + (y2 - y1) / 3;
            x4 = x1 + (x2 - x1) * 2 / 3;
            y4 = y1 + (y2 - y1) * 2 / 3;

            cA = x2 - x1;
            cO = y2 - y1;

            radio = (float)(Math.Sqrt(((double)cA * (double)cA) + ((double)cO * (double)cO)) * h);
            angTheta = Math.Atan(((double)cO / (double)cA)) * 180 / Math.PI;


            if (x2 > x1)
                angAlpha = angTheta - 90;
            else
                angAlpha = angTheta + 90;

            dx = (float)(radio * Math.Cos((double)angAlpha * Math.PI / 180));
            dY = (float)(radio * Math.Sin((double)angAlpha * Math.PI / 180));

            x3 += dx;
            y3 += dY;
            x4 += dx;
            y4 += dY;

            p3 = new PointF(x3, y3);
            p4 = new PointF(x4, y4);
        }
       
        public void ActulizaPtsCtrl(CNodoVertice vert)
        {
            PointF p3, p4;
            p3 = new PointF();
            p4 = new PointF();

            if (tipo == 3)//En caso de que el grafo sea origen
            {
               foreach(CNodoVertice nV in listaVertices)
                   if (nV != vert)
                       foreach (CNodoArista aux in nV.getListaArista())
                       {
                           EstablecePuntosCtrl(ref p3, ref p4, aux.getVO().getPunto(), aux.getVD().getPunto(), aux.getAltura());
                           aux.setPoint3Ctrl(p3);
                           aux.setPoint4Ctrl(p4);
                       }
            }

            foreach (CNodoArista aux in vert.getListaArista())
            {
                EstablecePuntosCtrl(ref p3, ref p4, aux.getVO().getPunto(), aux.getVD().getPunto(), aux.getAltura());

                if (aux.getVO() == vert)
                {
                    foreach (CNodoArista aux2 in aux.getVertRel().getListaArista())
                    {
                         if (aux2.getVO() == vert && aux2.getPoint3Ctrl() == aux.getPoint4Ctrl() && aux2.getPoint4Ctrl() == aux.getPoint3Ctrl())
                            {
                                aux2.setPoint3Ctrl(p4);
                                aux2.setPoint4Ctrl(p3);
                                break;
                            }
                    }
                    aux.setPoint3Ctrl(p3);
                    aux.setPoint4Ctrl(p4);
                }
                else
                {
                    foreach (CNodoArista aux2 in aux.getVertRel().getListaArista())
                    {
                        if (aux2.getVD() == vert && aux2.getPoint3Ctrl() == aux.getPoint4Ctrl() && aux2.getPoint4Ctrl() == aux.getPoint3Ctrl())
                         {
                                aux2.setPoint3Ctrl(p3);
                                aux2.setPoint4Ctrl(p4);
                                break;
                         }
                    }
                        aux.setPoint3Ctrl(p4);
                        aux.setPoint4Ctrl(p3);
                    }
                }
            }

        public void EstableceColorNodo()
        {
            foreach (CNodoVertice v in listaVertices)
                v.setColorVert(Color.Blue);
        }

        public void ActualizaColorArista()
        {
            foreach (CNodoVertice nV in getListVert())
                foreach (CNodoArista nA in nV.getListaArista())
                    nA.setColor(Color.Black);
        }
      }
 }
