namespace ApEditorGrafo
{
    partial class FormMatrizCostos
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
            this.matrizDeCostos = new System.Windows.Forms.DataGridView();
            this.aceptar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.matrizDeCostos)).BeginInit();
            this.SuspendLayout();
            // 
            // matrizDeCostos
            // 
            this.matrizDeCostos.AllowUserToAddRows = false;
            this.matrizDeCostos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrizDeCostos.Location = new System.Drawing.Point(21, 21);
            this.matrizDeCostos.Name = "matrizDeCostos";
            this.matrizDeCostos.Size = new System.Drawing.Size(344, 282);
            this.matrizDeCostos.TabIndex = 0;
            // 
            // aceptar
            // 
            this.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.aceptar.Location = new System.Drawing.Point(260, 322);
            this.aceptar.Name = "aceptar";
            this.aceptar.Size = new System.Drawing.Size(105, 34);
            this.aceptar.TabIndex = 1;
            this.aceptar.Text = "Aceptar";
            this.aceptar.UseVisualStyleBackColor = true;
            // 
            // Cancelar
            // 
            this.Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelar.Location = new System.Drawing.Point(149, 322);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(105, 34);
            this.Cancelar.TabIndex = 1;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            // 
            // FormMatrizCostos
            // 
            this.AcceptButton = this.aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancelar;
            this.ClientSize = new System.Drawing.Size(398, 369);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.aceptar);
            this.Controls.Add(this.matrizDeCostos);
            this.Name = "FormMatrizCostos";
            this.Text = "FormMatrizCostos";
            this.Load += new System.EventHandler(this.FormMatrizCostos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.matrizDeCostos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button aceptar;
        private System.Windows.Forms.Button Cancelar;
        internal System.Windows.Forms.DataGridView matrizDeCostos;
    }
}