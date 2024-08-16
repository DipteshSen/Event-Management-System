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

namespace Event_Management_System
{
    public partial class AddEventForm : Form
    {

        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";

        public AddEventForm()
        {
            InitializeComponent();
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string venue = textBox2.Text;
            DateTime date = Convert.ToDateTime(dateTimePicker1.Text);

            if (EventAdd(name, venue, date))
            {
                MessageBox.Show("Event Added");
                ClearForm();
            }
            else
            {
                MessageBox.Show("Event Addition Failed");
            }

            

        }

        private bool EventAdd(string name,string venue,DateTime date)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "insert into Events(EventName, Venue, Date) values(@a,@b,@c) ";
                    using(SqlCommand cmd=new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@a", name);
                        cmd.Parameters.AddWithValue("@b", venue);
                        cmd.Parameters.AddWithValue("@c", date);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            
        }
    }
}
