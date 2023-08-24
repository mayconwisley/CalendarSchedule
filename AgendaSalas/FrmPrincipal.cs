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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void BtnNovaSala_Click(object sender, EventArgs e)
        {
            FrmCadSala frmCadSala = new();
            frmCadSala.ShowDialog();
        }
    }
}
