using ScheduleRooms.Repositorio;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmConAgenda : Form
{
    readonly SalaRepositorio salaRepositorio = new();
    readonly ReuniaoRepositorio reuniaoRepositorio = new();

    int roomId = 0, reuniaoId = 0;

    FrmPrincipal _form;

    public FrmConAgenda()
    {
        InitializeComponent();
    }
    public FrmConAgenda(FrmPrincipal form) : this()
    {
        _form = form;
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
            MessageBox.Show($"Erro:\n\n{ex.Message}");
        }
    }

    private async void ListarAgenda(int idSala)
    {
        try
        {
            var listaAgenda = await reuniaoRepositorio.ListarPorSala(idSala);

            DgvListaAgenda.DataSource = listaAgenda.Select(s => new
            {
                s.Id,
                s.DateStart,
                s.DataFim,
                s.Description,
                s.AllowChat,
                s.AllowCall,
                DescSala = s.Room.Description
            }).ToList();

            int totalAgenda = DgvListaAgenda.RowCount;
            GbListaAgencia.Text = $"Lista de Agenda {totalAgenda:00}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro:\n\n{ex.Message}");
        }
    }

    private void CbxSelecionarSala_SelectedIndexChanged(object sender, EventArgs e)
    {
        roomId = int.Parse(CbxSelecionarSala.SelectedValue.ToString());

        try
        {
            ListarAgenda(roomId);
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
        try
        {
            if (reuniaoId == 0)
            {
                MessageBox.Show("Selecione um item para excluir", "Aviso");
                return;
            }
            await reuniaoRepositorio.Excluir(reuniaoId);
            ListarAgenda(roomId);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro:\n\n{ex.Message}");
        }
    }

    private void FrmConAgenda_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_form is null)
        {
            return;
        }
        _form.ListarAgenda();
    }
}
