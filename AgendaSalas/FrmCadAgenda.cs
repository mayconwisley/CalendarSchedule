using AgendaSalas.Models;
using AgendaSalas.Repositorio;
using System;
using System.Windows.Forms;

namespace AgendaSalas
{
    public partial class FrmCadAgenda : Form
    {
        readonly SalaRepositorio salaRepositorio = new();
        readonly ReuniaoRepositorio reuniaoRepositorio = new();
        private int salaId = 0;
        private readonly int reuniaoId = 0;
        readonly bool alterar;

        public FrmCadAgenda()
        {
            InitializeComponent();
        }

        public FrmCadAgenda(int idReuniao, bool alterarReuniao)
        {
            InitializeComponent();

            reuniaoId = idReuniao;
            alterar = alterarReuniao;
            BtnSalvar.Text = "&Alterar";

            ListarDados(reuniaoId);
        }

        private async void ListarSalas()
        {
            CbxSelecionarSala.DataSource = await salaRepositorio.ListarTudo();
        }

        private void LimparCampos()
        {
            MktDataInicio.Clear();
            MktDataFim.Clear();
            RTxtDescricao.Clear();
            CbPermitirLigar.Checked = false;
            CbPermitirChamar.Checked = false;
            LblInfo.Text = "";
        }

        private async void ListarDados(int idReuniao)
        {
            var listaAgenda = await reuniaoRepositorio.BuscarPorId(idReuniao);

            MktDataInicio.Text = listaAgenda.DataInicio.ToString();
            MktDataFim.Text = listaAgenda.DataFim.ToString();
            RTxtDescricao.Text = listaAgenda.Descricao.ToString();
            CbPermitirLigar.Checked = listaAgenda.PermitirLigar;
            CbPermitirChamar.Checked = listaAgenda.PermitirChamar;
        }

        private void IniciarDatas()
        {
            MktDataInicio.Text = DateTime.Now.ToString();
            MktDataFim.Text = DateTime.Now.AddHours(1).ToString();
        }

        private void FrmCadAgenda_Load(object sender, EventArgs e)
        {
            ListarSalas();
            IniciarDatas();
        }

        private void CbxSelecionarSala_SelectedIndexChanged(object sender, EventArgs e)
        {
            salaId = int.Parse(CbxSelecionarSala.SelectedValue.ToString());
        }

        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            Reuniao reuniao = new()
            {
                Id = reuniaoId,
                DataInicio = DateTime.Parse(MktDataInicio.Text),
                DataFim = DateTime.Parse(MktDataFim.Text),
                Descricao = RTxtDescricao.Text.Trim(),
                PermitirLigar = CbPermitirLigar.Checked,
                PermitirChamar = CbPermitirChamar.Checked,
                SalaId = salaId
            };

            try
            {
                if (alterar)
                {
                    await reuniaoRepositorio.Alterar(reuniao);
                }
                else
                {
                    await reuniaoRepositorio.Adicionar(reuniao);
                }

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MktDataFim_Leave(object sender, EventArgs e)
        {
            DateTime dtInicio = DateTime.Parse(MktDataInicio.Text);
            DateTime dtFim = DateTime.Parse(MktDataFim.Text);

            if (dtFim < dtInicio)
            {
                MessageBox.Show("Data fim menor que a data inicio. Verifique.", "Aviso");
                return;
            }

            TimeSpan tempoDeReuniao = dtFim - dtInicio;

            LblInfo.Text = $"Tempo de Reunião {tempoDeReuniao.Hours:00}:{tempoDeReuniao.Minutes:00}";

        }
    }
}
