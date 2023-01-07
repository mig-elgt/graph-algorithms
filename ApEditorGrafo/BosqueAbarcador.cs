using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ApEditorGrafo
{
    public partial class BosqueAbarcador : Form
    {
        internal CGrafoDirigido gD;
        internal CGrafoNoDirigido gNDir;
        private CGrafoDirigido bosqueAbarcador;
        private CGrafoNoDirigido bA;
        private Point p1;
        private Point p2;
        private bool band;
        private Graphics pagPri;
        private Pen pluma;
        private Pen plumaAr;
        private Size size;
        private Bitmap pagina;
        private Font fuente;
        private int radio;
        private CNodoVertice verSelec;
        private CNodoVertice vertOrigen;
        private CNodoVertice vertDestino;
        private PointF[] ptsBezier;
        private bool bandGrafo;
        internal int opc;
        internal bool grafoAciclico;

        public BosqueAbarcador()
        {
            InitializeComponent();
            band = false;
            pagPri = this.CreateGraphics();
            p1 = new Point();
            p2 = new Point();
            pluma = new Pen(Color.Blue, 2);
            plumaAr = new Pen(Color.Red, 6);
            size = new Size(50, 50);
            pagina = new Bitmap(this.Width, this.Height);
            fuente = new Font("Arial", 15, FontStyle.Bold);
            radio = size.Width / 2;
            grafoAciclico = false;
        }

        private void BosqueAbarcador_Load(object sender, EventArgs e)
        {
           
            switch(opc)
            {
                case 1:
                    IniBosqueAbarcador();
                    if (grafoAciclico)
                        this.Text = this.Text + " -> GRAFO ACÍCLICO";
                break;
                case 2:
                    CreaBosqueAbarcado();
                break;
            }

            bandGrafo = true;
        }

        private void CreaBosqueAbarcado()
        {
            int x, y, dX;

            bA = new CGrafoNoDirigido();
            x = this.Width / 2;
            y = 100;
            dX = 120;
            
            foreach (CNodoVertice nV in gNDir.getRaizes())
            {
                creaVertices(nV, x, y,dX);
                x += 200;
            }

            InsArista();
        }

        private void IniBosqueAbarcador()
        {
            bosqueAbarcador = new CGrafoDirigido();
            int x, y, dX;

            x = this.Width/2;
            y = 100;
            dX = 100;

            foreach (CNodoVertice nV in gD.getRaizes())
            {
                creaVertices(nV, x, y, dX);
                x += 200;
            }

            insertaAristas();
        }

        private void creaVertices(CNodoVertice vert, int x, int y, int dX)
        {
            Rectangle rt;
            Point pt = new Point(x, y);
            int arcos;
            List<int> lCor = new List<int>();
            int cont = 0;
            int dX2 = dX;

            rt = new Rectangle(new Point(x - size.Width / 2, y - size.Height / 2), size);
            

            vert.setVisita(false);

            switch (opc)
            { 
                case 1:
                    bosqueAbarcador.insVertice(vert.getNombre(), pt, radio, rt);
                break;
                case 2:
                    bA.insVertice(vert.getNombre(), pt, radio,rt);
                break;
            }

            arcos = numArcosArbol(vert);
            generaCoordenadas(ref lCor, dX, x, arcos);

            foreach (CNodoArista nA in vert.getListaArista())
            {
                if (nA.getTipoArco() == 1 && (nA.getVertRel().getVisita() == true))
                {
                    creaVertices(nA.getVertRel(), lCor[cont], y + 100, dX-=30);
                    cont++;
                    dX += 30;
                }
            }
        }

        private void generaCoordenadas(ref List<int> lC, int dX, int xO, int nA)
        {
            int xP = (xO - nA/2 * dX);

            for (int i = 0; i < nA; i++)
            {
                lC.Add(xP);
                xP += dX;

                if (xP == xO && (nA % 2 == 0))
                    xP += dX;
            }
        }

        private int numArcosArbol(CNodoVertice v)
        {
            int cont = 0;

            foreach (CNodoArista nA in v.getListaArista())
                if (nA.getTipoArco() == 1 && (nA.getVertRel().getVisita() == true))
                    cont++;

            return (cont);
        }

        private void insertaAristas()
        {
            CNodoVertice vO, vD;
            CNodoArista arista;
            float h;

            foreach (CNodoVertice nV in dameGra().getListVert())
                foreach (CNodoArista nA in nV.getListaArista())
                {
                    if (nA.getTipoArco() == 1)
                        h = 0;
                    else
                        h = 0.20F;

                    vO = buscaVertice(nV);
                    vD = buscaVertice(nA.getVertRel());
              
                    bosqueAbarcador.insertaArista(vO, vD, h);
                    
                    arista = dameArista(vO, vD);
                    arista.setTipoArco(nA.getTipoArco());
                }
        }

        private void InsArista()
        {
            CNodoVertice vO, vD;
            CNodoArista arista;
            float h;

            foreach (CNodoVertice nV in dameGra().getListVert())
                foreach (CNodoArista nA in nV.getListaArista())
                {
                    vO = buscaVertice(nV);
                    vD = buscaVertice(nA.getVertRel());

                    if (nA.getTipoArco() == 1)
                        h = 0;
                    else
                        h = 0.20F;

                    if (ExisteArista(vO, vD) == false)
                    {
                        bA.insertaArista(vO, vD, h, Color.Black);

                        arista = dameArista(vO, vD);
                        arista.setTipoArco(nA.getTipoArco());
                        arista = dameArista(vD, vO);
                        arista.setTipoArco(nA.getTipoArco());
                    }
                }   
        }

        private bool ExisteArista(CNodoVertice vO, CNodoVertice vD)
        {
            foreach (CNodoArista nA in vO.getListaArista())
                if (nA.getVertRel() == vD)
                    return (true);

            return (false);
        }

        private CGrafo dameGra()
        {
            CGrafo g = null;

            switch (opc)
            { 
                case 1:
                    g = gD;
                break;
                case 2:
                    g = gNDir;
                break;
            }

            return (g);
        }

        private CNodoArista dameArista(CNodoVertice vO, CNodoVertice vD)
        {
            foreach (CNodoVertice nV in getGrafo().getListVert())
                if (nV == vO)
                    foreach (CNodoArista nA in nV.getListaArista())
                        if (nA.getVertRel() == vD)
                            return (nA);

            return (null);
        }

        private CNodoVertice buscaVertice(CNodoVertice aux)
        {
            foreach (CNodoVertice nV in getGrafo().getListVert())
                if (nV.getNombre().CompareTo(aux.getNombre()) == 0)
                    return (nV);

            return (null);
        }

        private void BosqueAbarcador_Paint(object sender, PaintEventArgs e)
        {
            Graphics graf;

            if (bandGrafo == true)
            {
                graf = Graphics.FromImage(pagina);
                graf.SmoothingMode = SmoothingMode.AntiAlias;
                graf.Clear(this.BackColor);
                DibujaBosque(graf);
                pagPri.DrawImage(pagina, 0, 0);
                graf.Dispose();
                band = false;
            }
        }

        private void DibujaBosque(Graphics g)
        {
            foreach (CNodoVertice nV in getGrafo().getListVert())
            {
                foreach (CNodoArista nodoAux in nV.getListaArista())
                {
                    switch (nodoAux.getTipoArco())
                    {
                        case 1://Arco de Árbol
                            plumaAr = new Pen(Color.Red, 5);
                            plumaAr.DashStyle = DashStyle.Solid;
                            break;
                        case 2://Arcos de Avance
                            plumaAr = new Pen(Color.Orange, 5);
                            plumaAr.DashStyle = DashStyle.DashDot;
                            break;
                        case 3://Arcos de Retroceso
                            plumaAr = new Pen(Color.Green, 5);
                            plumaAr.DashStyle = DashStyle.Dash;
                            break;
                        case 4: //Arcos cruzados
                            plumaAr = new Pen(Color.Gray, 5);
                            plumaAr.DashStyle = DashStyle.Dot;
                            break;
                    }

                    if (opc == 2)
                    {
                        if (nodoAux.getVertRel().getPintado() != true)
                          DibujaCurvaBezier(g, nV.getPunto(), nodoAux.getPoint3Ctrl(), nodoAux.getPoint4Ctrl(), nodoAux.getVertRel().getPunto());
                          
                        nV.setPintado(true);
                    }
                    else
                    {
                        plumaAr.EndCap = LineCap.ArrowAnchor;
                        DibujaCurvaBezier(g, nV.getPunto(), nodoAux.getPoint3Ctrl(), nodoAux.getPoint4Ctrl(), nodoAux.getVertRel().getPunto());
                    }
                }

                g.FillEllipse(new SolidBrush(Color.White), nV.getRect());
                g.DrawString(nV.getNombre(), fuente, Brushes.Black, nV.getPunto().X - 8, nV.getPunto().Y - 9);
                //Dibujado del numero cromatico para grafos coloreadps
                if (nV.getColor() != 0)
                    g.DrawString(nV.getColor().ToString(), fuente, Brushes.Orange, nV.getPunto().X - 19, nV.getPunto().Y - 15);

                g.DrawEllipse(new Pen(nV.getColorVert(), 3), nV.getRect());
            }

            if (opc == 2)
                foreach (CNodoVertice nV in getGrafo().getListVert())
                    nV.setPintado(false);
        }

        private CGrafo getGrafo()
        {
            CGrafo gAux = null;

            switch (opc)
            {
                case 1:
                    gAux = bosqueAbarcador;
                break;
                case 2:
                    gAux = bA;
                break;
            }
            
            return (gAux);
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

            numPts = (int)calculaDistancia(p0, p3);

            dt = (float)1.0 / (numPts - 1);

            ptsBezier = new PointF[numPts];

            for (int i = 0; i < numPts; i++)
                ptsBezier[i] = PuntosBezier(p0, p1, p2, p3, i * dt);

            aux = aux2 = ptsBezier[0];

            for (int i = 0; i < numPts; i++)
            {
                if (calculaDistancia(new Point((int)ptsBezier[i].X, (int)ptsBezier[i].Y), p3) - 1 <= radio)
                {
                    aux = ptsBezier[i];
                    break;
                }

                if (!band && (calculaDistancia(new Point((int)ptsBezier[i].X, (int)ptsBezier[i].Y), p0) > radio))
                {
                    aux2 = ptsBezier[i];
                    band = true;
                }
            }

            g.DrawBezier(plumaAr, aux2, p1, p2, aux);
        }

        private float calculaDistancia(Point pA, Point pB)
        {
            Point p2 = pB;
            return (float)(Math.Sqrt(Math.Pow(Math.Abs(pA.X - p2.X), 2) + Math.Pow(Math.Abs(pA.Y - p2.Y), 2)));
        }

        private void BosqueAbarcador_Resize(object sender, EventArgs e)
        {
            pagPri = this.CreateGraphics();
            pagina = new Bitmap(this.Width, this.Height);
            bandGrafo = true;
            BosqueAbarcador_Paint(this, null);
        }

        private void seleccionarVert()
        {
            foreach (CNodoVertice v in getGrafo().getListVert())
                if (calculaDistancia(p1, v.getPunto()) <= radio)
                {
                    verSelec = v;
                    break;
                }
        }

        private void BosqueAbarcador_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = e.Location;

            if (e.Button == MouseButtons.Left)
                seleccionarVert(); // Se busca si se selecciono algun vertice
        }

        private void BosqueAbarcador_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;

            if (e.Button == MouseButtons.Left)
                if (verSelec != null)
                {
                    Cursor = Cursors.SizeAll;
                    mueveVertice(e.Location);
                    bandGrafo = true;
                    BosqueAbarcador_Paint(this, null);
                }
        }

        private void mueveVertice(Point pNew)
        {
            Point centro = verSelec.getPunto();
            Point nuevo;

            nuevo = actualizaPto(centro, pNew);
            p1 = pNew;

            verSelec.setPunto(nuevo);
            getGrafo().ActulizaPtsCtrl(verSelec);
            verSelec.setRect(new Rectangle(nuevo.X - (size.Width) / 2, nuevo.Y - (size.Height) / 2, size.Width, size.Height));
        }

        private Point actualizaPto(Point centro, Point e)
        {
            Point nuevo = new Point();

            nuevo.X = centro.X + (e.X - p1.X);
            nuevo.Y = centro.Y + (e.Y - p1.Y);

            return (nuevo);
        }

        private void BosqueAbarcador_MouseUp(object sender, MouseEventArgs e)
        {
            verSelec =  null;
        }

        private void BosqueAbarcador_Resize_1(object sender, EventArgs e)
        {
            pagPri = this.CreateGraphics();
            pagina = new Bitmap(this.Width, this.Height);
            bandGrafo = true;
            BosqueAbarcador_Paint(this, null);
        } 
    } 
}
   








