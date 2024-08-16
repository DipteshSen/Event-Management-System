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
    public partial class EditEventsById : Form
    {
        int EventId;
        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";

        public EditEventsById(int eventId)
        {
            InitializeComponent();
            EventId = eventId;

        }

        


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string eventName = textBox1.Text;
                string venue = textBox2.Text;
                DateTime date = Convert.ToDateTime(dateTimePicker1.Value);

                if(SaveChanges(eventName, venue, date))
                {
                    MessageBox.Show("Event Details Updated Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Event Details Updation Failed");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool SaveChanges(string eventName, string venue, DateTime eventDate)
        {
            try
            {
                using(SqlConnection conn= new SqlConnection(connectionString))
                {
                    conn.Open();
                    string InsertQuery = "Update Events set EventName='" + eventName + "', Venue='" + venue + "' , Date='" + eventDate + "' where EventId=" + EventId;

                    using(SqlCommand cmd= new SqlCommand(InsertQuery, conn))
                    {
                        int rs = cmd.ExecuteNonQuery();
                        return rs > 0;
                    }
                }
            }catch(Exception ex)
            {
                return false;
            }
        }

        private void EditEventsById_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string fetch="select * from Events where EventId="+EventId;
                using(SqlCommand cmd= new SqlCommand(fetch,conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader["EventName"].ToString();
                            textBox2.Text = reader["Venue"].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(reader["Date"].ToString());
                        }
                    }
                }
                
            }
        }
    }
}
