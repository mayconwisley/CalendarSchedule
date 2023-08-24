namespace AgendaSalas
{
    partial class FrmConAgenda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DgvListaAgenda = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataFim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermitirLigar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PermitirChamar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaAgenda)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 548);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selecionar Sala";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(6, 19);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(177, 21);
            this.comboBox2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgvListaAgenda);
            this.groupBox1.Location = new System.Drawing.Point(12, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(869, 452);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Agenda";
            // 
            // DgvListaAgenda
            // 
            this.DgvListaAgenda.AllowUserToAddRows = false;
            this.DgvListaAgenda.AllowUserToDeleteRows = false;
            this.DgvListaAgenda.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvListaAgenda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvListaAgenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListaAgenda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.DataInicio,
            this.DataFim,
            this.Descricao,
            this.PermitirLigar,
            this.PermitirChamar});
            this.DgvListaAgenda.Location = new System.Drawing.Point(6, 19);
            this.DgvListaAgenda.MultiSelect = false;
            this.DgvListaAgenda.Name = "DgvListaAgenda";
            this.DgvListaAgenda.ReadOnly = true;
            this.DgvListaAgenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvListaAgenda.Size = new System.Drawing.Size(857, 427);
            this.DgvListaAgenda.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // DataInicio
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "g";
            dataGridViewCellStyle5.NullValue = null;
            this.DataInicio.DefaultCellStyle = dataGridViewCellStyle5;
            this.DataInicio.HeaderText = "Data Inicio";
            this.DataInicio.Name = "DataInicio";
            this.DataInicio.ReadOnly = true;
            // 
            // DataFim
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "g";
            dataGridViewCellStyle6.NullValue = null;
            this.DataFim.DefaultCellStyle = dataGridViewCellStyle6;
            this.DataFim.HeaderText = "Data Fim";
            this.DataFim.Name = "DataFim";
            this.DataFim.ReadOnly = true;
            // 
            // Descricao
            // 
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            // 
            // PermitirLigar
            // 
            this.PermitirLigar.HeaderText = "Permitir Ligar";
            this.PermitirLigar.Name = "PermitirLigar";
            this.PermitirLigar.ReadOnly = true;
            this.PermitirLigar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PermitirLigar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PermitirChamar
            // 
            this.PermitirChamar.HeaderText = "Permitir Chamar";
            this.PermitirChamar.Name = "PermitirChamar";
            this.PermitirChamar.ReadOnly = true;
            this.PermitirChamar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PermitirChamar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(753, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 50);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Excluir";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FrmConAgenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 548);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConAgenda";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Agenda";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaAgenda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DgvListaAgenda;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PermitirLigar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PermitirChamar;
        private System.Windows.Forms.Button button1;
    }
}