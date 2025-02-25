using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AplikasiKasirTutorial
{
    class Koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=CLAESTIS\\SQLEXPRESS;Initial " +
                "Catalog=DB_KASIR;Integrated Security=True";
            return conn;
        }
    }
}
