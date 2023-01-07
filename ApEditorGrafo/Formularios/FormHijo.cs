using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ApEditorGrafo.Formularios;
using ApEditorGrafo.Clases;

namespace ApEditorGrafo
{
    public partial class FormHijo : Form
    {
        private int cont;
        private int opc;
        private Point p1;
        private Point p2;
        private bool band;
        private Graphics pagPri;
        private bool bandGrafo;
        private Pen pluma;
        private Pen plumaAr;
        private Rectangle rt;
        private Size size;
        private Bitmap pagina;
        private Font fuente;
        private int tipoGrafo;
        private int radio;
        private int nNodo;
        private CGrafo Grafo;
        private CGrafoNoDirigido GrafoNoDir;
        private CGrafoDirigido GrafoDir;
        private CNodoVertice verSelec;
        private CNodoVertice vertOrigen;
        private CNodoVertice vertDestino;
        private bool bandMenu;
        private float altura;
        private bool numCromatico;
        private CPilaVertices caminoVert;
        private CNodoVertice ant;
        private PointF[] ptsBezier;
        private bool grafoPonderado;
        private bool opcDijkstra;
        private bool opcWarshall;
        private int numCam;
        private List<CNodoVertice> listaCamino;

        public FormHijo()
        {
            InitializeComponent();

            band = false;
            pagPri = this.CreateGraphics();
            p1 = new Point();
            p2 = new Point();
            pluma = new Pen(Color.Blue, 2);
            plumaAr = new Pen(Color.Red, 6);
            size = new Size(50, 50);
            rt = new Rectangle();
            pagina = new Bitmap(this.Width, this.Height);
            opc = 0;
            Grafo = new CGrafo();
            fuente = new Font("Arial", 15, FontStyle.Bold);
            radio = size.Width / 2;
            tipoGrafo = 1;
            nNodo = 0;
            bandGrafo = false;
        }

        public int getTipoGrafo() { return (tipoGrafo); }

        public int[][] entregaMatriz()
        {
            int[][] mat = null;

            switch (tipoGrafo)
            {
                case 2:
                    mat = GrafoNoDir.generaMatrizAdyacencia();
                break;
                case 3:
                    mat = GrafoDir.generaMatrizAdyacencia();
                break;
            }

            return (mat);
        }


        private void FormHijo_Paint(object sender, PaintEventArgs e)
        {
            Graphics graf;

            if (band == true)
            {
                pagPri = this.CreateGraphics();
                graf = Graphics.FromImage(pagina);
                graf.SmoothingMode = SmoothingMode.AntiAlias;
                graf.Clear(this.BackColor);


                switch (opc)
                {
                    case 1://Dibujar Nodo 
                        nNodo++;
                        (dameGrafo()).insVertice(Convert.ToString(nNodo), p1, radio, rt);
                        (dameGrafo()).incNodos();
                         DibujaGrafo(graf);
                    break;
                    case 2://Dibujar aristas no dirigida
                        DibujaGrafo(graf);
                        plumaAr = new Pen(Color.Red, 6);
                        plumaAr.EndCap = LineCap.Flat;
                        graf.DrawLine(plumaAr, p1, p2);
                    break;
                    case 3:
                        DibujaGrafo(graf);
                        plumaAr = new Pen(Color.Red,6);
                        plumaAr.EndCap = LineCap.ArrowAnchor;
                        graf.DrawLine(plumaAr, p1, p2);
                    break;
                    case 4://Mover Vertice
                    case 5://Mover grafo
                    case 6://Se ha eliminado un vertice
                    case 7://Se ha insertado una arista
                    case 8:
                    case 11:
                    case 12:
                    case 13://Dibujado del Grafo
                        DibujaGrafo(graf);
                    break;
                }
                
                pagPri.DrawImage(pagina,0,0);
                graf.Dispose();
                band = false;
            }

        }

        private CGrafo dameGrafo()
        {
            CGrafo gAux = null;

            switch (tipoGrafo)
            {
                case 1:
                    gAux = Grafo;
                    break;
                case 2:
                    gAux = GrafoNoDir;
                    break;
                case 3:
                    gAux = GrafoDir;
                    break;
            }

            return (gAux);
        }

        private void btNodo_Click(object sender, EventArgs e)
        {
            opc = 1;
            float res = ((2 + 1) % 5);
        }

        private void FormHijo_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = e.Location;
            Color color;

