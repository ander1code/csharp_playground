using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibrary.Factories
{
    internal class ConnectionFactory
    {
        private static ConnectionFactory instance = null;
        private string connectionString;

        private ConnectionFactory()
        {
            // this.connectionString = "Data Source=DESKTOP-BNI8QH6;Initial Catalog=dbReviews;Integrated Security=True";

            // pegando do App.Config
            this.connectionString = ConfigurationManager.ConnectionStrings["dbReviews"].ConnectionString;
        }

        public static ConnectionFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectionFactory();

                }
                return ConnectionFactory.instance;
            }
        }

        public SqlConnection Open()
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        public void Close(SqlConnection conn)
        {
            if (conn != null || conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}
