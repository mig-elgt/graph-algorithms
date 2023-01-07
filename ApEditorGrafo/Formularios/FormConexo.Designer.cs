namespace ApEditorGrafo.Formularios
{
    partial class FormConexo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listVertices = new System.Windows.Forms.ListBox();
            this.vertB = new System.Windows.Forms.RadioButton();
            this.vertA = new System.Windows.Forms.RadioButton();
            this.vB = new System.Windows.Forms.Label();
            this.vA = new System.Windows.Forms.Label();
            this.btChecaConectividad = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listVertices);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 193);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Vertices";
            // 
            // listVertices
            // 
            this.listVertices.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listVertices.FormattingEnabled = true;
            this.listVertices.ItemHeight = 14;
            this.listVertices.Location = new System.Drawing.Point(15, 23);
            this.listVertices.Name = "listVertices";
            this.listVertices.Size = new System.Drawing.Size(97, 158);
            this.listVertices.TabIndex = 0;
            this.listVertices.SelectedIndexChanged += new System.EventHandler(this.listVertices_SelectedIndexChanged);
            // 
            // vertB
            // 
            this.vertB.AutoSize = true;
            this.vertB.Location = new System.Drawing.Point(180, 107);
            this.vertB.Name = "vertB";
            this.vertB.Size = new System.Drawing.Size(14, 13);
            this.vertB.TabIndex = 11;
            this.vertB.UseVisualStyleBackColor = true;
            // 
            // vertA
            // 
            this.vertA.AutoSize = true;
            this.vertA.Checked = true;
            this.vertA.Location = new System.Drawing.Point(180, 66);
            this.vertA.Name = "vertA";
            this.vertA.Size = new System.Drawing.Size(14, 13);
            this.vertA.TabIndex = 12;
            this.vertA.TabStop = true;
            this.vertA.UseVisualStyleBackColor = true;
            // 
            // vB
            // 
            this.vB.BackColor = System.Drawing.Color.White;
            this.vB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vB.Location = new System.Drawing.Point(200, 101);
            this.vB.Name = "vB";
            this.vB.Size = new System.Drawing.Size(60, 23);
            this.vB.TabIndex = 9;
            this.vB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vA
            // 
            this.vA.BackColor = System.Drawing.Color.White;
            this.vA.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vA.Location = new System.Drawing.Point(200, 60);
            this.vA.Name = "vA";
            this.vA.Size = new System.Drawing.Size(60, 23);
            this.vA.TabIndex = 10;
            this.vA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btChecaConectividad
            // 
            this.btChecaConectividad.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChecaConectividad.Location = new System.Drawing.Point(170, 148);
            this.btChecaConectividad.Name = "btChecaConectividad";
            this.btChecaConectividad.Size = new System.Drawing.Size(109, 45);
            this.btChecaConectividad.TabIndex = 8;
            this.btChecaConectividad.Text = "Checar Conectividad";
            this.btChecaConectividad.UseVisualStyleBackColor = true;
            this.btChecaConectividad.Click += new System.EventHandler(this.btChecaConectividad_Click);
            // 
            // btAceptar
            // 
            this.btAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAceptar.Location = new System.Drawing.Point(204, 221);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 14;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(203, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "Veritce";
            // 
            // FormConexo
            // 
            this.AcceptButton = this.btAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btAceptar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.vertB);
            this.Controls.Add(this.vertA);
            this.Controls.Add(this.vB);
            this.Controls.Add(this.vA);
            this.Controls.Add(this.btChecaConectividad);
            this.Name = "FormConexo";
            this.Text = "Conectividad";
            this.Load += new System.EventHandler(this.FormConexo_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listVertices;
        private System.Windows.Forms.RadioButton vertB;
        private System.Windows.Forms.RadioButton vertA;
        internal System.Windows.Forms.Label vB;
        internal System.Windows.Forms.Label vA;
        private System.Windows.Forms.Button btChecaConectividad;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Label label1;
    }
}