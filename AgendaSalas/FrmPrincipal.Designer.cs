namespace AgendaSalas
{
    partial class FrmPrincipal
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
            BtnNovaSala = new System.Windows.Forms.Button();
            BtnListarRelatorio = new System.Windows.Forms.Button();
            BtnConsultarAgenda = new System.Windows.Forms.Button();
            BtnNovaAgenda = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            DgvListaAgendaAtual = new System.Windows.Forms.DataGridView();
            IdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            SalaDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataInicioDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataFimDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DescricaoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            PermitirLigarDgv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            PermitirChamarDgv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            panel3 = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            panel4 = new System.Windows.Forms.Panel();
            LblDataAtual = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvListaAgendaAtual).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.Controls.Add(BtnNovaSala);
            panel1.Controls.Add(BtnListarRelatorio);
            panel1.Controls.Add(BtnConsultarAgenda);
            panel1.Controls.Add(BtnNovaAgenda);
            panel1.Location = new System.Drawing.Point(14, 14);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1351, 88);
            panel1.TabIndex = 0;
            // 
            // BtnNovaSala
            // 
            BtnNovaSala.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 0);
            BtnNovaSala.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            BtnNovaSala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnNovaSala.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            BtnNovaSala.Location = new System.Drawing.Point(157, 8);
            BtnNovaSala.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnNovaSala.Name = "BtnNovaSala";
            BtnNovaSala.Size = new System.Drawing.Size(253, 72);
            BtnNovaSala.TabIndex = 3;
            BtnNovaSala.Text = "Nova Sala";
            BtnNovaSala.UseVisualStyleBackColor = true;
            BtnNovaSala.Click += BtnNovaSala_Click;
            // 
            // BtnListarRelatorio
            // 
            BtnListarRelatorio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnListarRelatorio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 0);
            BtnListarRelatorio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            BtnListarRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnListarRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            BtnListarRelatorio.Location = new System.Drawing.Point(940, 8);
            BtnListarRelatorio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnListarRelatorio.Name = "BtnListarRelatorio";
            BtnListarRelatorio.Size = new System.Drawing.Size(253, 72);
            BtnListarRelatorio.TabIndex = 2;
            BtnListarRelatorio.Text = "Listar Relatório";
            BtnListarRelatorio.UseVisualStyleBackColor = true;
            // 
            // BtnConsultarAgenda
            // 
            BtnConsultarAgenda.Anchor = System.Windows.Forms.AnchorStyles.Top;
            BtnConsultarAgenda.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 0);
            BtnConsultarAgenda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            BtnConsultarAgenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnConsultarAgenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            BtnConsultarAgenda.Location = new System.Drawing.Point(679, 8);
            BtnConsultarAgenda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnConsultarAgenda.Name = "BtnConsultarAgenda";
            BtnConsultarAgenda.Size = new System.Drawing.Size(253, 72);
            BtnConsultarAgenda.TabIndex = 1;
            BtnConsultarAgenda.Text = "Consultar";
            BtnConsultarAgenda.UseVisualStyleBackColor = true;
            BtnConsultarAgenda.Click += BtnConsultarAgenda_Click;
            // 
            // BtnNovaAgenda
            // 
            BtnNovaAgenda.Anchor = System.Windows.Forms.AnchorStyles.Top;
            BtnNovaAgenda.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 0);
            BtnNovaAgenda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            BtnNovaAgenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnNovaAgenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            BtnNovaAgenda.Location = new System.Drawing.Point(418, 8);
            BtnNovaAgenda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnNovaAgenda.Name = "BtnNovaAgenda";
            BtnNovaAgenda.Size = new System.Drawing.Size(253, 72);
            BtnNovaAgenda.TabIndex = 0;
            BtnNovaAgenda.Text = "Nova Agenda";
            BtnNovaAgenda.UseVisualStyleBackColor = true;
            BtnNovaAgenda.Click += BtnNovaAgenda_Click;
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel2.Controls.Add(groupBox1);
            panel2.Location = new System.Drawing.Point(14, 183);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1351, 505);
            panel2.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(DgvListaAgendaAtual);
            groupBox1.Location = new System.Drawing.Point(4, 3);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(1344, 498);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Salas Agendadas Atualmente";
            // 
            // DgvListaAgendaAtual
            // 
            DgvListaAgendaAtual.AllowUserToAddRows = false;
            DgvListaAgendaAtual.AllowUserToDeleteRows = false;
            DgvListaAgendaAtual.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            DgvListaAgendaAtual.BackgroundColor = System.Drawing.SystemColors.Control;
            DgvListaAgendaAtual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            DgvListaAgendaAtual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvListaAgendaAtual.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { IdDgv, SalaDgv, DataInicioDgv, DataFimDgv, DescricaoDgv, PermitirLigarDgv, PermitirChamarDgv });
            DgvListaAgendaAtual.Location = new System.Drawing.Point(4, 22);
            DgvListaAgendaAtual.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DgvListaAgendaAtual.MultiSelect = false;
            DgvListaAgendaAtual.Name = "DgvListaAgendaAtual";
            DgvListaAgendaAtual.ReadOnly = true;
            DgvListaAgendaAtual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            DgvListaAgendaAtual.Size = new System.Drawing.Size(1334, 470);
            DgvListaAgendaAtual.TabIndex = 0;
            // 
            // IdDgv
            // 
            IdDgv.HeaderText = "Id";
            IdDgv.Name = "IdDgv";
            IdDgv.ReadOnly = true;
            // 
            // SalaDgv
            // 
            SalaDgv.HeaderText = "Sala";
            SalaDgv.Name = "SalaDgv";
            SalaDgv.ReadOnly = true;
            // 
            // DataInicioDgv
            // 
            DataInicioDgv.HeaderText = "Data Inicio";
            DataInicioDgv.Name = "DataInicioDgv";
            DataInicioDgv.ReadOnly = true;
            // 
            // DataFimDgv
            // 
            DataFimDgv.HeaderText = "Data Fim";
            DataFimDgv.Name = "DataFimDgv";
            DataFimDgv.ReadOnly = true;
            // 
            // DescricaoDgv
            // 
            DescricaoDgv.HeaderText = "Descrição";
            DescricaoDgv.Name = "DescricaoDgv";
            DescricaoDgv.ReadOnly = true;
            // 
            // PermitirLigarDgv
            // 
            PermitirLigarDgv.HeaderText = "Permitir Ligar";
            PermitirLigarDgv.Name = "PermitirLigarDgv";
            PermitirLigarDgv.ReadOnly = true;
            // 
            // PermitirChamarDgv
            // 
            PermitirChamarDgv.HeaderText = "Permitir Chamar";
            PermitirChamarDgv.Name = "PermitirChamarDgv";
            PermitirChamarDgv.ReadOnly = true;
            // 
            // panel3
            // 
            panel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panel3.Controls.Add(label2);
            panel3.Location = new System.Drawing.Point(14, 696);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(322, 38);
            panel3.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(5, 12);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(185, 15);
            label2.TabIndex = 0;
            label2.Text = "Desenvolvido por: Maycon Wisley";
            // 
            // panel4
            // 
            panel4.Controls.Add(LblDataAtual);
            panel4.Location = new System.Drawing.Point(14, 108);
            panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(640, 68);
            panel4.TabIndex = 1;
            // 
            // LblDataAtual
            // 
            LblDataAtual.AutoSize = true;
            LblDataAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            LblDataAtual.Location = new System.Drawing.Point(4, 20);
            LblDataAtual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LblDataAtual.Name = "LblDataAtual";
            LblDataAtual.Size = new System.Drawing.Size(98, 24);
            LblDataAtual.TabIndex = 0;
            LblDataAtual.Text = "DataAtual";
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1379, 748);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FrmPrincipal";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Agendar Salas";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvListaAgendaAtual).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnNovaAgenda;
        private System.Windows.Forms.Button BtnConsultarAgenda;
        private System.Windows.Forms.Button BtnListarRelatorio;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DgvListaAgendaAtual;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label LblDataAtual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalaDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataInicioDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFimDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescricaoDgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PermitirLigarDgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PermitirChamarDgv;
        private System.Windows.Forms.Button BtnNovaSala;
    }
}