            if (e.Button == MouseButtons.Left)
            {
                switch (opc)
                {
                    case 1:
                        rt = new Rectangle(new Point(e.X - size.Width / 2, e.Y - size.Height / 2), size);
                        band = true;
                        FormHijo_Paint(this, null);
                        break;
                    case 2:
                    case 3:
                        seleccionarVert();
                        vertOrigen = verSelec;
                        break;
                    case 4:
                    case 5:
                    case 6:
                        seleccionarVert(); // Se busca si se selecciono algun vertice

                        if (verSelec != null && opc == 6)// Esta condicion es para checar si se va a eliminar algun vertice
                        {
                            switch (tipoGrafo)
                            {
                                case 1:
                                case 3:
                                    (dameGrafo()).eliminaVertice(verSelec);
                                    (dameGrafo()).decNodos();
                                break;
                                case 2:
                                    GrafoNoDir.eliminaVertice(verSelec);
                                    GrafoNoDir.decNodos();
                                break;
                            }

                            band = true;
                            FormHijo_Paint(this, null);
                        }
                   break;
                   case 8:
                         color = pagina.GetPixel(e.X, e.Y);

                         if (color.ToArgb() == Color.Black.ToArgb() || (color.ToArgb() == Color.Red.ToArgb()))
                         {
                             eliminaArista();
                             band = true;
                             FormHijo_Paint(this, null);
                         }
                   break;
                   case 13://Opcion para ponderar el Grafo
                       color = pagina.GetPixel(e.X, e.Y);

                       if ((color.ToArgb() == Color.Black.ToArgb()) || (color.ToArgb() == Color.Red.ToArgb()))
                       {
                           CNodoArista aristaAux = null;
                           FCapturaPesoArista dlgCapCstArista;
                           CNodoVertice vertAux = null;
                           int peso;

                           foreach (CNodoVertice nV in GrafoNoDir.getListVert())
                           {
                               vertAux = nV;
                               if ((aristaAux = SeleccionaArista(nV)) != null)
                                   break;
                           }

                           if (aristaAux != null)
                           {
                               dlgCapCstArista = new FCapturaPesoArista();
                               
                               dlgCapCstArista.posX = p1.X + this.Location.X-20;
                               dlgCapCstArista.posY = p1.Y + this.Location.Y+110;
                               dlgCapCstArista.tBcostoAr.Text = aristaAux.getPeso().ToString();

                               if (dlgCapCstArista.ShowDialog() == DialogResult.OK)
                               {
                                   peso = Convert.ToInt32(dlgCapCstArista.tBcostoAr.Text);
                                   aristaAux.setPeso(peso);
                                   GrafoNoDir.EstableceCostoArista(vertAux,peso,aristaAux);
                                   band = true;
                                   FormHijo_Paint(this, null);
                               }
                           }
                       }
                   break;
                   /*case 12:
                       color = pagina.GetPixel(e.X, e.Y);

                       if (color.ToArgb() == Color.Black.ToArgb())
                       {
                           eliminaArista();
                         /*  foreach (CNodoArista nA in vertDestino.getListaArista())
                               GrafoNoDir.insertaArista(vertOrigen, nA.getVertRel());

                           GrafoNoDir.eliminaVertice(vertDestino);
                           
                           GrafoNoDir.decNodos();

                       }

                       band = true;
                       FormHijo_Paint(this, null);
                   break;*/
                }
            }
            else
            {
                seleccionarVert();
                if (verSelec != null)
                {
                    string msg = " Nodo : " + verSelec.getNombre();

                    if (tipoGrafo == 3)
                        msg += "\n Grado Interno : " + verSelec.getGradoInt() + "\n Grado Externo : " + verSelec.getGradoExt();
                    else
                        msg += "\n Grado : " + verSelec.getGrado();

                    toolTip1.Show(msg, this, e.X, e.Y);
                }
                else
                {
                    int x,y;
                    x = this.Left;
                    y = this.Top;
                    menuContextoEdicion.Show(e.X+x+10, e.Y+y+120);
                }
            }
        }

        private CNodoArista SeleccionaArista(CNodoVertice nV)
        {
            PointF ptBezier;
            Rectangle r;
            int numPts;
            float dt;
           
            numPts = 100;
            dt = (float)1.0 / (numPts - 1);

            foreach (CNodoArista nA in nV.getListaArista())
                for (int i = 0; i < numPts; i++)
                {
                    ptBezier = PuntosBezier(nV.getPunto(), nA.getPoint3Ctrl(), nA.getPoint4Ctrl(), nA.getVertRel().getPunto(), i * dt);
                    r = new Rectangle((int)(ptBezier.X - 5), (int)(ptBezier.Y - 5), 10, 10);

                    if (r.IntersectsWith(new Rectangle(p1.X, p1.Y, 1, 1)) == true)
                          return (nA);
                }
        
           return(null);
        }

        private void eliminaArista()
        {
            CGrafo grafo = dameGrafo();
            CNodoArista nAux = null;

            foreach (CNodoVertice nV in grafo.getListVert())
                if ((nAux = SeleccionaArista(nV))!= null)
                {
                    switch (tipoGrafo)
                    {
                        case 2://No dirigido
                            GrafoNoDir.eliminaArista(nV, nAux.getVertRel(), nAux);
                            /*if(numCromatico == true)
                                GrafoNoDir.AsignarColores();*/
                        break;
                        case 3://Dirigido
                            GrafoDir.eliminaArista(nV, nAux.getVertRel(), nAux);
                         break;
                    }
                    break;
                }
        }
        
