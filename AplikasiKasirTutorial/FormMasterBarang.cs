using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AplikasiKasirTutorial
{
    public partial class FormMasterBarang: Form
    {
        Koneksi koneksi = new Koneksi();
        private SqlCommand command;
        private DataSet dataSet;
        private SqlDataAdapter adapter;

        void ShowSatuanBarang()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("PCS");
            comboBox1.Items.Add("BOX");
            comboBox1.Items.Add("BOTOL");
            comboBox1.Items.Add("PACKS");
            comboBox1.Items.Add("KG");
            comboBox1.Items.Add("KARUNG");
            comboBox1.Items.Add("LITER");
        }

        void KondisiAwal()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            buttonInput.Enabled = true;
            buttonEdit.Enabled = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            ShowSatuanBarang();
            ShowDataBarang();
        }

        void ShowDataBarang()
        {
            SqlConnection sqlConnection = koneksi.GetConn();
            sqlConnection.Open();
            command = new SqlCommand("select * from TB_BARANG", sqlConnection);
            dataSet = new DataSet();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dataSet, "TB_BARANG");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "TB_BARANG";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
        }
        public FormMasterBarang()
        {
            InitializeComponent();
        }

        private void FormMasterBarang_Load(object sender, EventArgs e)
        {
            KondisiAwal();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        }
        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" ||
                textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" ||
                textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Data Belum Lengkap", "Peringatan", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection sqlConnection = koneksi.GetConn();
                sqlConnection.Open();
                command = new SqlCommand("insert into TB_BARANG values ('" + textBox1.Text + "', '" +
                    textBox2.Text + "', '" + textBox3.Text + "', '" + 
                    textBox4.Text + "', '" + textBox5.Text + "', '" + 
                    comboBox1.Text + "')", sqlConnection);

                command.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                KondisiAwal();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Pastikan yang diklik adalah baris data, bukan header
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                comboBox1.Text = row.Cells[5].Value.ToString();

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                comboBox1.Enabled = false;
                buttonEdit.Enabled = true;
                buttonInput.Enabled = false;
                buttonDelete.Enabled = true;
                buttonEdit.Text = "EDIT";
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "EDIT")
            {
                // Aktifkan TextBox dan ComboBox untuk diedit
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                comboBox1.Enabled = true;

                // Ubah teks tombol Edit menjadi Simpan
                buttonEdit.Text = "SIMPAN";
                buttonDelete.Enabled = false;
            }
            else if (buttonEdit.Text == "SIMPAN")
            {
                // Pastikan semua field terisi sebelum update
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" ||
                textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" ||
                textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
                {
                    MessageBox.Show("Data tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Proses update ke database
                SqlConnection sqlConnection = koneksi.GetConn();
                sqlConnection.Open();
                command = new SqlCommand("UPDATE TB_BARANG SET NamaBarang = @NamaBarang, HargaBeli = @HargaBeli, HargaJual = @HargaJual, JumlahBarang = @JumlahBarang" +
                    " SatuanBarang = @SatuanBarang WHERE KodeBarang = @KodeBarang", sqlConnection);

                command.Parameters.AddWithValue("@KodeBarang", textBox1.Text);
                command.Parameters.AddWithValue("@NamaBarang", textBox2.Text);
                command.Parameters.AddWithValue("@HargaBeli", textBox3.Text);
                command.Parameters.AddWithValue("@HargaJual", textBox4.Text);
                command.Parameters.AddWithValue("@JumlahBarang", textBox5.Text);
                command.Parameters.AddWithValue("@SatuanBarang", comboBox1.Text);

                command.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show("Data berhasil diperbarui!", "Informasi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Kembalikan kondisi awal
                KondisiAwal();
                buttonEdit.Text = "EDIT";
                buttonDelete.Enabled = true;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Pastikan semua field terisi sebelum update
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" ||
            textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" ||
            textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Data tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Proses update ke database
                SqlConnection sqlConnection = koneksi.GetConn();
                command = new SqlCommand("DELETE FROM TB_BARANG WHERE KodeBarang ='" + textBox1.Text + "'", sqlConnection);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus!", "Informasi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                KondisiAwal();
            }
        }
    }
}
