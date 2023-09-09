using ScheduleRooms.Models;
using ScheduleRooms.Repository;
using System;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmCadSala : Form
{
    private readonly RoomRepository roomRepository = new();
    private int roomId = 0;

    public FrmCadSala()
    {
        InitializeComponent();
    }

    private async void GetRooms()
    {

        DgvListaSalas.DataSource = await roomRepository.GetAll();
        int countRooms = DgvListaSalas.RowCount;

        GbListasSalas.Text = $"Listas de Salas - {countRooms:00}";
    }

    private void ClearFields()
    {
        TxtDescricao.Clear();
        TxtRamal.Clear();
        TxtSala.Clear();
    }

    private void FrmCadSala_Load(object sender, EventArgs e)
    {
        GetRooms();
    }
    private async void BtnSalvar_Click(object sender, EventArgs e)
    {
        Room room = new()
        {
            Name = TxtSala.Text.Trim(),
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


            await roomRepository.Create(room);
            GetRooms();
            ClearFields();
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
            Id = roomId,
            Name = TxtSala.Text.Trim(),
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
            await roomRepository.Update(room);
            GetRooms();
            ClearFields();
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
            await roomRepository.Delete(roomId);
            GetRooms();
            ClearFields();
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
            roomId = int.Parse(DgvListaSalas.Rows[e.RowIndex].Cells["IdDgv"].Value.ToString());
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
