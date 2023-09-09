using ScheduleRooms.Repository;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmConAgenda : Form
{
    readonly RoomRepository roomRepository = new();
    readonly ScheduleRepository scheduleRepository = new();

    int roomId = 0, scheduleId = 0;

    readonly FrmPrincipal _form;

    public FrmConAgenda()
    {
        InitializeComponent();
    }
    public FrmConAgenda(FrmPrincipal form) : this()
    {
        _form = form;
    }
    private async void GetRooms()
    {
        try
        {
            var listaSalas = await roomRepository.GetAll();

            CbxSelecionarSala.DataSource = listaSalas;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro:\n\n{ex.Message}");
        }
    }

    private async void ListScheduleByIdRoom(int idSala)
    {
        try
        {
            var listaAgenda = await scheduleRepository.GetByRoomId(idSala);

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
            ListScheduleByIdRoom(roomId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void FrmConAgenda_Load(object sender, EventArgs e)
    {
        GetRooms();
    }

    private void DgvListaAgenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        scheduleId = int.Parse(DgvListaAgenda.Rows[e.RowIndex].Cells["IdDgv"].Value.ToString());
        FrmCadAgenda frmCadAgenda = new(scheduleId, true);
        frmCadAgenda.ShowDialog();
    }

    private void DgvListaAgenda_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        scheduleId = int.Parse(DgvListaAgenda.Rows[e.RowIndex].Cells["IdDgv"].Value.ToString());
    }

    private async void BtnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (scheduleId == 0)
            {
                MessageBox.Show("Selecione um item para excluir", "Aviso");
                return;
            }
            await scheduleRepository.Delete(scheduleId);
            ListScheduleByIdRoom(roomId);
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
        _form.ListSchedule();
    }
}
