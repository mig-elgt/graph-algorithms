namespace ApEditorGrafo
{
    partial class FormPadre
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPadre));
            this.BarraMenusFormPadre = new System.Windows.Forms.MenuStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchivoNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchivoCerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOperaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.OperaIsomorfismo = new System.Windows.Forms.ToolStripMenuItem();
            this.ventanaVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.VentanaCascada = new System.Windows.Forms.ToolStripMenuItem();
            this.VentanaHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.VentanaVert = new System.Windows.Forms.ToolStripMenuItem();
            this.BarraHerramientasFormPadre = new System.Windows.Forms.ToolStrip();
            this.btNuevo = new System.Windows.Forms.ToolStripButton();
            this.btAbrir = new System.Windows.Forms.ToolStripButton();
            this.btGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.BarraMenusFormPadre.SuspendLayout();
            this.BarraHerramientasFormPadre.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarraMenusFormPadre
            // 
            this.BarraMenusFormPadre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BarraMenusFormPadre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArchivo,
            this.menuOperaciones,
            this.ventanaVertical});
            this.BarraMenusFormPadre.Location = new System.Drawing.Point(0, 0);
            this.BarraMenusFormPadre.MdiWindowListItem = this.ventanaVertical;
            this.BarraMenusFormPadre.Name = "BarraMenusFormPadre";
            this.BarraMenusFormPadre.Size = new System.Drawing.Size(860, 24);
            this.BarraMenusFormPadre.TabIndex = 1;
            this.BarraMenusFormPadre.Text = "menuStrip1";
            // 
            // menuArchivo
            // 
            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ArchivoNuevo,
            this.ArchivoCerrar});
            this.menuArchivo.MergeIndex = 0;
            this.menuArchivo.Name = "menuArchivo";
            this.menuArchivo.Size = new System.Drawing.Size(60, 20);
            this.menuArchivo.Text = "&Archivo";
            // 
            // ArchivoNuevo
            // 
            this.ArchivoNuevo.MergeIndex = 0;
            this.ArchivoNuevo.Name = "ArchivoNuevo";
            this.ArchivoNuevo.Size = new System.Drawing.Size(109, 22);
            this.ArchivoNuevo.Text = "&Nuevo";
            this.ArchivoNuevo.Click += new System.EventHandler(this.ArchivoNuevo_Click);
            // 
            // ArchivoCerrar
            // 
            this.ArchivoCerrar.MergeIndex = 1;
            this.ArchivoCerrar.Name = "ArchivoCerrar";
            this.ArchivoCerrar.Size = new System.Drawing.Size(109, 22);
            this.ArchivoCerrar.Text = "&Cerrar";
            this.ArchivoCerrar.Click += new System.EventHandler(this.ArchivoCerrar_Click);
            // 
            // menuOperaciones
            // 
            this.menuOperaciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OperaIsomorfismo});
            this.menuOperaciones.MergeIndex = 1;
            this.menuOperaciones.Name = "menuOperaciones";
            this.menuOperaciones.Size = new System.Drawing.Size(85, 20);
            this.menuOperaciones.Text = "&Operaciones";
            // 
            // OperaIsomorfismo
            // 
            this.OperaIsomorfismo.MergeIndex = 1;
            this.OperaIsomorfismo.Name = "OperaIsomorfismo";
            this.OperaIsomorfismo.Size = new System.Drawing.Size(141, 22);
            this.OperaIsomorfismo.Text = "&Isomorfismo";
            this.OperaIsomorfismo.Click += new System.EventHandler(this.OperaIsomorfismo_Click);
            // 
            // ventanaVertical
            // 
            this.ventanaVertical.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VentanaCascada,
            this.VentanaHorizontal,
            this.VentanaVert});
            this.ventanaVertical.MergeIndex = 2;
            this.ventanaVertical.Name = "ventanaVertical";
            this.ventanaVertical.Size = new System.Drawing.Size(62, 20);
            this.ventanaVertical.Text = "&Ventana";
            // 
            // VentanaCascada
            // 
            this.VentanaCascada.Name = "VentanaCascada";
            this.VentanaCascada.Size = new System.Drawing.Size(129, 22);
            this.VentanaCascada.Text = "&Cascada";
            this.VentanaCascada.Click += new System.EventHandler(this.VentanaCascada_Click);
            // 
            // VentanaHorizontal
            // 
            this.VentanaHorizontal.Name = "VentanaHorizontal";
            this.VentanaHorizontal.Size = new System.Drawing.Size(129, 22);
            this.VentanaHorizontal.Text = "&Horizontal";
            this.VentanaHorizontal.Click += new System.EventHandler(this.VentanaHorizontal_Click);
            // 
            // VentanaVert
            // 
            this.VentanaVert.Name = "VentanaVert";
            this.VentanaVert.Size = new System.Drawing.Size(129, 22);
            this.VentanaVert.Text = "&Vertical";
            this.VentanaVert.Click += new System.EventHandler(this.VentanaVert_Click);
            // 
            // BarraHerramientasFormPadre
            // 
            this.BarraHerramientasFormPadre.BackColor = System.Drawing.Color.Silver;
            this.BarraHerramientasFormPadre.ImageScalingSize = new System.Drawing.Size(35, 35);
            this.BarraHerramientasFormPadre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNuevo,
            this.btAbrir,
            this.btGuardar,
            this.toolStripSeparator});
            this.BarraHerramientasFormPadre.Location = new System.Drawing.Point(0, 24);
            this.BarraHerramientasFormPadre.Name = "BarraHerramientasFormPadre";
            this.BarraHerramientasFormPadre.Size = new System.Drawing.Size(860, 42);
            this.BarraHerramientasFormPadre.TabIndex = 3;
            this.BarraHerramientasFormPadre.Text = "toolStrip1";
            // 
            // btNuevo
            // 
            this.btNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btNuevo.Image")));
            this.btNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNuevo.MergeIndex = 0;
            this.btNuevo.Name = "btNuevo";
            this.btNuevo.Size = new System.Drawing.Size(39, 39);
            this.btNuevo.Text = "&Nuevo";
            this.btNuevo.Click += new System.EventHandler(this.ArchivoNuevo_Click);
            // 
            // btAbrir
            // 
            this.btAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btAbrir.Image")));
            this.btAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAbrir.MergeIndex = 1;
            this.btAbrir.Name = "btAbrir";
            this.btAbrir.Size = new System.Drawing.Size(39, 39);
            this.btAbrir.Text = "&Abrir";
            // 
            // btGuardar
            // 
            this.btGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btGuardar.Image")));
            this.btGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btGuardar.MergeIndex = 2;
            this.btGuardar.Name = "btGuardar";
            this.btGuardar.Size = new System.Drawing.Size(39, 39);
            this.btGuardar.Text = "&Guardar";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.MergeIndex = 3;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 42);
            // 
            // FormPadre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 488);
            this.Controls.Add(this.BarraHerramientasFormPadre);
            this.Controls.Add(this.BarraMenusFormPadre);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.BarraMenusFormPadre;
            this.Name = "FormPadre";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MdiChildActivate += new System.EventHandler(this.FormPadre_MdiChildActivate);
            this.BarraMenusFormPadre.ResumeLayout(false);
            this.BarraMenusFormPadre.PerformLayout();
            this.BarraHerramientasFormPadre.ResumeLayout(false);
            this.BarraHerramientasFormPadre.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip BarraMenusFormPadre;
        private System.Windows.Forms.ToolStripMenuItem menuArchivo;
        private System.Windows.Forms.ToolStripMenuItem ArchivoNuevo;
        private System.Windows.Forms.ToolStripMenuItem ArchivoCerrar;
        private System.Windows.Forms.ToolStripMenuItem ventanaVertical;
        private System.Windows.Forms.ToolStripMenuItem VentanaCascada;
        private System.Windows.Forms.ToolStripMenuItem VentanaHorizontal;
        private System.Windows.Forms.ToolStripMenuItem VentanaVert;
        private System.Windows.Forms.ToolStripButton btNuevo;
        private System.Windows.Forms.ToolStripButton btAbrir;
        private System.Windows.Forms.ToolStripButton btGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        internal System.Windows.Forms.ToolStrip BarraHerramientasFormPadre;
        private System.Windows.Forms.ToolStripMenuItem menuOperaciones;
        private System.Windows.Forms.ToolStripMenuItem OperaIsomorfismo;
    }
}

