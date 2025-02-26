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
        Koneksi koneksi = new Koneksi();
        SqlCommand cmd;
        SqlDataReader reader;
        public static string KodeKasirGlobal { get; set; }

        public FormTransJual()
        {
            InitializeComponent();
        }

        private void FormTransJual_Load(object sender, EventArgs e)
        {
            KondisiAwal();
            labelKasir.Text = KodeKasirGlobal;
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
            textBoxNoBarang.Text = "";

            labelTanggal.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelJam.Text = DateTime.Now.ToString("HH:mm:ss");
        }
   
        private void textBoxNoJual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {
                    SqlConnection connection = koneksi.GetConn();
                    string query = "SELECT * FROM TB_BARANG WHERE KodeBarang = '" + textBoxNoBarang.Text + "'";
                    cmd = new SqlCommand(query, connection);
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxNoBarang.Text = reader[0].ToString();
                        labelNamaBarang.Text = reader[1].ToString();
                        labelHargaJual.Text = reader[3].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBoxJumlah_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                int hargaJual = int.Parse(labelHargaJual.Text);
                int jumlah = int.Parse(textBoxJumlah.Text);
                int total = hargaJual * jumlah;

                labelHargaJumlah.Text = total.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBoxNoBarang.Text.Trim() == "" || labelNamaBarang.Text.Trim() == ""
                || labelHargaJual.Text.Trim() == "" || textBoxJumlah.Text.Trim() == ""
                || labelHargaJumlah.Text.Trim() == "")
            {
                MessageBox.Show("Data belum lengkap");
            }
            else
            {
             
            }
        }
    }
}
