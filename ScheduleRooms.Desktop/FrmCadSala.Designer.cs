namespace ScheduleRooms
{
    partial class FrmCadSala
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
            panel1 = new System.Windows.Forms.Panel();
            BtnExcluir = new System.Windows.Forms.Button();
            BtnAlterar = new System.Windows.Forms.Button();
            BtnSalvar = new System.Windows.Forms.Button();
            GbListasSalas = new System.Windows.Forms.GroupBox();
            DgvListaSalas = new System.Windows.Forms.DataGridView();
            IdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            SalaDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            RamalDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DescricaoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            groupBox2 = new System.Windows.Forms.GroupBox();
            TxtRamal = new System.Windows.Forms.TextBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            TxtDescricao = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            TxtSala = new System.Windows.Forms.TextBox();
            panel1.SuspendLayout();
            GbListasSalas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvListaSalas).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnExcluir);
            panel1.Controls.Add(BtnAlterar);
            panel1.Controls.Add(BtnSalvar);
            panel1.Controls.Add(GbListasSalas);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(573, 440);
            panel1.TabIndex = 0;
            // 
            // BtnExcluir
            // 
            BtnExcluir.Enabled = false;
            BtnExcluir.Location = new System.Drawing.Point(434, 118);
            BtnExcluir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnExcluir.Name = "BtnExcluir";
            BtnExcluir.Size = new System.Drawing.Size(125, 45);
            BtnExcluir.TabIndex = 5;
            BtnExcluir.Text = "&Excluir";
            BtnExcluir.UseVisualStyleBackColor = true;
            BtnExcluir.Click += BtnExcluir_Click;
            // 
            // BtnAlterar
            // 
            BtnAlterar.Enabled = false;
            BtnAlterar.Location = new System.Drawing.Point(434, 66);
            BtnAlterar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnAlterar.Name = "BtnAlterar";
            BtnAlterar.Size = new System.Drawing.Size(125, 45);
            BtnAlterar.TabIndex = 4;
            BtnAlterar.Text = "&Alterar";
            BtnAlterar.UseVisualStyleBackColor = true;
            BtnAlterar.Click += BtnAlterar_Click;
            // 
            // BtnSalvar
            // 
            BtnSalvar.Location = new System.Drawing.Point(434, 14);
            BtnSalvar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnSalvar.Name = "BtnSalvar";
            BtnSalvar.Size = new System.Drawing.Size(125, 45);
            BtnSalvar.TabIndex = 3;
            BtnSalvar.Text = "&Salvar";
            BtnSalvar.UseVisualStyleBackColor = true;
            BtnSalvar.Click += BtnSalvar_Click;
            // 
            // GbListasSalas
            // 
            GbListasSalas.Controls.Add(DgvListaSalas);
            GbListasSalas.Location = new System.Drawing.Point(14, 177);
            GbListasSalas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GbListasSalas.Name = "GbListasSalas";
            GbListasSalas.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GbListasSalas.Size = new System.Drawing.Size(545, 248);
            GbListasSalas.TabIndex = 6;
            GbListasSalas.TabStop = false;
            GbListasSalas.Text = "Listas de Salas";
            // 
            // DgvListaSalas
            // 
            DgvListaSalas.AllowUserToAddRows = false;
            DgvListaSalas.AllowUserToDeleteRows = false;
            DgvListaSalas.AllowUserToOrderColumns = true;
            DgvListaSalas.BackgroundColor = System.Drawing.SystemColors.Control;
            DgvListaSalas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            DgvListaSalas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { IdDgv, SalaDgv, RamalDgv, DescricaoDgv });
            DgvListaSalas.Location = new System.Drawing.Point(7, 15);
            DgvListaSalas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DgvListaSalas.MultiSelect = false;
            DgvListaSalas.Name = "DgvListaSalas";
            DgvListaSalas.ReadOnly = true;
            DgvListaSalas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            DgvListaSalas.Size = new System.Drawing.Size(531, 219);
            DgvListaSalas.TabIndex = 0;
            DgvListaSalas.CellDoubleClick += DgvListaSalas_CellDoubleClick;
            // 
            // IdDgv
            // 
            IdDgv.DataPropertyName = "Id";
            IdDgv.HeaderText = "Id";
            IdDgv.Name = "IdDgv";
            IdDgv.ReadOnly = true;
            IdDgv.Width = 42;
            // 
            // SalaDgv
            // 
            SalaDgv.DataPropertyName = "SalaReuniao";
            SalaDgv.HeaderText = "Room";
            SalaDgv.Name = "SalaDgv";
            SalaDgv.ReadOnly = true;
            SalaDgv.Width = 53;
            // 
            // RamalDgv
            // 
            RamalDgv.DataPropertyName = "Ramal";
            RamalDgv.HeaderText = "Ramal";
            RamalDgv.Name = "RamalDgv";
            RamalDgv.ReadOnly = true;
            RamalDgv.Width = 65;
            // 
            // DescricaoDgv
            // 
            DescricaoDgv.DataPropertyName = "Description";
            DescricaoDgv.HeaderText = "Descrição";
            DescricaoDgv.Name = "DescricaoDgv";
            DescricaoDgv.ReadOnly = true;
            DescricaoDgv.Width = 83;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(TxtRamal);
            groupBox2.Location = new System.Drawing.Point(327, 14);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(100, 61);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ramal";
            // 
            // TxtRamal
            // 
            TxtRamal.Location = new System.Drawing.Point(10, 19);
            TxtRamal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TxtRamal.MaxLength = 5;
            TxtRamal.Name = "TxtRamal";
            TxtRamal.Size = new System.Drawing.Size(80, 23);
            TxtRamal.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(TxtDescricao);
            groupBox3.Location = new System.Drawing.Point(14, 82);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Size = new System.Drawing.Size(413, 61);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Descrição";
            // 
            // TxtDescricao
            // 
            TxtDescricao.Location = new System.Drawing.Point(10, 19);
            TxtDescricao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TxtDescricao.MaxLength = 1000;
            TxtDescricao.Name = "TxtDescricao";
            TxtDescricao.Size = new System.Drawing.Size(392, 23);
            TxtDescricao.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TxtSala);
            groupBox1.Location = new System.Drawing.Point(14, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(306, 61);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Room";
            // 
            // TxtSala
            // 
            TxtSala.Location = new System.Drawing.Point(8, 19);
            TxtSala.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TxtSala.MaxLength = 500;
            TxtSala.Name = "TxtSala";
            TxtSala.Size = new System.Drawing.Size(291, 23);
            TxtSala.TabIndex = 0;
            // 
            // FrmCadSala
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(573, 440);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCadSala";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cadastro Room";
            Load += FrmCadSala_Load;
            panel1.ResumeLayout(false);
            GbListasSalas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvListaSalas).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtRamal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtSala;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TxtDescricao;
        private System.Windows.Forms.GroupBox GbListasSalas;
        private System.Windows.Forms.DataGridView DgvListaSalas;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnSalvar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalaDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn RamalDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescricaoDgv;
    }
}