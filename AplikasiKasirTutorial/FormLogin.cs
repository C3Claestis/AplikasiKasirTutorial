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
    public partial class FormLogin: Form
    {
        private SqlCommand command;
        private DataSet dataSet;
        private SqlDataAdapter adapter;
        private SqlDataReader reader;

        Koneksi koneksi = new Koneksi();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            SqlDataReader reader = null;
            SqlConnection conn = koneksi.GetConn();
            {
                conn.Open();
                command = new SqlCommand("select * from TB_KASIR where KodeKasir = '" + 
                    textBoxUsername.Text + "' and PasswordKasir = '" + textBoxPassword.Text + "'", conn);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    FormMenuUtama.menuUtama.menuLogin.Enabled = false;
                    FormMenuUtama.menuUtama.menuLogout.Enabled = true;
                    FormMenuUtama.menuUtama.menuMaster.Enabled = true;
                    FormMenuUtama.menuUtama.menuLaporan.Enabled = true;
                    FormMenuUtama.menuUtama.menuTransaksi.Enabled = true;
                    FormMenuUtama.menuUtama.menuUtility.Enabled = true;

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username atau Password salah", "Peringatan", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //if (textBoxUsername.Text == "Admin" && textBoxPassword.Text == "Admin")
            //{
            //    FormMenuUtama formMenuUtama = new FormMenuUtama();
            //    formMenuUtama.Show();

            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Username atau Password salah", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
