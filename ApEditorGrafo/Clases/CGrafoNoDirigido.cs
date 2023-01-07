using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ApEditorGrafo.Clases;

namespace ApEditorGrafo
{
    [Serializable]
    class CGrafoNoDirigido : CGrafo
    {
        private CPilaVertices pila;
        private List<CNodoArista> L; //Lista de Aristas, que forman al Árbol Ábarcador de Costo minimo
        private List<CNodoVertice> raizes; //Lista para formar el Bosque abaracdor de Costo mínimo
        private List<CNodoVertice> listaPtsArt;
        private int CostoMinimo;
        private int numP;

        public CGrafoNoDirigido() { }

        public CGrafoNoDirigido(CGrafo g, int tipo)
        {
            this.setTipo(tipo);
            this.setListVert(g.getListVert());
            this.setNumNodos(g.getNumNodos());
        }

        public int getCostoMinimo()
        {
            CostoMinimo = 0;

            foreach (CNodoArista a in L)
                CostoMinimo += a.getPeso();

            return (CostoMinimo);
        }

        public List<CNodoVertice> getLisPtsArt()
        {
            return (listaPtsArt);
        }

        public List<CNodoVertice> getRaizes() { return (raizes); }

        public void insertaArista(CNodoVertice vO, CNodoVertice vD, float h, Color c)
        {
            CNodoArista nuevo;
            PointF p3, p4;
            p3 = new PointF();
            p4 = new PointF();
           
            //Relaicon de vO - vD
            nuevo = new CNodoArista(9999,vD,c);
            nuevo.setVO(vO);
            nuevo.setVD(vD);
            EstablecePuntosCtrl(ref p3, ref p4, vO.getPunto(), vD.getPunto(), h);
            nuevo.setPoint3Ctrl(p3);
            nuevo.setPoint4Ctrl(p4);
            nuevo.setAltura(h);
            vO.getListaArista().Add(nuevo);
            //Relaicon de vD - VO
            nuevo = new CNodoArista(9999,vO,c);
            nuevo.setPoint3Ctrl(p4);
            nuevo.setPoint4Ctrl(p3);
            nuevo.setVO(vO);
            nuevo.setVD(vD);
            nuevo.setAltura(h);
            vD.getListaArista().Add(nuevo);
        }

        public void eliminaVertice(CNodoVertice nodoVer)
        {
            foreach (CNodoVertice nV in getListVert())
              if( nodoVer != nV )
                for (int i = 0; i  < nV.getListaArista().Count; )
                    if ((nV.getListaArista())[i].getVertRel() == nodoVer)
                        (nV.getListaArista()).Remove((nV.getListaArista())[i]);
                    else
                        i++;

            foreach (CNodoArista nA in nodoVer.getListaArista())
                nA.getVertRel().decrementaGrado();

            getListVert().Remove(nodoVer);
        } 

        public void eliminaArista(CNodoVertice vO, CNodoVertice vD, CNodoArista nA)
        {
            //Decrementar grado
            vO.decrementaGrado();
            vD.decrementaGrado();
            vO.getListaArista().Remove(nA);
            
            foreach(CNodoArista nAux in vD.getListaArista())
                if (nAux.getPoint3Ctrl() == nA.getPoint4Ctrl())
                {
                    vD.getListaArista().Remove(nAux);
                    break;
                }
        }

        public bool CircuitoEuler()
        {
            foreach (CNodoVertice ver in getListVert())
                if (ver.getGrado() % 2 != 0)
                    return (false);

            return (true);
        }

        public bool CaminoEuleriano()
        {
            int cont = 0;
            bool res = false;

            foreach (CNodoVertice ver in getListVert())
               if (ver.getGrado()% 2 != 0)
                    cont++;

            if (cont == 2)
                res = true;

            return (res);
        }

