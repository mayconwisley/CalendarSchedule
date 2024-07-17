using CalendarSchedule.Models;
using ScheduleRooms.Repository;
using System;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmCadAgenda : Form
{
    readonly RoomRepository roomRepository = new();
    readonly ScheduleRepository scheduleRepository = new();
    private int roomId = 0;
    private readonly int _scheduleId = 0;
    readonly bool isModify;
    readonly FrmPrincipal _form;
   
    public FrmCadAgenda()
    {
        InitializeComponent();
    }

    public FrmCadAgenda(FrmPrincipal form) : this()
    {
        _form = form;
    }

    public FrmCadAgenda(int scheduleId, bool modifySchedule) : this()
    {
        _scheduleId = scheduleId;
        isModify = modifySchedule;
        BtnSalvar.Text = "&Update";
    }

    private async void GetRooms()
    {
        CbxSelecionarSala.DataSource = await roomRepository.GetAll();
    }

    private void ClearFields()
    {
        IniciarDatas();
        RTxtDescricao.Clear();
        CbPermitirLigar.Checked = false;
        CbPermitirChamar.Checked = false;
        LblInfo.Text = "";
    }

    private async void GetData(int scheduleId)
    {
        try
        {
            var listaAgenda = await scheduleRepository.GetById(scheduleId);

            MktDataInicio.Text = listaAgenda.DateStart.ToString();
            MktDataFim.Text = listaAgenda.DataFim.ToString();
            RTxtDescricao.Text = listaAgenda.Description.ToString();
            CbPermitirLigar.Checked = listaAgenda.AllowCall;
            CbPermitirChamar.Checked = listaAgenda.AllowChat;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void IniciarDatas()
    {
        MktDataInicio.Text = DateTime.Now.ToString();
        MktDataFim.Text = DateTime.Now.AddHours(1).ToString();
    }

    private void FrmCadAgenda_Load(object sender, EventArgs e)
    {
        GetRooms();
        IniciarDatas();

        if (isModify)
        {
            GetData(_scheduleId);
        }
        Information();
    }

    private void CbxSelecionarSala_SelectedIndexChanged(object sender, EventArgs e)
    {
        roomId = int.Parse(CbxSelecionarSala.SelectedValue.ToString());
    }

    private async void BtnSalvar_Click(object sender, EventArgs e)
    {
        Schedule schedule = new()
        {
            Id = _scheduleId,
            DateStart = DateTime.Parse(MktDataInicio.Text),
            DataFim = DateTime.Parse(MktDataFim.Text),
            Description = RTxtDescricao.Text.Trim(),
            AllowCall = CbPermitirLigar.Checked,
            AllowChat = CbPermitirChamar.Checked,
            RoomId = roomId
        };
        if (string.IsNullOrWhiteSpace(RTxtDescricao.Text))
        {
            MessageBox.Show("É necessário informar uma descrição", "Aviso");
            return;
        }
        try
        {
            if (isModify)
            {
                await scheduleRepository.Update(schedule);
            }
            else
            {
                await scheduleRepository.Create(schedule);
            }

            ClearFields();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Information()
    {
        DateTime dateStart = DateTime.Parse(MktDataInicio.Text);
        DateTime dateEnd = DateTime.Parse(MktDataFim.Text);

        TimeSpan timeSchedule = dateEnd - dateStart;

        LblInfo.Text = $"Tempo de Reunião {timeSchedule.Hours:00}:{timeSchedule.Minutes:00}";
    }

    private void MktDataFim_Leave(object sender, EventArgs e)
    {
        DateTime dateStart = DateTime.Parse(MktDataInicio.Text);
        DateTime dateEnd = DateTime.Parse(MktDataFim.Text);

        if (dateEnd < dateStart)
        {
            MessageBox.Show("Data fim menor que a data inicio. Verifique.", "Aviso");
            return;
        }

        Information();

    }

    private void FrmCadAgenda_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_form is null)
        {
            return;
        }
        _form.ListSchedule();
    }
}
