using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AplikasiKasirTutorial
{
    public partial class FormTransJual: Form
    {
        public FormTransJual()
        {
            InitializeComponent();
        }

        private void FormTransJual_Load(object sender, EventArgs e)
        {
            KondisiAwal();
        }

        void KondisiAwal()
        {
            labelNamaBarang.Text = "";
            labelHargaJual.Text = "";
            labelKembali.Text = "";
            labelTotal.Text = "";
            labelItem.Text = "";
            textBoxBayar.Text = "";
            textBoxJumlah.Text = "";
            textBoxNoJual.Text = "";

            labelTanggal.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelJam.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