        private void seleccionarVert()
        {
            CGrafo grafo = dameGrafo();

            foreach (CNodoVertice v in grafo.getListVert())
                if (calculaDistancia(p1,v.getPunto()) <= radio)
                {
                    verSelec = v;
                    break;
                }
        }

        private float calculaDistancia(Point pA, Point pB)
        {
            Point p2 = pB;
            return (float)(Math.Sqrt(Math.Pow(Math.Abs(pA.X - p2.X), 2) + Math.Pow(Math.Abs(pA.Y - p2.Y), 2)));
        }

        private void DibujaGrafo(Graphics g)
        {
            CGrafo grafo = dameGrafo();

            foreach (CNodoVertice nV in grafo.getListVert())
            {
                foreach (CNodoArista nodoAux in nV.getListaArista())
                {
                    plumaAr = new Pen(nodoAux.getColor(), 6);
                    if (tipoGrafo == 2)
                    {
                        if (nodoAux.getVertRel().getPintado() != true)
                        {
                            DibujaCurvaBezier(g, nV.getPunto(), nodoAux.getPoint3Ctrl(), nodoAux.getPoint4Ctrl(), nodoAux.getVertRel().getPunto());
                          
                            if (grafoPonderado)
                                g.DrawString(nodoAux.getPeso().ToString(), fuente, Brushes.Green, ptsBezier[ptsBezier.Length / 2]);
                        }
                        nV.setPintado(true);
                    }
                    else
                    {
                        plumaAr.EndCap = LineCap.ArrowAnchor;
                        DibujaCurvaBezier(g, nV.getPunto(), nodoAux.getPoint3Ctrl(), nodoAux.getPoint4Ctrl(), nodoAux.getVertRel().getPunto());
                        
                        if (grafoPonderado)
                            g.DrawString(nodoAux.getPeso().ToString(), fuente, Brushes.Green, ptsBezier[ptsBezier.Length / 2]);
                    }
                }

                g.FillEllipse(new SolidBrush(Color.White), nV.getRect());
                g.DrawString(nV.getNombre(), fuente, Brushes.Brown, nV.getPunto().X - 8, nV.getPunto().Y - 9);
                //Dibujado del numero cromatico para grafos coloreadps
                if( nV.getColor() != 0) 
                   g.DrawString(nV.getColor().ToString(), fuente, Brushes.Orange, nV.getPunto().X - 30, nV.getPunto().Y - 40);
                
                g.DrawEllipse(new Pen( nV.getColorVert(),3), nV.getRect());
            }
              
           if(tipoGrafo == 2)
            foreach (CNodoVertice nV in grafo.getListVert())
                nV.setPintado(false);
        }

        private PointF PuntosBezier(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            float Ax, Ay, Bx, By, Cx, Cy, Dx, Dy, Px, Py;
            float nt = 1 - t;

            Ax = (float)(p0.X * Math.Pow(nt, 3));
            Bx = (float)(3 * p1.X * t * Math.Pow(nt, 2));
            Cx = (float)(3 * p2.X * t * t * nt);
            Dx = (float)(p3.X * Math.Pow(t, 3));

            Ay = (float)(p0.Y * Math.Pow(nt, 3));
            By = (float)(3 * p1.Y * t * Math.Pow(nt, 2));
            Cy = (float)(3 * p2.Y * t * t * nt);
            Dy = (float)(p3.Y * Math.Pow(t, 3));

            Px = Ax + Bx + Cx + Dx;
            Py = Ay + By + Cy + Dy;

            return (new PointF(Px, Py));
        }

        private void DibujaCurvaBezier(Graphics g, Point p0, PointF p1, PointF p2, Point p3)
        {
            PointF aux, aux2;
            int numPts;
            float dt;
            bool band = false;
            
            numPts = (int)calculaDistancia(p0, p3);//100;
            
            dt = (float)1.0 / (numPts - 1);

             ptsBezier = new PointF[numPts];

            for (int i = 0; i < numPts; i++)
                ptsBezier[i] = PuntosBezier(p0, p1, p2, p3, i * dt);

            aux = aux2 = ptsBezier[0];

            for (int i = 0; i < numPts; i++)
            {
                if (calculaDistancia(new Point( (int)ptsBezier[i].X, (int)ptsBezier[i].Y), p3)-1 <= radio)
                {
                  aux = ptsBezier[i];
                  break;
                }

                if(!band && (calculaDistancia(new Point( (int)ptsBezier[i].X, (int)ptsBezier[i].Y), p0) > radio))
                {
                    aux2 = ptsBezier[i];
                    band = true;
                }
            }
            
            g.DrawBezier(plumaAr, aux2, p1, p2, aux);
        }
     
        private void FormHijo_MouseMove(object sender, MouseEventArgs e)
        {
            etCoordenadas.Text = e.X.ToString() + ", " + e.Y.ToString() + " pixeles";
            Cursor = Cursors.Arrow;
          
           if (e.Button == MouseButtons.Left)
            {
                switch (opc)
                {
                    case 2:
                    case 3:
                        p2 = e.Location;
                        Cursor = Cursors.Cross;
                        band = true;
                        FormHijo_Paint(this, null);
                        break;
                    case 4:
                    case 5:
                        if (verSelec != null)
                        {
                            Cursor = Cursors.SizeAll;

                            if (opc == 4)
                                mueveVertice(e.Location);
                            else
                                mueveGrafo(e.Location);

                            band = true;
                            FormHijo_Paint(this, null);
                        }
                        break;

                }
            }
        }

