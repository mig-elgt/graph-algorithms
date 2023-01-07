namespace ApEditorGrafo
{
    partial class BosqueAbarcador
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
            this.SuspendLayout();
            // 
            // BosqueAbarcador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(664, 676);
            this.Name = "BosqueAbarcador";
            this.Text = "Bosque Abarcador";
            this.Load += new System.EventHandler(this.BosqueAbarcador_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BosqueAbarcador_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BosqueAbarcador_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BosqueAbarcador_MouseDown);
            this.Resize += new System.EventHandler(this.BosqueAbarcador_Resize_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BosqueAbarcador_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}