        public bool GeneraCircuitoEuler()
        {
            if (CircuitoEuler() == true)
            {
                CGrafoNoDirigido g = new CGrafoNoDirigido();
                CNodoVertice nV, nVC;
                CNodoArista nA, nAC;

                foreach (CNodoVertice nVe in getListVert())
                    g.insVertice(nVe.getNombre(), new Point(1, 1), 0, new Rectangle(1, 1, 1, 1));

                for (int i = 0; i < getListVert().Count; i++)
                {
                    nV = getListVert()[i];
                    nVC = g.getListVert()[i];

                    for (int j = 0; j < nV.getListaArista().Count; j++)
                    {
                        nA = nV.getListaArista()[j];

                        if(nA.getVertRel() != null )
                          foreach(CNodoVertice nodo in g.getListVert())
                            if (nodo.getNombre().CompareTo(nA.getVertRel().getNombre()) == 0)
                            {
                                g.insertaArista(nVC, nodo,0F);
                                break;
                            }
                    }
                }
                
                pila = new CPilaVertices();
                CircuitoE(g.getListVert()[0], g);
                pila.Push(g.getListVert()[0]);
                
                return (true);
            }

            return (false);
        }
       
        public void setPila(CPilaVertices p)
        {
            pila = p;
        }

        public CPilaVertices getPila() { return (pila); }

        private bool CircuitoE(CNodoVertice ver, CGrafoNoDirigido g)
        {
            CNodoVertice nAux, vertAnt;
            bool band = true;
            int ged;

            ged = grado(g);

            if ( ged != 0)
            {
                pila.Push(ver);
                nAux = buscaCamino(ver);

                if (nAux == null)
                    band = false;
                else
                    do
                    {
                        g.eliminaArista(ver, nAux,null);
                        band = CircuitoE(nAux, g);

                        if (band == false)
                        {
                            pila.Pop();
                            vertAnt = nAux;
                            nAux = buscaCamino(ver);
                            g.insertaArista(ver, vertAnt,0F);
                        }

                    } while (band != true && nAux != null);
            }

            return (band);
        }

        private CNodoVertice buscaCamino(CNodoVertice ver)
        {
            if (ver.getListaArista().Count == 0)
                return (null);
             
            CNodoVertice verMenor = ver.getListaArista()[0].getVertRel();

            foreach (CNodoArista nA in ver.getListaArista())
                if (Convert.ToInt32(nA.getVertRel().getNombre()) < Convert.ToInt32(verMenor.getNombre()))
                    verMenor = nA.getVertRel();

            return (verMenor);
        }

        private static int grado(CGrafo g)
        {
            int acum = 0;

            foreach (CNodoVertice nV in g.getListVert())
                acum += nV.getListaArista().Count;

            return (acum / 2);
        }

        public int dameGrado()
        {
            int acum = 0;

            foreach (CNodoVertice nV in getListVert())
                acum += nV.getListaArista().Count;

            return (acum / 2);
        }

        public void AsignarColores()
        {
            List<int> colores, coloresAdy, colorAux;
            int contador, color;
            contador = 1;

            colores = new List<int>();
           
            foreach (CNodoVertice nV in getListVert())
            {   
                coloresAdy = new List<int>();

                if (nV.getListaArista().Count == 0)
                    color = contador-1;
                else
                {
                    foreach (CNodoArista nA in nV.getListaArista())
                    {
                        color = nA.getVertRel().getColor();
                        if (color > 0)
                            coloresAdy.Add(color);
                    }

                    if (coloresAdy.Count == 0 && colores.Count == 0)
                    {
                        colores.Add(contador);
                        color = contador;
                        contador++;
                    }
                    else
                    {
                        colorAux = new List<int>();
                        for (int i = 0; i < colores.Count; i++)
                            colorAux.Add(colores[i]);

                        for (int i = 0; i < coloresAdy.Count; i++)
                            colorAux.Remove(coloresAdy[i]);

                        if (colorAux.Count == 0)
                        {
                            color = contador;
                            colores.Add(contador);
                            contador++;
                        }
                        else
                        {
                            color = colorAux[0];
                            for (int i = 0; i < colorAux.Count; i++)
                                if (color < colorAux[i])
                                    color = colorAux[i];
                        }
                    }
                }
                
                nV.setColor(color);
            }
        }

        public bool CircuitoHamilton(CNodoVertice vO, CNodoVertice vD, CPilaVertices pila)
        {
            bool band = true;

            if (vD.getVisita() == true)
            {
                if (vD == vO)
                {
                    band = true;
                    foreach (CNodoVertice nV in getListVert())
                        if (nV.getVisita() == false)
                        {
                            band = false;
                            break;
                        }

                }
                else
                    band = false;
            }
            else
            {
                vD.setVisita(true);
                pila.Push(vD);

                foreach (CNodoArista nA in vD.getListaArista())
                {
                    band = CircuitoHamilton(vO, nA.getVertRel(), pila);
                    if (band == true)
                        break;
                }
                if (band == false)
                {
                    pila.Pop();
                    vD.setVisita(false); 
                }
            }

           return(band);
        }

