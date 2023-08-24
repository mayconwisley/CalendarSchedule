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
    public partial class FrmConAgenda : Form
    {
        readonly SalaRepositorio salaRepositorio = new();
        readonly ReuniaoRepositorio reuniaoRepositorio = new();

        int salaId = 0, reuniaoId = 0;

        public FrmConAgenda()
        {
            InitializeComponent();
        }

        private async void ListarSalas()
        {
            try
            {
                var listaSalas = await salaRepositorio.ListarTudo();

                CbxSelecionarSala.DataSource = listaSalas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ListarAgenda(int idSala)
        {
            var listaAgenda = await reuniaoRepositorio.ListarPorSala(idSala);


            DgvListaAgenda.DataSource = listaAgenda.Select(s => new
            {
                s.Id,
                s.DataInicio,
                s.DataFim,
                s.Descricao,
                s.PermitirChamar,
                s.PermitirLigar,
                DescSala = s.Sala.Descricao
            }).ToList();
        }

        private void CbxSelecionarSala_SelectedIndexChanged(object sender, EventArgs e)
        {
            salaId = int.Parse(CbxSelecionarSala.SelectedValue.ToString());

            try
            {
                ListarAgenda(salaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmConAgenda_Load(object sender, EventArgs e)
        {
            ListarSalas();
        }

        private void DgvListaAgenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            reuniaoId = int.Parse(DgvListaAgenda.Rows[e.RowIndex].Cells["IdDgv"].Value.ToString());
            FrmCadAgenda frmCadAgenda = new(reuniaoId, true);
            frmCadAgenda.ShowDialog();
        }

        private void DgvListaAgenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            reuniaoId = int.Parse(DgvListaAgenda.Rows[e.RowIndex].Cells["IdDgv"].Value.ToString());
        }

        private async void BtnExcluir_Click(object sender, EventArgs e)
        {
            await reuniaoRepositorio.Excluir(reuniaoId);
            ListarAgenda(salaId);
        }
    }
}
