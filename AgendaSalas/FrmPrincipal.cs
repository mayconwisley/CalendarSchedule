using AgendaSalas.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaSalas
{
    public partial class FrmPrincipal : Form
    {
        readonly ReuniaoRepositorio reuniaoRepositorio = new();

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private async void ListarAgenda()
        {
            try
            {
                var listaAgenda = await reuniaoRepositorio.ListarAgendadas(DateTime.Now);

                DgvListaAgendaAtual.DataSource = listaAgenda.Select(s => new
                {
                    s.Id,
                    s.DataInicio,
                    s.DataFim,
                    s.Descricao,
                    s.PermitirChamar,
                    s.PermitirLigar,
                    s.Sala.SalaReuniao
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnNovaSala_Click(object sender, EventArgs e)
        {
            FrmCadSala frmCadSala = new();
            frmCadSala.ShowDialog();
        }

        private void BtnNovaAgenda_Click(object sender, EventArgs e)
        {
            FrmCadAgenda frmCadAgenda = new();
            frmCadAgenda.ShowDialog();
        }

        private void BtnConsultarAgenda_Click(object sender, EventArgs e)
        {
            FrmConAgenda frmConAgenda = new();
            frmConAgenda.ShowDialog();
        }

        private void TimerDataHoraAtual_Tick(object sender, EventArgs e)
        {
            LblDataAtual.Text = $"Data: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}";
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ListarAgenda();
        }

        private void LkLblAtualizar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListarAgenda();
        }
    }
}