        private void mueveVertice(Point pNew) 
        {
            Point centro = verSelec.getPunto();
            Point nuevo;
          
            nuevo = actualizaPto(centro, pNew);
            p1 = pNew;
            
            verSelec.setPunto(nuevo);
            dameGrafo().ActulizaPtsCtrl(verSelec);
            verSelec.setRect( new Rectangle(nuevo.X - (size.Width)/2, nuevo.Y- (size.Height)/2,size.Width,size.Height));
        }

        private void mueveGrafo(Point pNew)
        {
            Point nuevo;
            CGrafo grafo = dameGrafo();
            
            foreach (CNodoVertice v in grafo.getListVert())
            {
                nuevo = actualizaPto(v.getPunto(), pNew);
                v.setPunto(nuevo);
                v.setRect(new Rectangle(nuevo.X - (size.Width) / 2, nuevo.Y - (size.Height) / 2, size.Width, size.Height));
                grafo.ActulizaPtsCtrl(v);
            }
            
            p1 = pNew;
        }

        private Point actualizaPto(Point centro, Point e)
        {
            Point nuevo = new Point();

            nuevo.X = centro.X + (e.X - p1.X);
            nuevo.Y = centro.Y + (e.Y - p1.Y);

            return (nuevo);
        } 

        private void FormHijo_Resize(object sender, EventArgs e)
        {
            pagPri = this.CreateGraphics();
            pagina = new Bitmap(this.Width, this.Height);
            band = true;
            opc = 11;
            FormHijo_Paint(this, null);
        }

        private void btMueveNodo_Click(object sender, EventArgs e)
        {
            opc = 4;
        }

        private void FormHijo_MouseUp(object sender, MouseEventArgs e)
        {
            verSelec = null;
            toolTip1.Hide(this);

            if (opc == 2 || opc == 3)
            {
                Point p3 = p1;
                p1 = p2;
                float h = 0;
                seleccionarVert();
                vertDestino = verSelec;

                if (vertOrigen != null && vertDestino != null && calculaDistancia(p1,p3) > radio * 2)
                {
                    if (opc == 2)
                    {
                        if (bandGrafo == false)
                        {
                            tipoGrafo = 2;
                            GrafoNoDir = new CGrafoNoDirigido(Grafo, tipoGrafo);
                            DeshabilitaMetodosDir();
                        }
                    }
                    else
                    {
                        if (bandGrafo == false)
                        {
                            tipoGrafo = 3;
                            GrafoDir = new CGrafoDirigido(Grafo, tipoGrafo);
                            DeshabilitaMetodosNoDir();
                        }
                    }
                    
                    Grafo = null;
                    bandGrafo = true;

                    if (altura != 0)
                        foreach (CNodoArista aux in vertOrigen.getListaArista())
                            if (aux.getVO() == vertOrigen && aux.getVD() == vertDestino)
                                if (aux.getAltura() > h)
                                    h = aux.getAltura();

                    h += altura;
                    switch (tipoGrafo)
                    {
                        case 2:
                            vertOrigen.aumentaGrado();
                            vertDestino.aumentaGrado();
                            GrafoNoDir.insertaArista(vertOrigen, vertDestino,h,Color.Black);
                        break;
                        case 3:
                            vertOrigen.incGradoExt();
                            vertDestino.incGradoInt();
                           
                            GrafoDir.insertaArista(vertOrigen, vertDestino,h);
                            menuGrafosPlanos.Enabled = false;
                            grafosColoreadosToolStripMenuItem.Enabled = false;
                        break;
                    }
                }

                verSelec = vertOrigen = vertDestino = null;
                band = true;
                FormHijo_Paint(this, null);
            }
        }

        private void DeshabilitaMetodosDir()
        {
            OpcAristaDir.Enabled = false;
            dijkstraToolStripMenuItem.Enabled = false;
            menuAlgoFloyd.Enabled = false;
            menuAlgoFloyd.Enabled = false;
            AlgCompFuertes.Enabled = false;
            AlgoBosqueAbarcador.Enabled = false;
            menuWarshall.Enabled = false;
        }

        private void DeshabilitaMetodosNoDir()
        {
            OpcAristaNoDir.Enabled = false;
            AlgoPrim.Enabled = false;
            ALgoKruskal.Enabled = false;
            ptsDeArticulacion.Enabled = false;
            puntosDeArticulaciónToolStripMenuItem.Enabled = false;
            menuCircuitoEuler.Enabled = false;
            menuCaminoEuler.Enabled = false;
            ConectividadAB.Enabled = false;
            MenuCircuitoHamiltonToolStripMenuItem.Enabled = false;
            mCponderarGrafo.Enabled = false;
        }

