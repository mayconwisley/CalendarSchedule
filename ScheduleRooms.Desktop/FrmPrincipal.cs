using ScheduleRooms.Repository;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmPrincipal : Form
{
    readonly ScheduleRepository scheduleRepository = new();

    public FrmPrincipal()
    {
        InitializeComponent();
    }

    public async void ListSchedule()
    {
        try
        {
            var listSchedule = await scheduleRepository.GetScheduleDateCurrent(DateTime.Now);

            DgvListaAgendaAtual.DataSource = listSchedule.Select(s => new
            {
                s.Id,
                s.DateStart,
                s.DataFim,
                s.Description,
                s.AllowChat,
                s.AllowCall,
                s.Room.Name
            }).ToList();

            int totalList = DgvListaAgendaAtual.RowCount;
            GbListaSalasAgenda.Text = $"Salas Agendadas Atualmente {totalList:00}";

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
        FrmCadAgenda frmCadAgenda = new(this);
        frmCadAgenda.ShowDialog();
    }

    private void BtnConsultarAgenda_Click(object sender, EventArgs e)
    {
        FrmConAgenda frmConAgenda = new(this);
        frmConAgenda.ShowDialog();
    }

    private void TimerDataHoraAtual_Tick(object sender, EventArgs e)
    {
        LblDataAtual.Text = $"Data: {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}";
    }

    private void FrmPrincipal_Load(object sender, EventArgs e)
    {
        ListSchedule();
    }

    private void LkLblAtualizar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ListSchedule();
    }
}
