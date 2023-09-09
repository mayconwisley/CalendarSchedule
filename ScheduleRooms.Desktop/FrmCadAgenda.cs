using ScheduleRooms.Models;
using ScheduleRooms.Repositorio;
using System;
using System.Windows.Forms;

namespace ScheduleRooms;

public partial class FrmCadAgenda : Form
{
    readonly SalaRepositorio salaRepositorio = new();
    readonly ReuniaoRepositorio reuniaoRepositorio = new();
    private int roomId = 0;
    private readonly int reuniaoId = 0;
    readonly bool alterar;
    FrmPrincipal _form;
    public FrmCadAgenda()
    {
        InitializeComponent();
    }

    public FrmCadAgenda(FrmPrincipal form) : this()
    {
        _form = form;
    }

    public FrmCadAgenda(int idReuniao, bool alterarReuniao) : this()
    {
        reuniaoId = idReuniao;
        alterar = alterarReuniao;
        BtnSalvar.Text = "&Alterar";
    }

    private async void ListarSalas()
    {
        CbxSelecionarSala.DataSource = await salaRepositorio.ListarTudo();
    }

    private void LimparCampos()
    {
        IniciarDatas();
        RTxtDescricao.Clear();
        CbPermitirLigar.Checked = false;
        CbPermitirChamar.Checked = false;
        LblInfo.Text = "";
    }

    private async void ListarDados(int idReuniao)
    {
        try
        {
            var listaAgenda = await reuniaoRepositorio.BuscarPorId(idReuniao);

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
        ListarSalas();
        IniciarDatas();

        if (alterar)
        {
            ListarDados(reuniaoId);
        }
        Informacao();
    }

    private void CbxSelecionarSala_SelectedIndexChanged(object sender, EventArgs e)
    {
        roomId = int.Parse(CbxSelecionarSala.SelectedValue.ToString());
    }

    private async void BtnSalvar_Click(object sender, EventArgs e)
    {
        Reuniao reuniao = new()
        {
            Id = reuniaoId,
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

    private void Informacao()
    {
        DateTime dtInicio = DateTime.Parse(MktDataInicio.Text);
        DateTime dtFim = DateTime.Parse(MktDataFim.Text);

        TimeSpan tempoDeReuniao = dtFim - dtInicio;

        LblInfo.Text = $"Tempo de Reunião {tempoDeReuniao.Hours:00}:{tempoDeReuniao.Minutes:00}";
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

        Informacao();

    }

    private void FrmCadAgenda_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_form is null)
        {
            return;
        }
        _form.ListarAgenda();
    }
}
