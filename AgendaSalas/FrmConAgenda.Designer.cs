namespace ScheduleRooms
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
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            BtnExcluir = new System.Windows.Forms.Button();
            GbListaAgencia = new System.Windows.Forms.GroupBox();
            DgvListaAgenda = new System.Windows.Forms.DataGridView();
            IdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataInicioDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataFimDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DescricaoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            PermitirLigarDgv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            PermitirChamarDgv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            SalaIdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            SalaDescricaoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            groupBox2 = new System.Windows.Forms.GroupBox();
            CbxSelecionarSala = new System.Windows.Forms.ComboBox();
            panel1.SuspendLayout();
            GbListaAgencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvListaAgenda).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(BtnExcluir);
            panel1.Controls.Add(GbListaAgencia);
            panel1.Controls.Add(groupBox2);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(946, 530);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 506);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(159, 15);
            label2.TabIndex = 4;
            label2.Text = "Dois cliques para ATUALIZAR";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 491);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(135, 15);
            label1.TabIndex = 4;
            label1.Text = "Um clique para EXCLUIR";
            // 
            // BtnExcluir
            // 
            BtnExcluir.Location = new System.Drawing.Point(784, 15);
            BtnExcluir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnExcluir.Name = "BtnExcluir";
            BtnExcluir.Size = new System.Drawing.Size(149, 58);
            BtnExcluir.TabIndex = 3;
            BtnExcluir.Text = "&Excluir";
            BtnExcluir.UseVisualStyleBackColor = true;
            BtnExcluir.Click += BtnExcluir_Click;
            // 
            // GbListaAgencia
            // 
            GbListaAgencia.Controls.Add(DgvListaAgenda);
            GbListaAgencia.Location = new System.Drawing.Point(14, 97);
            GbListaAgencia.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GbListaAgencia.Name = "GbListaAgencia";
            GbListaAgencia.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GbListaAgencia.Size = new System.Drawing.Size(919, 391);
            GbListaAgencia.TabIndex = 2;
            GbListaAgencia.TabStop = false;
            GbListaAgencia.Text = "Lista de Agenda";
            // 
            // DgvListaAgenda
            // 
            DgvListaAgenda.AllowUserToAddRows = false;
            DgvListaAgenda.AllowUserToDeleteRows = false;
            DgvListaAgenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            DgvListaAgenda.BackgroundColor = System.Drawing.SystemColors.Control;
            DgvListaAgenda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            DgvListaAgenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvListaAgenda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { IdDgv, DataInicioDgv, DataFimDgv, DescricaoDgv, PermitirLigarDgv, PermitirChamarDgv, SalaIdDgv, SalaDescricaoDgv });
            DgvListaAgenda.Location = new System.Drawing.Point(7, 15);
            DgvListaAgenda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DgvListaAgenda.MultiSelect = false;
            DgvListaAgenda.Name = "DgvListaAgenda";
            DgvListaAgenda.ReadOnly = true;
            DgvListaAgenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            DgvListaAgenda.Size = new System.Drawing.Size(904, 369);
            DgvListaAgenda.TabIndex = 0;
            DgvListaAgenda.CellClick += DgvListaAgenda_CellClick;
            DgvListaAgenda.CellDoubleClick += DgvListaAgenda_CellDoubleClick;
            // 
            // IdDgv
            // 
            IdDgv.DataPropertyName = "Id";
            IdDgv.HeaderText = "Id";
            IdDgv.Name = "IdDgv";
            IdDgv.ReadOnly = true;
            IdDgv.Width = 42;
            // 
            // DataInicioDgv
            // 
            DataInicioDgv.DataPropertyName = "DateStart";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            DataInicioDgv.DefaultCellStyle = dataGridViewCellStyle1;
            DataInicioDgv.HeaderText = "Data Inicio";
            DataInicioDgv.Name = "DataInicioDgv";
            DataInicioDgv.ReadOnly = true;
            DataInicioDgv.Width = 81;
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
            DataFimDgv.Width = 73;
            // 
            // DescricaoDgv
            // 
            DescricaoDgv.DataPropertyName = "Description";
            DescricaoDgv.HeaderText = "Descrição";
            DescricaoDgv.Name = "DescricaoDgv";
            DescricaoDgv.ReadOnly = true;
            DescricaoDgv.Width = 83;
            // 
            // PermitirLigarDgv
            // 
            PermitirLigarDgv.DataPropertyName = "AllowCall";
            PermitirLigarDgv.HeaderText = "Permitir Ligar";
            PermitirLigarDgv.Name = "PermitirLigarDgv";
            PermitirLigarDgv.ReadOnly = true;
            PermitirLigarDgv.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            PermitirLigarDgv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            PermitirLigarDgv.Width = 95;
            // 
            // PermitirChamarDgv
            // 
            PermitirChamarDgv.DataPropertyName = "AllowChat";
            PermitirChamarDgv.HeaderText = "Permitir Chamar";
            PermitirChamarDgv.Name = "PermitirChamarDgv";
            PermitirChamarDgv.ReadOnly = true;
            PermitirChamarDgv.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            PermitirChamarDgv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            PermitirChamarDgv.Width = 109;
            // 
            // SalaIdDgv
            // 
            SalaIdDgv.DataPropertyName = "RoomId";
            SalaIdDgv.HeaderText = "Room Id";
            SalaIdDgv.Name = "SalaIdDgv";
            SalaIdDgv.ReadOnly = true;
            SalaIdDgv.Visible = false;
            // 
            // SalaDescricaoDgv
            // 
            SalaDescricaoDgv.DataPropertyName = "DescSala";
            SalaDescricaoDgv.HeaderText = "Room Descrição";
            SalaDescricaoDgv.Name = "SalaDescricaoDgv";
            SalaDescricaoDgv.ReadOnly = true;
            SalaDescricaoDgv.Width = 98;
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
            groupBox2.Text = "Selecionar Room";
            // 
            // CbxSelecionarSala
            // 
            CbxSelecionarSala.DisplayMember = "SalaReuniao";
            CbxSelecionarSala.FormattingEnabled = true;
            CbxSelecionarSala.Location = new System.Drawing.Point(11, 18);
            CbxSelecionarSala.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CbxSelecionarSala.Name = "CbxSelecionarSala";
            CbxSelecionarSala.Size = new System.Drawing.Size(206, 23);
            CbxSelecionarSala.TabIndex = 0;
            CbxSelecionarSala.ValueMember = "Id";
            CbxSelecionarSala.SelectedIndexChanged += CbxSelecionarSala_SelectedIndexChanged;
            // 
            // FrmConAgenda
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(946, 530);
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
            FormClosing += FrmConAgenda_FormClosing;
            Load += FrmConAgenda_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            GbListaAgencia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvListaAgenda).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CbxSelecionarSala;
        private System.Windows.Forms.GroupBox GbListaAgencia;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}