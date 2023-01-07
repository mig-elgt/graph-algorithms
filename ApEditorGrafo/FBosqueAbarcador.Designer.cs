namespace ApEditorGrafo
{
    partial class FBosqueAbarcador
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
            this.cBtiposArcos = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lBArcos = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBtiposArcos
            // 
            this.cBtiposArcos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBtiposArcos.FormattingEnabled = true;
            this.cBtiposArcos.Items.AddRange(new object[] {
            "Árbol",
            "Avance",
            "Retroceso",
            "Cruzados"});
            this.cBtiposArcos.Location = new System.Drawing.Point(21, 40);
            this.cBtiposArcos.Name = "cBtiposArcos";
            this.cBtiposArcos.Size = new System.Drawing.Size(147, 32);
            this.cBtiposArcos.TabIndex = 0;
            this.cBtiposArcos.Text = "Arcos";
            this.cBtiposArcos.SelectedIndexChanged += new System.EventHandler(this.cBtiposArcos_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(298, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lBArcos
            // 
            this.lBArcos.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBArcos.FormattingEnabled = true;
            this.lBArcos.ItemHeight = 22;
            this.lBArcos.Location = new System.Drawing.Point(197, 40);
            this.lBArcos.Name = "lBArcos";
            this.lBArcos.Size = new System.Drawing.Size(136, 136);
            this.lBArcos.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBtiposArcos);
            this.groupBox1.Controls.Add(this.lBArcos);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 202);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipos de Arcos";
            // 
            // FBosqueAbarcador
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 276);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "FBosqueAbarcador";
            this.Text = "Bosque Abarcador";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cBtiposArcos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lBArcos;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}