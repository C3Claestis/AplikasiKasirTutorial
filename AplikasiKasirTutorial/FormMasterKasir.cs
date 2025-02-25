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
    public partial class FormMasterKasir: Form
    {
        Koneksi koneksi = new Koneksi();
        private SqlCommand command;
        private DataSet dataSet;
        private SqlDataAdapter adapter;
        private SqlDataReader reader;

        private void FormMasterKasir_Load(object sender, EventArgs e)
        {
            KondisiAwal();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        }

        void ShowLevelKasir()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("ADMIN");
            comboBox1.Items.Add("USER");
        }

        void KondisiAwal()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            ShowLevelKasir();
            ShowDataKasir();
        }

        public FormMasterKasir()
        {
            InitializeComponent();
        }
        void ShowDataKasir()
        {
            SqlConnection sqlConnection = koneksi.GetConn();
            sqlConnection.Open();
            command = new SqlCommand("select * from TB_KASIR", sqlConnection);
            dataSet = new DataSet();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dataSet, "TB_KASIR");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "TB_KASIR";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Refresh();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || 
                textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Data Belum Lengkap", "Peringatan", MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection sqlConnection = koneksi.GetConn();
                sqlConnection.Open();
                command = new SqlCommand("insert into TB_KASIR values ('" + textBox1.Text + "', '" + 
                    textBox2.Text + "', '" + textBox3.Text + "', '" + comboBox1.Text + "')", sqlConnection);

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
                comboBox1.Text = row.Cells[3].Value.ToString();

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                buttonInput.Enabled = false;
                buttonDelete.Enabled = true;
                buttonEdit.Text = "EDIT";
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "EDIT")
            {
                // Aktifkan TextBox dan ComboBox untuk diedit
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                comboBox1.Enabled = true;

                // Ubah teks tombol Edit menjadi Simpan
                buttonEdit.Text = "SIMPAN";
                buttonDelete.Enabled = false;
            }
            else if (buttonEdit.Text == "SIMPAN")
            {
                // Pastikan semua field terisi sebelum update
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" ||
                    textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
                {
                    MessageBox.Show("Data tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }

                // Proses update ke database
                SqlConnection sqlConnection = koneksi.GetConn();
                sqlConnection.Open();
                command = new SqlCommand("UPDATE TB_KASIR SET NamaKasir = @NamaKasir, PasswordKasir = @PasswordKasir," +
                    " LevelKasir = @LevelKasir WHERE KodeKasir = @KodeKasir", sqlConnection);

                command.Parameters.AddWithValue("@KodeKasir", textBox1.Text);
                command.Parameters.AddWithValue("@NamaKasir", textBox2.Text);
                command.Parameters.AddWithValue("@PasswordKasir", textBox3.Text);
                command.Parameters.AddWithValue("@LevelKasir", comboBox1.Text);

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
    }
}
