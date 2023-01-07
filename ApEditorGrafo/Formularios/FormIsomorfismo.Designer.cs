namespace ApEditorGrafo.Formularios
{
    partial class FormIsomorfismo
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
            this.listGrafos = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btComparaGrafos = new System.Windows.Forms.Button();
            this.grafoA = new System.Windows.Forms.Label();
            this.grafoB = new System.Windows.Forms.Label();
            this.opcA = new System.Windows.Forms.RadioButton();
            this.opcB = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listGrafos
            // 
            this.listGrafos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listGrafos.FormattingEnabled = true;
            this.listGrafos.ItemHeight = 15;
            this.listGrafos.Location = new System.Drawing.Point(13, 36);
            this.listGrafos.Name = "listGrafos";
            this.listGrafos.Size = new System.Drawing.Size(116, 124);
            this.listGrafos.TabIndex = 0;
            this.listGrafos.SelectedIndexChanged += new System.EventHandler(this.listGrafos_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(172, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btComparaGrafos
            // 
            this.btComparaGrafos.Location = new System.Drawing.Point(172, 141);
            this.btComparaGrafos.Name = "btComparaGrafos";
            this.btComparaGrafos.Size = new System.Drawing.Size(75, 46);
            this.btComparaGrafos.TabIndex = 2;
            this.btComparaGrafos.Text = "Comparar";
            this.btComparaGrafos.UseVisualStyleBackColor = true;
            this.btComparaGrafos.Click += new System.EventHandler(this.btComparaGrafos_Click);
            // 
            // grafoA
            // 
            this.grafoA.BackColor = System.Drawing.Color.White;
            this.grafoA.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grafoA.Location = new System.Drawing.Point(187, 53);
            this.grafoA.Name = "grafoA";
            this.grafoA.Size = new System.Drawing.Size(60, 23);
            this.grafoA.TabIndex = 5;
            this.grafoA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grafoB
            // 
            this.grafoB.BackColor = System.Drawing.Color.White;
            this.grafoB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grafoB.Location = new System.Drawing.Point(187, 94);
            this.grafoB.Name = "grafoB";
            this.grafoB.Size = new System.Drawing.Size(60, 23);
            this.grafoB.TabIndex = 5;
            this.grafoB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // opcA
            // 
            this.opcA.AutoSize = true;
            this.opcA.Checked = true;
            this.opcA.Location = new System.Drawing.Point(167, 59);
            this.opcA.Name = "opcA";
            this.opcA.Size = new System.Drawing.Size(14, 13);
            this.opcA.TabIndex = 6;
            this.opcA.TabStop = true;
            this.opcA.UseVisualStyleBackColor = true;
            // 
            // opcB
            // 
            this.opcB.AutoSize = true;
            this.opcB.Location = new System.Drawing.Point(167, 100);
            this.opcB.Name = "opcB";
            this.opcB.Size = new System.Drawing.Size(14, 13);
            this.opcB.TabIndex = 6;
            this.opcB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listGrafos);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 174);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Grafos";
            // 
            // FormIsomorfismo
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 241);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opcB);
            this.Controls.Add(this.opcA);
            this.Controls.Add(this.grafoB);
            this.Controls.Add(this.grafoA);
            this.Controls.Add(this.btComparaGrafos);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIsomorfismo";
            this.Text = "Isomorfismo ";
            this.Load += new System.EventHandler(this.FormIsomorfismo_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listGrafos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btComparaGrafos;
        internal System.Windows.Forms.Label grafoA;
        internal System.Windows.Forms.Label grafoB;
        private System.Windows.Forms.RadioButton opcA;
        private System.Windows.Forms.RadioButton opcB;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}