        public List<CNodoArista> Prim()
        {
            List<CNodoVertice> V;
            List<CNodoVertice> U;
            CNodoArista aristaMC;

            V = getListVert();
            U = new List<CNodoVertice>();
            L = new List<CNodoArista>();
            
            U.Add(V[0]);

            while (U.Count != V.Count)
            {
                aristaMC = DameAristaMC(V, U);
                L.Add(aristaMC);
                U.Add(aristaMC.getVertRel());
            }

            return (L);
        }

        public List<CNodoArista> Kruskal()
        {
            List<CNodoVertice> V; //Lista de Vertices
            List<List<CNodoVertice>> C;//Componentes del Grafo
            List<CNodoArista> Q; // Lista de Arista del grafo
            CNodoVertice vO, vD;
            CNodoArista aristaMenorCst;

            int cU, cV;

            V = getListVert();
            C = CreaListComponentes(V);
            L = new List<CNodoArista>();
            Q = new List<CNodoArista>();
            cU = cV = -1;
            
            CargarAristas(V, Q);
            OrdenaAristas_Quisksort(Q);

            while (L.Count < (getNumNodos() - 1))
            {
                aristaMenorCst = AristaMenorPeso(Q);
               
                vO = BuscaVertOrigen(aristaMenorCst);
                vD = aristaMenorCst.getVertRel();
                
                BuscaComponente(vO, C, ref cU);
                BuscaComponente(vD, C, ref cV);

                if (cU != cV)
                {
                    L.Add(aristaMenorCst);
                    UnionDeComponentes(cU, cV, C);
                }
            }
            
            return (L);
        }

        private void OrdenaAristas_Quisksort(List<CNodoArista> Q)
        {
            Quicksort_Recursivo(Q, 0, Q.Count - 1);
        }

        private void Quicksort_Recursivo(List<CNodoArista> Q, int ini, int fin)
        {
            CNodoArista aristaAux;
            int izq, der, pos;
            bool band;

            aristaAux = null;
            pos = izq = ini;
            der = fin;
            band = true;

            while (band)
            {
                band = false;

                while((Q[pos].getPeso() <= Q[der].getPeso()) &&(pos != der ))
                       der--;

                if (pos != der)
                {
                    aristaAux = Q[pos];
                    Q[pos] = Q[der];
                    Q[der] = aristaAux;
                    pos = der;

                    while ((Q[pos].getPeso() >= Q[izq].getPeso()) && (pos != izq))
                        izq++;

                    if (pos != izq)
                    {
                        band = true;
                        aristaAux = Q[pos];
                        Q[pos] = Q[izq];
                        Q[izq] = aristaAux;
                        pos = izq;
                    }
                }
            }

            if ((pos - 1) > ini)
                Quicksort_Recursivo(Q, ini, pos - 1);

            if(fin > (pos+1))
                Quicksort_Recursivo(Q, pos + 1, fin);
        }

        private void UnionDeComponentes(int cU, int cV, List<List<CNodoVertice>> C)
        {
            foreach (CNodoVertice vert in C[cV])
                C[cU].Add(vert);

            C[cV].Clear();
        }

        private void BuscaComponente(CNodoVertice vert, List<List<CNodoVertice>> C, ref int comp)
        {
            for (int i = 0; i < C.Count; i++)
                if (C[i].Contains(vert) == true)
                {
                    comp = i;
                    break;
                }
        }

        private CNodoArista AristaMenorPeso(List<CNodoArista> Q)
        {
            CNodoArista ariMenor = null;
            
            ariMenor = Q[0];
            Q.Remove(ariMenor);
            
            return (ariMenor);
        }

        private CNodoVertice BuscaVertOrigen(CNodoArista a)
        {
            foreach (CNodoVertice v in getListVert())
                foreach (CNodoArista nA in v.getListaArista())
                    if (nA == a)
                        return (v);

            return (null);
        }

