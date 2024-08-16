using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Event_Management_System
{
    public class Dbconnection
    {
        private string connectionString;

        public Dbconnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string PrintDbConnectionString()
        {
            return connectionString;
        }


         
    }
}
