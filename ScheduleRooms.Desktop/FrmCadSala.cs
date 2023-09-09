using ScheduleRooms.Models;
using ScheduleRooms.Repositorio;
using System;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmCadSala : Form
{
    private readonly SalaRepositorio salaRepositorio = new();
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
        Room room = new()
        {
            SalaReuniao = TxtSala.Text.Trim(),
            Ramal = TxtRamal.Text.Trim(),
            Description = TxtDescricao.Text.Trim()
        };

        if (string.IsNullOrWhiteSpace(TxtSala.Text))
        {
            MessageBox.Show("Preencher o nome da Room.", "Aviso");
            return;
        }

        try
        {


            await salaRepositorio.Adicionar(room);
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
        Room room = new()
        {
            Id = idSala,
            SalaReuniao = TxtSala.Text.Trim(),
            Ramal = TxtRamal.Text.Trim(),
            Description = TxtDescricao.Text.Trim()
        };
        if (string.IsNullOrWhiteSpace(TxtSala.Text))
        {
            MessageBox.Show("Preencher o nome da Room.", "Aviso");
            return;
        }

        try
        {
            await salaRepositorio.Alterar(room);
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
