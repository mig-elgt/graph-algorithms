using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ApEditorGrafo
{
    [Serializable]
    class CGrafoDirigido : CGrafo
    {
        private int[] D;//Arreglo que almacena los costos especiales del vertice origen al ene vert de la digrafa
        private CNodoVertice[] P;//Este arreglo almacena los vertices que representan el camino mas corto
        private int[][] A;//Matriz de longitud utilizada en el algoritmo de Floyd
        private int[][] C;//Matriz de caminos, almacena todos los vertices intermedios
        private int[][] CT;//Esta matriz representa la Cerradura Transitiva de la matriz de Adyacendia de la digrafa
        private List<List<CNodoVertice>> CP; //Componentes fuertes del grafos dirigido
        private int nP;
        private int numComp;
        private List<CNodoArista> []listaDeArcos; // Tipos de Arcos pertenecintes al Grafo
        private List<CNodoVertice> raizes;

        public CGrafoDirigido(CGrafo g, int t)
        {
            this.setTipo(t);
            this.setListVert(g.getListVert());
            this.setNumNodos(g.getNumNodos());
        }

        public CGrafoDirigido()
        {
            setTipo(3);
            creaListVert();
        }

        public List<CNodoArista>[] GetListaDeArcos()
        {
            return (listaDeArcos);
        }

        public int getAtLongitud(int i)
        {
           return(D[i]);
        }

        public int getALong(int i, int j)
        {
             return(A[i][j]);
        }

        public int getAtCamino(int i, int j)
        {
            return(C[i][j]);
        }

        public bool CircuitoEuleriano() 
        {
            bool res = false;
            return (res);
        }

        public bool CaminoEuleriano()
        {
            bool res = false;
            return (res);
        }

        public int getAtCerraduraTransitiva(int i, int j)
        {
            return(CT[i][j]);
        }

        public void Dijkstra()
        {
            List<CNodoVertice> V;
            List<CNodoVertice> S;
            int[][] C;
            int w,v;
            bool band;
           
            V = getListVert();
            C = getMatrizCostos(); 
            S = new List<CNodoVertice>(getNumNodos());
            D = new int[getNumNodos()];
 
            S.Insert(0, V[0]);
            
            for(int j = 1; j < getNumNodos(); j++)
                D[j] = C[0][j];

            for (int i = 1; i < getNumNodos(); i++)
            {
                w = buscaMenor(V, S);
                S.Insert(i, V[w]);

                foreach (CNodoArista nA in V[w].getListaArista())
                {
                    band = false;

                    foreach (CNodoVertice nV in S)
                        if (nA.getVertRel() == nV)
                        {
                            band = true;
                            break;
                        }

                    if (!band)
                    {
                        v = V.IndexOf(nA.getVertRel());

                        if (D[v] > D[w] + C[w][v])
                            D[v] = D[w] + C[w][v];
                        else
                            D[v] = D[v];
                    }
               }
            }
        }
      
        //Se escoge el vertice con costos menor del conjunto V-S
        public int buscaMenor(List<CNodoVertice> V, List<CNodoVertice> S)
        {
            int i, pos,menor = 999999;
            bool band;
            pos = 0;

            for (i = 0; i < V.Count; i++)
            {
                band = false;

                for (int k = 0; k < S.Count && !band ; k++)
                    if (V[i] == S[k])
                        band = true;

                if (!band)
                    if (menor > D[i])
                    {
                        menor = D[i];
                        pos = i;
                    }
            }

            return (pos);
        }

        public void Floyd()
        {
            int N;

            iniMatrizLongitud();
            iniMatrizCaminos();
            
            N = getNumNodos();

            for(int k = 0; k < N; k++)
                for(int i = 0; i < N; i++)
                    for(int j = 0; j < N; j++)
                        if ((k != i && i != j && k != j) && (A[i][k] + A[k][j] < A[i][j]))
                        {
                            A[i][j] = A[i][k] + A[k][j];
                            C[i][j] = k + 1;
                        }
        }

        private void crearMatriz(ref int[][] M)
        {
            M = new int[getNumNodos()][];
            
            for(int i = 0; i < getNumNodos(); i++)
                M[i] = new int[getNumNodos()];
        }

        private void iniMatrizLongitud()
        {
            crearMatriz(ref A);
            
            for (int i = 0; i < getNumNodos(); i++)
                for (int j = 0; j < getNumNodos(); j++)
                    A[i][j] = getAtCosto(i, j);
        }

        private void iniMatrizCaminos()
        {
            crearMatriz(ref C);
        }

        public void Warshall()
        {
            int k,i,j,N;

            crearMatriz(ref CT);
            N = getNumNodos();

            //Inicializar la matriz CT
            for ( i = 0; i < N; i++)
                for ( j = 0; j < N; j++)
                    CT[i][j] = getAtRelacion(i, j);

            for (k = 0; k < N; k++)
                for (i = 0; i < N; i++)
                    for (j = 0; j < N; j++)
                        if (CT[i][j] == 0)
                            if (CT[i][k] == 1 && CT[k][j] == 1)
                                CT[i][j] = 1;
       }

        public List<List<CNodoVertice>> generaCompFuertes()
        {
            CP = new List<List<CNodoVertice>>();
            CGrafoDirigido Gr;
            CNodoVertice vertMayor;

            foreach (CNodoVertice nV in getListVert())
            {
                nV.setVisita(false);
                foreach (CNodoArista nA in nV.getListaArista())
                    nA.setColor(Color.Black);
            }
            numComp = 0;
            //1.- Busqueda en profundidad con el grafo dirigido originnal
            foreach (CNodoVertice nV in getListVert())
                if (nV.getVisita() == false)
                    fpRecMod(nV, nV.getListaArista());

            //2.- Crear un nuevo grafo con las relacion invertidas entre los vertices
            Gr = crearGR();
            
            //3.- Aplicar la busqueda en profundidad al Gr, tomando como punto de partida el vertice
            //    con mayor numero de profundidad
            do
            {
                vertMayor = buscarVertMayor(Gr.getListVert());
                CP.Add( new List<CNodoVertice>());
                fpRec(vertMayor, vertMayor.getListaArista());
                numComp++;
           
            }while(!AllVisited(Gr.getListVert()));


            return (CP);
        }

        private bool AllVisited(List<CNodoVertice> lV)
        {
            foreach (CNodoVertice nV in lV)
                if (nV.getVisita() == false)
                    return (false);

            return (true);
        }

        private CNodoVertice buscarVertMayor(List<CNodoVertice> lV)
        {
            CNodoVertice vertMayor = null;

            foreach(CNodoVertice nV in lV )
                 if(nV.getVisita() == false)
                 {
                     vertMayor = nV;
                     break;
                 }

             foreach (CNodoVertice nv in lV)
                 if (nv.getVisita() == false && vertMayor.getNP() < nv.getNP())
                     vertMayor = nv;

             return (vertMayor);
        }

        private CGrafoDirigido crearGR()
        {
            CGrafoDirigido GrAux = new CGrafoDirigido();
            CNodoVertice vertAux;
            int vO, vD, i = 0;
            nP = 0;
            //Insertar los vertices
            foreach (CNodoVertice nV in getListVert())
            {
                GrAux.insVertice(nV.getNombre(), nV.getPunto(), 20, nV.getRect());
                vertAux = GrAux.getVertice(i);
                vertAux.setNP(nV.getNP());
                i++;
            }
            
            //Insertar las aristas en forma inversa
            foreach (CNodoVertice nV in getListVert())
                foreach (CNodoArista nA in nV.getListaArista())
                {
                    vD = getListVert().IndexOf(nV);
                    vO = getListVert().IndexOf(nA.getVertRel());
                  
                    GrAux.insertaArista(GrAux.getListVert()[vO], GrAux.getListVert()[vD], 0);
                }

            return (GrAux);
        }

        private void fpRecMod(CNodoVertice vert, List<CNodoArista> lA)
        {
            vert.setVisita(true);

            foreach (CNodoArista nA in lA)
                if (nA.getVertRel().getVisita() == false)
                    fpRecMod(nA.getVertRel(), nA.getVertRel().getListaArista());

            vert.setNP(++nP);
        }

        private void fpRec(CNodoVertice vert, List<CNodoArista> lA)
        {
            vert.setVisita(true);
            CP[numComp].Add(vert);

            foreach (CNodoArista nA in lA)
                if (nA.getVertRel().getVisita() == false)
                    fpRec(nA.getVertRel(), nA.getVertRel().getListaArista());
                
        }

        public List<CNodoArista>[] generaArcos()
        {
            listaDeArcos = new List<CNodoArista>[4];
            raizes = new List<CNodoVertice>();
            nP = 0;

            foreach (CNodoVertice nV in getListVert())
            {
                nV.setVisita(false);
                nV.setDescendientes(0);
                
                foreach (CNodoArista nA in nV.getListaArista())
                    nA.setTipoArco(0);
            }

            for (int i = 0; i < 4; i++)
                listaDeArcos[i] = new List<CNodoArista>();

            foreach (CNodoVertice nV in getListVert())
                if (nV.getVisita() == false)
                {
                    raizes.Add(nV);
                    BusquedaProfundidad(nV, nV.getListaArista());
                }

            estableceArcos();

            return (listaDeArcos);
        }

        public List<CNodoVertice> getRaizes()
        {
            return (raizes);
        }

        public void estableceArcos()
        {
            int tipoArco;
            CNodoVertice v;

            foreach (CNodoVertice w in getListVert())
                foreach (CNodoArista nA in w.getListaArista())
                {
                    v = nA.getVertRel();

                    if (nA.getTipoArco() != 1)
                    {
                        if ((w.getNP() < v.getNP()))
                        {
                            if (v.descendiente(w))
                                tipoArco = 1;
                            else
                                tipoArco = 3;
                        }
                        else
                        {
                            if (w.descendiente(v))
                                tipoArco = 2;
                            else
                                tipoArco = 3;
                        }

                        nA.setTipoArco(tipoArco+1);
                        listaDeArcos[tipoArco].Add(nA);
                    }
                }
        }

        private void BusquedaProfundidad(CNodoVertice v, List<CNodoArista> lA)
        {
            CNodoVertice w;

            v.setVisita(true);
            v.setNP(++nP);

            foreach (CNodoArista nA in lA)
            {
                w = nA.getVertRel();

                if (w.getVisita() == false)
                {
                    nA.setTipoArco(1);
                    listaDeArcos[0].Add(nA);
                    BusquedaProfundidad(w, w.getListaArista());
                    v.setNumDesc(v.getNumDesc()+1 + w.getNumDesc());
                }
            }
        }
    }
}





