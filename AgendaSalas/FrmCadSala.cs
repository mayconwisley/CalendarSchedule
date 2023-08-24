using AgendaSalas.Models;
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
    public partial class FrmCadSala : Form
    {
        private SalaRepositorio salaRepositorio = new();
        private int idSala = 0;

        public FrmCadSala()
        {
            InitializeComponent();
        }

        private async void ListarSalas()
        {

            DgvListaSalas.DataSource = await salaRepositorio.ListarTudo();
            int countSalas = DgvListaSalas.RowCount;

            GbListasSalas.Text = $"Listas de Salas - {countSalas:00}";
        }

        private void LimparCampos()
        {
            TxtDescricao.Clear();
            TxtRamal.Clear();
            TxtSala.Clear();
        }

        private void FrmCadSala_Load(object sender, EventArgs e)
        {
            ListarSalas();
        }
        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            Sala sala = new()
            {
                SalaReuniao = TxtSala.Text.Trim(),
                Ramal = TxtRamal.Text.Trim(),
                Descricao = TxtDescricao.Text.Trim()
            };

            try
            {
                await salaRepositorio.Adicionar(sala);
                ListarSalas();
                LimparCampos();
                BtnAlterar.Enabled = false;
                BtnExcluir.Enabled = false;
                BtnSalvar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnAlterar_Click(object sender, EventArgs e)
        {
            Sala sala = new()
            {
                Id = idSala,
                SalaReuniao = TxtSala.Text.Trim(),
                Ramal = TxtRamal.Text.Trim(),
                Descricao = TxtDescricao.Text.Trim()
            };

            try
            {
                await salaRepositorio.Alterar(sala);
                ListarSalas();
                LimparCampos();
                BtnAlterar.Enabled = false;
                BtnExcluir.Enabled = false;
                BtnSalvar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                await salaRepositorio.Excluir(idSala);
                ListarSalas();
                LimparCampos();
                BtnAlterar.Enabled = false;
                BtnExcluir.Enabled = false;
                BtnSalvar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvListaSalas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                idSala = int.Parse(DgvListaSalas.Rows[e.RowIndex].Cells["IdDgv"].Value.ToString());
                TxtSala.Text = DgvListaSalas.Rows[e.RowIndex].Cells["SalaDgv"].Value.ToString();
                TxtRamal.Text = DgvListaSalas.Rows[e.RowIndex].Cells["RamalDgv"].Value.ToString();
                TxtDescricao.Text = DgvListaSalas.Rows[e.RowIndex].Cells["DescricaoDgv"].Value.ToString();
                BtnAlterar.Enabled = true;
                BtnExcluir.Enabled = true;
                BtnSalvar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