        private List<List<CNodoVertice>> CreaListComponentes(List<CNodoVertice> V)
        {
            List<List<CNodoVertice>> listComp;

            listComp = new List<List<CNodoVertice>>();

            for (int i = 0; i < V.Count; i++)
            {
                listComp.Add(new List<CNodoVertice>());
                listComp[i].Add(V[i]);
            }
            
            return (listComp);
        }

        private void CargarAristas(List<CNodoVertice> V, List<CNodoArista> Q)
        {
            foreach (CNodoVertice v in V)
            {
                v.setVisita(true);

                foreach (CNodoArista a in v.getListaArista())
                    if (a.getVertRel().getVisita() == false)
                        Q.Add(a);
            }

            foreach (CNodoVertice v in V)
                v.setVisita(false);
        }

        private CNodoArista DameAristaMC(List<CNodoVertice> V, List<CNodoVertice> U)
        {
            CNodoArista aristaAux = null;
            int menor = 99999;

            foreach(CNodoVertice v in U )
                foreach (CNodoArista nA in v.getListaArista())
                 if ((U.Contains(nA.getVertRel()) == false) && menor > nA.getPeso())
                    {
                        menor = nA.getPeso();
                        aristaAux = nA;
                    }

            return (aristaAux);
        }

        public void EstableceCostoArista(CNodoVertice vO, int costo, CNodoArista nA)
        {
            foreach (CNodoArista a in nA.getVertRel().getListaArista())
                if (a.getVertRel() == vO)
                    if (a.getPoint3Ctrl() == nA.getPoint4Ctrl() && a.getPoint4Ctrl() == nA.getPoint3Ctrl())
                        a.setPeso(costo);
        }

        public void EstableceColorArista(CNodoVertice vO, Color color, CNodoArista nA)
        {
            foreach (CNodoArista a in nA.getVertRel().getListaArista())
                if (a.getVertRel() == vO)
                    if (a.getPoint3Ctrl() == nA.getPoint4Ctrl() && a.getPoint4Ctrl() == nA.getPoint3Ctrl())
                        a.setColor(color);
        }

        public void RecorreGrafo()
        {
            foreach (CNodoVertice v in getListVert())
            {
                v.setVisita(false);
                foreach (CNodoArista a in v.getListaArista())
                    a.setTipoArco(0);
            }

            raizes = new List<CNodoVertice>();

            foreach (CNodoVertice v in getListVert())
                if (v.getVisita() == false)
                {
                    raizes.Add(v);
                    bpf(v, v.getListaArista());
                }

             
        }

        private void bpf(CNodoVertice v, List<CNodoArista> L)
        {
            CNodoVertice w;

            v.setVisita(true);
            v.setNP(++numP);

            foreach (CNodoArista a in L)
            {
                w = a.getVertRel();

                if (w.getVisita() == false)
                {
                    EstableceTipoArco(v, w, a, 1);
                    bpf(w, w.getListaArista());
                }
                else
                   if( a.getTipoArco() == 0)
                       EstableceTipoArco(v, w, a, 3);
            }
        }

        private void EstableceTipoArco(CNodoVertice v, CNodoVertice w, CNodoArista a, int tipoArco)
        {
            a.setTipoArco(tipoArco);

            foreach (CNodoArista nA in w.getListaArista())
                 if( nA.getVertRel() == v )
                     if ((nA.getPoint3Ctrl() == a.getPoint4Ctrl()) && (nA.getPoint4Ctrl() == a.getPoint3Ctrl()))
                     {
                         nA.setTipoArco(tipoArco);
                         break;
                     }
        }

        private void AsigaArcoCruzado()
        {
            foreach (CNodoVertice v in getListVert())
                foreach (CNodoArista a in v.getListaArista())
                    a.setTipoArco(4);
        }

