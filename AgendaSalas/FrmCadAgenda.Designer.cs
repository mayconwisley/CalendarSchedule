namespace AgendaSalas
{
    partial class FrmCadAgenda
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
            BtnSalvar = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox7 = new System.Windows.Forms.GroupBox();
            RTxtDescricao = new System.Windows.Forms.RichTextBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            LblInfo = new System.Windows.Forms.Label();
            groupBox5 = new System.Windows.Forms.GroupBox();
            CbPermitirChamar = new System.Windows.Forms.CheckBox();
            CbPermitirLigar = new System.Windows.Forms.CheckBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            MktDataFim = new System.Windows.Forms.MaskedTextBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            MktDataInicio = new System.Windows.Forms.MaskedTextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            CbxSelecionarSala = new System.Windows.Forms.ComboBox();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnSalvar);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(587, 480);
            panel1.TabIndex = 0;
            // 
            // BtnSalvar
            // 
            BtnSalvar.Location = new System.Drawing.Point(448, 13);
            BtnSalvar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnSalvar.Name = "BtnSalvar";
            BtnSalvar.Size = new System.Drawing.Size(125, 45);
            BtnSalvar.TabIndex = 2;
            BtnSalvar.Text = "&Salvar";
            BtnSalvar.UseVisualStyleBackColor = true;
            BtnSalvar.Click += BtnSalvar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox7);
            groupBox2.Controls.Add(groupBox6);
            groupBox2.Controls.Add(groupBox5);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new System.Drawing.Point(14, 89);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(558, 378);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Agendamento";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(RTxtDescricao);
            groupBox7.Location = new System.Drawing.Point(8, 144);
            groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox7.Size = new System.Drawing.Size(541, 220);
            groupBox7.TabIndex = 3;
            groupBox7.TabStop = false;
            groupBox7.Text = "Descrição";
            // 
            // RTxtDescricao
            // 
            RTxtDescricao.Location = new System.Drawing.Point(7, 15);
            RTxtDescricao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RTxtDescricao.MaxLength = 1000;
            RTxtDescricao.Name = "RTxtDescricao";
            RTxtDescricao.Size = new System.Drawing.Size(527, 191);
            RTxtDescricao.TabIndex = 0;
            RTxtDescricao.Text = "";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(LblInfo);
            groupBox6.Location = new System.Drawing.Point(316, 15);
            groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox6.Size = new System.Drawing.Size(233, 60);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            groupBox6.Text = "Informações";
            // 
            // LblInfo
            // 
            LblInfo.AutoSize = true;
            LblInfo.Location = new System.Drawing.Point(7, 23);
            LblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LblInfo.Name = "LblInfo";
            LblInfo.Size = new System.Drawing.Size(37, 15);
            LblInfo.TabIndex = 0;
            LblInfo.Text = "info...";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(CbPermitirChamar);
            groupBox5.Controls.Add(CbPermitirLigar);
            groupBox5.Location = new System.Drawing.Point(8, 82);
            groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox5.Size = new System.Drawing.Size(541, 55);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "Opcionais";
            // 
            // CbPermitirChamar
            // 
            CbPermitirChamar.AutoSize = true;
            CbPermitirChamar.Location = new System.Drawing.Point(298, 22);
            CbPermitirChamar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CbPermitirChamar.Name = "CbPermitirChamar";
            CbPermitirChamar.Size = new System.Drawing.Size(198, 19);
            CbPermitirChamar.TabIndex = 1;
            CbPermitirChamar.Text = "Permitir chamar alguém na sala?";
            CbPermitirChamar.UseVisualStyleBackColor = true;
            // 
            // CbPermitirLigar
            // 
            CbPermitirLigar.AutoSize = true;
            CbPermitirLigar.Location = new System.Drawing.Point(7, 22);
            CbPermitirLigar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CbPermitirLigar.Name = "CbPermitirLigar";
            CbPermitirLigar.Size = new System.Drawing.Size(248, 19);
            CbPermitirLigar.TabIndex = 0;
            CbPermitirLigar.Text = "Permitir ligar no ramal do sala de reunião?";
            CbPermitirLigar.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(MktDataFim);
            groupBox4.Location = new System.Drawing.Point(162, 15);
            groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Size = new System.Drawing.Size(147, 60);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Fim";
            // 
            // MktDataFim
            // 
            MktDataFim.Location = new System.Drawing.Point(8, 19);
            MktDataFim.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MktDataFim.Mask = "00/00/0000 00:00";
            MktDataFim.Name = "MktDataFim";
            MktDataFim.Size = new System.Drawing.Size(130, 23);
            MktDataFim.TabIndex = 0;
            MktDataFim.Leave += MktDataFim_Leave;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(MktDataInicio);
            groupBox3.Location = new System.Drawing.Point(8, 15);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Size = new System.Drawing.Size(147, 60);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Inicio";
            // 
            // MktDataInicio
            // 
            MktDataInicio.Location = new System.Drawing.Point(8, 19);
            MktDataInicio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MktDataInicio.Mask = "00/00/0000 00:00";
            MktDataInicio.Name = "MktDataInicio";
            MktDataInicio.Size = new System.Drawing.Size(130, 23);
            MktDataInicio.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CbxSelecionarSala);
            groupBox1.Location = new System.Drawing.Point(14, 13);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(229, 59);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Selecionar Sala";
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
            // FrmCadAgenda
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(587, 480);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCadAgenda";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cadastro Agenda";
            Load += FrmCadAgenda_Load;
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CbxSelecionarSala;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox MktDataInicio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MaskedTextBox MktDataFim;
        private System.Windows.Forms.Label LblInfo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox CbPermitirChamar;
        private System.Windows.Forms.CheckBox CbPermitirLigar;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox RTxtDescricao;
        private System.Windows.Forms.Button BtnSalvar;
    }
}