        private void btMueveGrafo_Click(object sender, EventArgs e)
        {
            opc = 5;
        }

        private void btEliminaNodo_Click(object sender, EventArgs e)
        {
            opc = 6;
        }

        private void btEliminaGrafo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de elimimar el Grafo",
                 "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pagPri.Clear(Color.White);
                (dameGrafo()).eliminate();
                nNodo = 0;
            }
        }

        private void btAristaNoDir_Click(object sender, EventArgs e)
        {
            opc = 2;
        }

        private void btAristaDirigida_Click(object sender, EventArgs e)
        {
            opc = 3;
        }

        private void btEliminaArista_Click(object sender, EventArgs e)
        {
            opc = 8;
        }

        private void menuContextoEdicion_Opening(object sender, CancelEventArgs e)
        {
            if (dameGrafo().getNumNodos() == 0)
            {
                moverToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
                menuPropiedadesGrafo.Enabled = false;
            }
            else
            {
                moverToolStripMenuItem.Enabled = true;
                eliminarToolStripMenuItem.Enabled = true;
                menuPropiedadesGrafo.Enabled = true;
            }

        }

        private void menuPropiedadesGrafo_Click(object sender, EventArgs e)
        {
            PropiedadesGrafo dlgProGraf = new PropiedadesGrafo();

            string[] cad = new string[dameGrafo().getNumNodos()];
            int i = 0;

            foreach (CNodoVertice nV in dameGrafo().getListVert())
            {
                cad[i] = nV.getNombre();
                i++;
            }

            dlgProGraf.nameVert = cad;
            dlgProGraf.matriAdy = dameGrafo().generaMatrizAdyacencia();
            dlgProGraf.Text = dlgProGraf.Text + " - " + this.Text;
            dlgProGraf.tipoDeGrafo = tipoGrafo;
            dlgProGraf.etNoVertices.Text = dameGrafo().getNumNodos().ToString();
            
            dlgProGraf.ShowDialog();
        }

        private void OperCircuitoEuler_Click(object sender, EventArgs e)
        {
            bool res = false;

            switch (tipoGrafo)
            {
                case 2:
                   res = GrafoNoDir.GeneraCircuitoEuler();
                break;
                case 3:
                   res = GrafoDir.CircuitoEuleriano();
                break;
            }
            
            if (res == true)
            {
               MessageBox.Show("El grafo tiene un circuito Euleriano", "Circuito de Euler", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
             /*  CNodoVertice cir = GrafoNoDir.getPila().Pop();
               string cad = null;

               while (cir != null)
               {
                   cad += cir.getNombre() + "->";
                   cir = GrafoNoDir.getPila().Pop();
               }

               Graphics g = this.CreateGraphics();
               g.DrawString(cad,new Font("Arial",10, FontStyle.Italic), Brushes.AliceBlue, 100,300);
            }
            else
                MessageBox.Show("No exite un circuito Euleriano", "Circuito de Euler", MessageBoxButtons.OK, MessageBoxIcon.Error);
         */
            }
       }

        private void OperCaminoEuler_Click(object sender, EventArgs e)
        {
            bool res = false;

            switch (tipoGrafo)
            {
                case 2:
                    res = GrafoNoDir.CaminoEuleriano();
                break;
                case 3:
                    res = GrafoDir.CaminoEuleriano();
                break;
            }

            if (res == true)
              MessageBox.Show("Existe un camino Euleriano", "Camino Euler", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No existe un camino Euleriano", "Camino Euler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ConectividadAB_Click(object sender, EventArgs e)
        {
            FormConexo dlgConec = new FormConexo();

            foreach (CNodoVertice nV in dameGrafo().getListVert())
                nV.setVisita(false);
            
            dlgConec.setGrafo(dameGrafo());
            dlgConec.ShowDialog();
        }

        private void gPResCorolarios_Click(object sender, EventArgs e)
        {
            int numAristas = 0;
            CGrafo g = dameGrafo();
            bool band = false;
           
            //Se determina el numero de arista de algun grafo
            switch (tipoGrafo)
            {
                case 2:
                    numAristas = GrafoNoDir.dameGrado();
                break;
            }

            if (numAristas <= 3 * g.getNumNodos() - 6)
            {
                band = true;

                if (determinaCircuitos(g.generaMatrizAdyacencia()) == true)
                    if (numAristas <= 2 * g.getNumNodos() - 4)
                        band = true;
                    else
                        band = false;
            }

            if (band == true)
                MessageBox.Show(" El grafo es Plano", "Grafos planos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(" El grafo no es Plano", "Grafos Planos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool determinaCircuitos(int [][] mA )
        {
            bool res = false;
            int tam = mA[0].Length;
            int [,] matAux = new int[tam,tam];
            int[,] mB = new int[tam, tam];

            for (int i = 0; i < tam; i++)
                for (int j = 0; j < tam; j++)
                    matAux[i,j] = mA[i][j];

            int iter = 0;
            int acum;
            while (iter < 2)
            {
                for (int i = 0; i < 1; i++)
                   for (int j = 0; j < tam; j++)
                    {
                       acum = 0;
                       for(int z = 0; z < tam; z++)
                           acum += mA[i][z] * matAux[z, j];

                       mB[i, j] = acum;
                    }

                for (int i = 1; i < tam; i++)
                    mB[i, 0] = mB[0, i];
               
                for(int i = 0; i < tam; i++)
                    for(int j = 0; j < tam; j++)
                       matAux[i,j] = mB[i,j];
                
                iter++;
               
            }

            if (matAux[0,0] == 0)
                res = true;

            return (res);
        }

        private void GCNumCromatico_Click(object sender, EventArgs e)
        {
            switch (tipoGrafo)
                {
                    case 2://No dirigido
                        GrafoNoDir.AsignarColores();
                        numCromatico = true;
                        band = true;
                        opc = 11;
                        FormHijo_Paint(this, null);
                    break;
                }
        }

        private void btContraccion_Click(object sender, EventArgs e)//Opcion para realizar la contraccion en una arista
        {
            opc = 12;
        }

        private void AristaFormLine_Click(object sender, EventArgs e)
        {
            altura = 0.0F;
            opc = 2;
        }

        private void AristaFormSpline_Click(object sender, EventArgs e)
        {
            altura = 0.16F;
            opc = 2;
        }

        private void GCquitar_Click(object sender, EventArgs e)
        {
            numCromatico = false;

            foreach (CNodoVertice nV in dameGrafo().getListVert())
                nV.setColor(0);

            band = true;
            opc = 4;
            FormHijo_Paint(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form dlg = new Form();

            dlg.Show();
        }

        private void MenuCircuitoHamiltonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool res = true;

            foreach (CNodoVertice nV in dameGrafo().getListVert())
            {
                if (nV.getGrado() < dameGrafo().getNumNodos()/2)
                {
                    res = false;
                    break;
                }
            }

            if (res == false)
                MessageBox.Show("El grafo no cuenta con un Circuito Hamilton", "Circuito de Hamilton", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                caminoVert = new CPilaVertices();
                GrafoNoDir.CircuitoHamilton(GrafoNoDir.getListVert()[0], GrafoNoDir.getListVert()[0], caminoVert);
                caminoVert.Push(GrafoNoDir.getListVert()[0]);
                string cad = "";
                string cad2;
                CNodoVertice aux = null;
                while (caminoVert.getTope() != null)
                {
                    aux = caminoVert.Pop();
                    cad += aux.getNombre() +" -> ";
                }

                cad2 = cad.Substring(0, cad.Length - 4);
                

                toolTip2.Show(cad2, this, 300, 300,5000);

                foreach (CNodoVertice nV in GrafoNoDir.getListVert())
                    nV.setVisita(false);
                
            }
        }

        private void AristaDirLineal_Click(object sender, EventArgs e)
        {
            opc = 3;
            altura = 0.0F;
        }

        private void AristaDirSpline_Click(object sender, EventArgs e)
        {
            altura = 0.16F;
            opc = 3;
        }

        private void dijkstraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            band = true;
            opc = 13;
            opcDijkstra = true;
            groupBox1.Text = "Algoritmo de Dijkstra";
            btMatrizCostos.Enabled = true;
            groupBox2.Text = "Longitud de caminos";
            Etiqueta.Text = "Longitud minima";
            etLongitud.Text = "";
            GrafoDir.EstableceColorNodo();
            grafoPonderado = true;
            FormHijo_Paint(this, null);
        }

        private void btMatrizCostos_Click(object sender, EventArgs e)
        {
            FormMatrizCostos dlgMC = new FormMatrizCostos();
            int[][] mAdy;

            dlgMC.gD = GrafoDir;

            if(dlgMC.ShowDialog() == DialogResult.OK )
            {
                mAdy = GrafoDir.generaMatrizAdyacencia();

                for (int i = 0; i < GrafoDir.getListVert().Count; i++)
                    for (int j = 0; j < GrafoDir.getListVert().Count; j++)
                        if (mAdy[i][j] > 0)
                            foreach (CNodoArista nA in GrafoDir.getListVert()[i].getListaArista())
                                if (nA.getVertRel() == GrafoDir.getListVert()[j])
                                {
                                    nA.setPeso(Convert.ToInt32(dlgMC.matrizDeCostos.Rows[i].Cells[j].Value));
                                    GrafoDir.setAtCosto(i, j, Convert.ToInt32(dlgMC.matrizDeCostos.Rows[i].Cells[j].Value));
                                }

                grafoPonderado = true;
                band = true;
                opc = 12;
                FormHijo_Paint(this, null);
            }
        }

        private void ejecutaMetodo_Click(object sender, EventArgs e)
        {
            cBOrigen.Items.Clear();
            cBDestino.Items.Clear();

            if (opcDijkstra)
            {
                GrafoDir.Dijkstra();
                cBOrigen.Items.Add(GrafoDir.getListVert()[0].getNombre());
                etLongitud.Text = "0";
            }
            else
                if (opcWarshall)
                {
                    GrafoDir.generaMatrizAdyacencia();
                    GrafoDir.Warshall();
                }
                else
                {
                    GrafoDir.crearMatrizCostos();
                    GrafoDir.Floyd();
                    etLongitud.Text = "0";
                }

            for (int i = 0; i < GrafoDir.getNumNodos(); i++)
            {
                if(!opcDijkstra)
                    cBOrigen.Items.Add(GrafoDir.getListVert()[i].getNombre());

                cBDestino.Items.Add(GrafoDir.getListVert()[i].getNombre());
            }
          
            cBOrigen.Text = GrafoDir.getListVert()[0].getNombre();
            cBDestino.Text = GrafoDir.getListVert()[0].getNombre();
            cBOrigen.SelectedIndex = 0;
        }

        private void cBDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vO, vD;
            
            vD = cBDestino.SelectedIndex;

            if (opcDijkstra)
                etLongitud.Text = GrafoDir.getAtLongitud(vD).ToString();
            else
            {
                 vO = cBOrigen.SelectedIndex;

                 if(opcWarshall)
                 {
                     etLongitud.Text = "Falso";

                     if (GrafoDir.getAtCerraduraTransitiva(vO, vD) == 1)
                         etLongitud.Text = "Verdadero";
                 
                 }else
                 {
                   etLongitud.Text = GrafoDir.getALong(vO, vD).ToString();
            
                   //Establecer colores de las Aristas
                   foreach (CNodoVertice nV in GrafoDir.getListVert())
                   foreach (CNodoArista nA in nV.getListaArista())
                      nA.setColor(Color.Black);

                   List<CNodoVertice> listaVert;

                   listaVert = GrafoDir.getListVert();
                   listaCamino = new List<CNodoVertice>();

                   camino(cBOrigen.SelectedIndex, cBDestino.SelectedIndex, listaVert, ref listaCamino);

                   vertOrigen = listaVert[cBOrigen.SelectedIndex];
                   vertDestino = listaVert[cBDestino.SelectedIndex];

                   numCam = listaCamino.Count - 1;

                   time.Start();
                }
            }
        }

        private void menuAlgoFloyd_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            band = true;
            opc = 13;
            opcWarshall = opcDijkstra = false;
            groupBox1.Text = "Algoritmo de Floyd";
            btMatrizCostos.Enabled = true;
            groupBox2.Text = "Longitud de caminos";
            Etiqueta.Text = "Longitud minima :";
            etLongitud.Text = "";
            GrafoDir.EstableceColorNodo();
            grafoPonderado = true;
            FormHijo_Paint(this, null);
        }

        private void DibujaCamino(CNodoVertice vO, CNodoVertice vD, Color color)
        {
            foreach (CNodoArista nA in vO.getListaArista())
                if (nA.getVertRel() == vD)
                {
                    nA.setColor(color);
                    break;
                }
        }

        private void camino(int i, int j, List<CNodoVertice> lV, ref List<CNodoVertice> lC)
        {
            int k;

            k = GrafoDir.getAtCamino(i, j);

            if (k != 0)
            {
                camino(k-1, j, lV, ref lC);
                lC.Add(lV[k-1]);
                camino(i, k-1, lV, ref lC);
            }
        }

        private void time_Tick_1(object sender, EventArgs e)
        {
            Console.Beep();

            if (numCam >= 0)
            {
                DibujaCamino(vertOrigen, listaCamino[numCam], Color.Red);
                vertOrigen = listaCamino[numCam];
                numCam--;
            }
            else
            {
                DibujaCamino(vertOrigen, vertDestino, Color.Red);
                time.Stop();
            }

            band = true;
            opc = 13;
            FormHijo_Paint(this, null);
        }

        private void menuWarshall_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            band = true;
            opc = 13;
            opcDijkstra = false;
            opcWarshall = true;
            groupBox1.Text = "Algoritmo de Warshall";
            btMatrizCostos.Enabled = false;
            groupBox2.Text = "Caminos";
            Etiqueta.Text = "Existe Camino ? : ";
            etLongitud.Text = "";
            GrafoDir.EstableceColorNodo();
            FormHijo_Paint(this, null);
        }

        private void cBOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cBDestino.SelectedIndex = 0;
        }

        private void AlgCompFuertes_Click(object sender, EventArgs e)
        {
            List<List<CNodoVertice>> listaCompFuertes;
            Color colorAux;

            groupBox1.Visible = false;
            grafoPonderado = false;

            listaCompFuertes = GrafoDir.generaCompFuertes();

            for (int i = 0; i < listaCompFuertes.Count; i++)
            {
                colorAux = dameColor(i);
                foreach (CNodoVertice nV in listaCompFuertes[i])
                    foreach (CNodoVertice vert in GrafoDir.getListVert())
                        if (nV.getNombre().CompareTo(vert.getNombre()) == 0)
                        {
                            vert.setColorVert(colorAux);
                            break;
                        }
            }

            band = true;
            opc = 13;
            FormHijo_Paint(this, null);
        }

        private Color dameColor(int pos)
        {
            Color[] colores = new Color[10];

            colores[0] = Color.Brown;
            colores[1] = Color.Green;
            colores[2] = Color.Yellow;
            colores[3] = Color.Tomato;
            colores[4] = Color.Red;
            colores[5] = Color.Purple;
            colores[6] = Color.Violet;
            colores[7] = Color.Orange;

            return (colores[pos]);
        }

        private void AlgoBosqueAbarcador_Click(object sender, EventArgs e)
        {
            BosqueAbarcador dlgBA = new BosqueAbarcador();
            List<CNodoArista>[] lA;

            groupBox1.Visible = false;
            grafoPonderado = false;
            GrafoDir.ActualizaColorArista();
            band = true;
            FormHijo_Paint(this,null);

            GrafoDir.generaArcos();

            if (GrafoDir.GetListaDeArcos()[2].Count == 0)
                dlgBA.grafoAciclico = true;
           
            dlgBA.gD = GrafoDir;
            dlgBA.opc = 1;

            dlgBA.ShowDialog();
        }
        private void mCponderarGrafo_Click(object sender, EventArgs e)
        {
            opc = 13;
            grafoPonderado = true;
            band = true;
            FormHijo_Paint(this, null);
        }

        private void AlgoPrim_Click(object sender, EventArgs e)
        {
            GrafoNoDir.EstableceColorNodo();
            if (ChecaConectividad())
            {
                DibujaArbol(GrafoNoDir.Prim());
                MessageBox.Show(" Costo Mínimo : " + GrafoNoDir.getCostoMinimo(), "Costo mínimo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("El Grafo tiene que estar conectado para poder" +
                                "generar el Árbol Abarcador de Costo Mísnimo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ChecaConectividad()
        {
            bool band = true;
            CNodoVertice firstNodo;
            List<CNodoVertice> V;

            V = GrafoNoDir.getListVert();
            firstNodo = V[0];

            for (int i = 1; i < V.Count; i++)
            {
                foreach (CNodoVertice vert in V)
                    vert.setVisita(false);

                if (!(band = GrafoNoDir.ConectividadAB(firstNodo, V[i])))
                    break;
            }

            foreach (CNodoVertice vert in V)
                vert.setVisita(false);

            return (band);
        }


        private void ALgoKruskal_Click(object sender, EventArgs e)
        {
            GrafoNoDir.EstableceColorNodo();
            if (ChecaConectividad())
            {
                DibujaArbol(GrafoNoDir.Kruskal());
                MessageBox.Show(" Costo Mínimo : " + GrafoNoDir.getCostoMinimo(), "Costo mínimo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("El Grafo tiene que estar conectado para poder " +
                                "generar el Árbol Abarcador de Costo Mínimo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DibujaArbol( List<CNodoArista> T )
        {
            CNodoVertice vO = null;
            bool b;

            GrafoNoDir.ActualizaColorArista();

            foreach (CNodoArista nA in T)
            {
                b = false;
                foreach (CNodoVertice nV in GrafoNoDir.getListVert())
                {
                    foreach (CNodoArista a in nV.getListaArista())
                        if (a.getPoint3Ctrl() == nA.getPoint3Ctrl() && a.getPoint4Ctrl() == nA.getPoint4Ctrl())
                        {
                            b = true;
                            break;
                        }
                    if (b == true)
                    {
                        vO = nV;
                        break;
                    }
                }

                nA.setColor(Color.Red);
                GrafoNoDir.EstableceColorArista(vO, Color.Red, nA);
            }

            opc = 13;
            band = true;
            FormHijo_Paint(this, null);
        }

        private void AlgRecBP_Click(object sender, EventArgs e)
        {
            BosqueAbarcador dlgBA = new BosqueAbarcador();
            GrafoNoDir.RecorreGrafo();
            dlgBA.gNDir = GrafoNoDir;
            dlgBA.opc = 2;
            dlgBA.ShowDialog();
        }

        private void busquedaEnAmplitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BosqueAbarcador dlgBA = new BosqueAbarcador();
            GrafoNoDir.BusquedaEnApmlitud();
            dlgBA.gNDir = GrafoNoDir;
            dlgBA.opc = 2;
            dlgBA.ShowDialog();
        }

        private void ptsDeArticulacion_Click(object sender, EventArgs e)
        {
            grafoPonderado = false;
            GrafoNoDir.EstableceColorNodo();
            GrafoNoDir.ActualizaColorArista();

            if (ChecaConectividad())
            {
                GrafoNoDir.BuscaPuntosDeArticulacion();

                if (GrafoNoDir.getLisPtsArt().Count != 0)
                {
                    foreach (CNodoVertice v in GrafoNoDir.getLisPtsArt())
                        v.setColorVert(Color.Red);

                    band = true;
                    FormHijo_Paint(this, null);
                }
                else
                    MessageBox.Show(" El Grafo es BICONEXO.\n Ya que NO cuenta con puntos de Articulación","Grago Biconexo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }else
                MessageBox.Show("El Grafo debe estar conectado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}