        public void BusquedaEnApmlitud()
        {
            List<CNodoVertice> Vp;
            List<CNodoVertice> Q;
            CNodoVertice v;

            Vp = new List<CNodoVertice>();
            Q = new List<CNodoVertice>();
            raizes = new List<CNodoVertice>();

            creaSubVert(ref Vp, getListVert()[0]);
            Q.Add(getListVert()[0]);
            Q[0].setVisita(true);
            raizes.Add(Q[0]);
            AsigaArcoCruzado();

            while (Vp.Count != 0)
            {
                v = Q[0];
                Q.RemoveAt(0);

                foreach (CNodoArista a in v.getListaArista())
                {
                    if (a.getVertRel().getVisita() == false)
                    {
                        EstableceTipoArco(v, a.getVertRel(), a, 1);
                        a.getVertRel().setVisita(true);
                        Q.Add(a.getVertRel());
                        Vp.Remove(a.getVertRel());
                    }
                }

                if (Q.Count == 0)
                {
                    Q.Add(Vp[0]);
                    raizes.Add(Q[0]);
                    Vp[0].setVisita(true);
                    Vp.Remove(Q[0]);
                }
                
            }
        }

        private void creaSubVert(ref List<CNodoVertice> listaVP, CNodoVertice primerVert)
        {
            foreach (CNodoVertice v in getListVert())
                if (v != primerVert)
                    listaVP.Add(v);
        }

        public List<CNodoVertice> BuscaPuntosDeArticulacion()
        {
            listaPtsArt = new List<CNodoVertice>();
            
            RecorreGrafo();
            bp();//Busqueda en profundidad para establecer el numero bajo a los vertices

            if (numArcosArbol(getListVert()[0]) > 1)
              listaPtsArt.Add(getListVert()[0]);

            getListVert()[0].setVisita(true);

            foreach (CNodoVertice v in getListVert())
                if (v.getVisita() == false)
                    DeterminaPtsArti(v, v.getListaArista());

            foreach (CNodoVertice vert in getListVert())
                vert.setVisita(false);

            return (listaPtsArt);
        }

        private void DeterminaPtsArti(CNodoVertice v, List<CNodoArista> L)
        {
            v.setVisita(true);

            foreach (CNodoArista a in L)
                if (a.getVertRel().getVisita() == false)
                    DeterminaPtsArti(a.getVertRel(), a.getVertRel().getListaArista());

            foreach (CNodoArista nA in L)
                if (nA.getTipoArco() == 1 && nA.getVertRel().getNP() > v.getNP())
                    if (nA.getVertRel().getNumBajo() >= v.getNP())
                        listaPtsArt.Add(v);
        }

        private int numArcosArbol(CNodoVertice v)
        {
            int cont = 0;

            foreach (CNodoArista nA in v.getListaArista())
                if (nA.getTipoArco() == 1 )
                    cont++;

            return (cont);
        }

        private void bp()
        {
            foreach (CNodoVertice v in getListVert())
                if (v.getVisita() == true)
                    EstableceNumBajo(v, v.getListaArista());
        }

        private void EstableceNumBajo(CNodoVertice v, List<CNodoArista> L)
        {
            int[] vNum = new int[3];
            v.setVisita(false);
            
            foreach (CNodoArista a in L)
                if (a.getVertRel().getVisita() == true)
                    EstableceNumBajo(a.getVertRel(), a.getVertRel().getListaArista());

            vNum[0] = v.getNP();
            vNum[1] = NumPEnArcosRetro(v);
            vNum[2] = NumBajoEnDescendiente(v);

            v.setNumBajo(Minimo(vNum));
        }

        private int NumPEnArcosRetro(CNodoVertice v)
        { 
            int menor = 9999;

            foreach(CNodoArista a in v.getListaArista())
                if(a.getTipoArco() == 3 )
                    if(menor > a.getVertRel().getNP())
                        menor = a.getVertRel().getNP();

            if(menor == 9999)
                menor = 0;

           return(menor);
        }

        private int NumBajoEnDescendiente(CNodoVertice v)
        {
            int menor = 9999;

            foreach (CNodoArista a in v.getListaArista())
                if (a.getTipoArco() == 1 && a.getVertRel().getNP() > v.getNP())
                    if (menor > a.getVertRel().getNumBajo())
                        menor = a.getVertRel().getNumBajo();

            if (menor == 9999)
                menor = 0;

            return (menor);
        }

        private int Minimo(int []vNum)
        {
            List<int> listaNum = new List<int>();

            for (int i = 0; i < 3; i++)
                if (vNum[i] != 0)
                    listaNum.Add(vNum[i]);

            listaNum.Sort();

            return (listaNum[0]);
        }
    }
}











