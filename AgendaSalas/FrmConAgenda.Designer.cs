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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            panel1 = new System.Windows.Forms.Panel();
            BtnExcluir = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            DgvListaAgenda = new System.Windows.Forms.DataGridView();
            groupBox2 = new System.Windows.Forms.GroupBox();
            CbxSelecionarSala = new System.Windows.Forms.ComboBox();
            IdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataInicioDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataFimDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DescricaoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            PermitirLigarDgv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            PermitirChamarDgv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            SalaIdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            SalaDescricaoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvListaAgenda).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnExcluir);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBox2);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1041, 632);
            panel1.TabIndex = 0;
            // 
            // BtnExcluir
            // 
            BtnExcluir.Location = new System.Drawing.Point(878, 14);
            BtnExcluir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnExcluir.Name = "BtnExcluir";
            BtnExcluir.Size = new System.Drawing.Size(149, 58);
            BtnExcluir.TabIndex = 3;
            BtnExcluir.Text = "&Excluir";
            BtnExcluir.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(DgvListaAgenda);
            groupBox1.Location = new System.Drawing.Point(14, 97);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(1014, 522);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Lista de Agenda";
            // 
            // DgvListaAgenda
            // 
            DgvListaAgenda.AllowUserToAddRows = false;
            DgvListaAgenda.AllowUserToDeleteRows = false;
            DgvListaAgenda.BackgroundColor = System.Drawing.SystemColors.Control;
            DgvListaAgenda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            DgvListaAgenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvListaAgenda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { IdDgv, DataInicioDgv, DataFimDgv, DescricaoDgv, PermitirLigarDgv, PermitirChamarDgv, SalaIdDgv, SalaDescricaoDgv });
            DgvListaAgenda.Location = new System.Drawing.Point(7, 22);
            DgvListaAgenda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DgvListaAgenda.MultiSelect = false;
            DgvListaAgenda.Name = "DgvListaAgenda";
            DgvListaAgenda.ReadOnly = true;
            DgvListaAgenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            DgvListaAgenda.Size = new System.Drawing.Size(1000, 493);
            DgvListaAgenda.TabIndex = 0;
            DgvListaAgenda.CellClick += DgvListaAgenda_CellClick;
            DgvListaAgenda.CellDoubleClick += DgvListaAgenda_CellDoubleClick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(CbxSelecionarSala);
            groupBox2.Location = new System.Drawing.Point(14, 14);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(229, 59);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Selecionar Sala";
            // 
            // CbxSelecionarSala
            // 
            CbxSelecionarSala.DisplayMember = "SalaReuniao";
            CbxSelecionarSala.FormattingEnabled = true;
            CbxSelecionarSala.Location = new System.Drawing.Point(7, 22);
            CbxSelecionarSala.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CbxSelecionarSala.Name = "CbxSelecionarSala";
            CbxSelecionarSala.Size = new System.Drawing.Size(206, 23);
            CbxSelecionarSala.TabIndex = 0;
            CbxSelecionarSala.ValueMember = "Id";
            CbxSelecionarSala.SelectedIndexChanged += CbxSelecionarSala_SelectedIndexChanged;
            // 
            // IdDgv
            // 
            IdDgv.DataPropertyName = "Id";
            IdDgv.HeaderText = "Id";
            IdDgv.Name = "IdDgv";
            IdDgv.ReadOnly = true;
            // 
            // DataInicioDgv
            // 
            DataInicioDgv.DataPropertyName = "DataInicio";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            DataInicioDgv.DefaultCellStyle = dataGridViewCellStyle1;
            DataInicioDgv.HeaderText = "Data Inicio";
            DataInicioDgv.Name = "DataInicioDgv";
            DataInicioDgv.ReadOnly = true;
            // 
            // DataFimDgv
            // 
            DataFimDgv.DataPropertyName = "DataFim";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            DataFimDgv.DefaultCellStyle = dataGridViewCellStyle2;
            DataFimDgv.HeaderText = "Data Fim";
            DataFimDgv.Name = "DataFimDgv";
            DataFimDgv.ReadOnly = true;
            // 
            // DescricaoDgv
            // 
            DescricaoDgv.DataPropertyName = "Descricao";
            DescricaoDgv.HeaderText = "Descrição";
            DescricaoDgv.Name = "DescricaoDgv";
            DescricaoDgv.ReadOnly = true;
            // 
            // PermitirLigarDgv
            // 
            PermitirLigarDgv.DataPropertyName = "PermitirLigar";
            PermitirLigarDgv.HeaderText = "Permitir Ligar";
            PermitirLigarDgv.Name = "PermitirLigarDgv";
            PermitirLigarDgv.ReadOnly = true;
            PermitirLigarDgv.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            PermitirLigarDgv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PermitirChamarDgv
            // 
            PermitirChamarDgv.DataPropertyName = "PermitirChamar";
            PermitirChamarDgv.HeaderText = "Permitir Chamar";
            PermitirChamarDgv.Name = "PermitirChamarDgv";
            PermitirChamarDgv.ReadOnly = true;
            PermitirChamarDgv.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            PermitirChamarDgv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // SalaIdDgv
            // 
            SalaIdDgv.DataPropertyName = "SalaId";
            SalaIdDgv.HeaderText = "Sala Id";
            SalaIdDgv.Name = "SalaIdDgv";
            SalaIdDgv.ReadOnly = true;
            SalaIdDgv.Visible = false;
            // 
            // SalaDescricaoDgv
            // 
            SalaDescricaoDgv.DataPropertyName = "Sala";
            SalaDescricaoDgv.HeaderText = "Sala Descrição";
            SalaDescricaoDgv.Name = "SalaDescricaoDgv";
            SalaDescricaoDgv.ReadOnly = true;
            // 
            // FrmConAgenda
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1041, 632);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConAgenda";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Consulta Agenda";
            Load += FrmConAgenda_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvListaAgenda).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CbxSelecionarSala;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DgvListaAgenda;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataInicioDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFimDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescricaoDgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PermitirLigarDgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PermitirChamarDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalaIdDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalaDescricaoDgv;
    }
}