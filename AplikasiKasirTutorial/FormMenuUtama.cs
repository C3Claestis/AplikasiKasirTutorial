using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiKasirTutorial
{
    public partial class FormMenuUtama: Form
    {
        public static FormMenuUtama menuUtama;
        FormLogin formLogin;
        FormMasterKasir formKasir;
        FormMasterBarang formBarang;
        FormTransJual formTransJual;

        private void FormMenuUtama_Load(object sender, EventArgs e)
        {
            LockMenu();
        }

        void formLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            formLogin = null;
        }
        void formKasir_FormClosed(object sender, FormClosedEventArgs e)
        {
            formKasir = null;
        }
        void formBarang_FormClosed(object sender, FormClosedEventArgs e)
        {
            formBarang = null;
        }
        void formTransJual_FormClosed(object sender, FormClosedEventArgs e)
        {
            formTransJual = null;
        }

        void LockMenu()
        {
            menuLogin.Enabled = true;
            menuLogout.Enabled = false;
            menuMaster.Enabled = false;
            menuLaporan.Enabled = false;
            menuTransaksi.Enabled = false;
            menuUtility.Enabled = false;
            toolSST2.Text = "";
            toolSST4.Text = "";
            menuUtama = this;
        }

        public FormMenuUtama()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuLogin_Click(object sender, EventArgs e)
        {
            if (formLogin == null)
            {
                formLogin = new FormLogin();
                formLogin.FormClosed += new FormClosedEventHandler(formLogin_FormClosed);
                formLogin.ShowDialog();
            }
            else
            {
                formLogin.Activate();
            }
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            LockMenu();
        }

        private void kasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formKasir == null)
            {
                formKasir = new FormMasterKasir();
                formKasir.FormClosed += new FormClosedEventHandler(formKasir_FormClosed);
                formKasir.ShowDialog();
            }
            else
            {
                formKasir.Activate();
            }
        }

        private void barangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formBarang == null)
            {
                formBarang = new FormMasterBarang();
                formBarang.FormClosed += new FormClosedEventHandler(formBarang_FormClosed);
                formBarang.ShowDialog();
            }
            else
            {
                formBarang.Activate();
            }
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formTransJual == null)
            {
                formTransJual = new FormTransJual();
                formTransJual.FormClosed += new FormClosedEventHandler(formTransJual_FormClosed);
                formTransJual.ShowDialog();
            }
            else
            {
                formTransJual.Activate();
            }
        }
    }
}
