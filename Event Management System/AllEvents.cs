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
    public partial class AllEvents : Form
    {
        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";
        public AllEvents()
        {
            InitializeComponent();
        }

        private void AllEvents_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }

        public void LoadEvents(string s="")
        {
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (s == "")
                    {
                         query = "select * from Events";
                    }
                    else
                    {
                        query = "select * from Events where  EventName like '%" + s + "%' or Date like '%" + s + "%' or Venue like '%" + s + "%' ;";
                    }
                    //MessageBox.Show(query);
                    
                    using(SqlDataAdapter adapter= new SqlDataAdapter(query, conn))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int EventId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EventId"].Value);
                if (DeleteEvent(EventId))
                {
                    MessageBox.Show("Deleted Successfully");
                    LoadEvents();
                }
                else
                {
                    MessageBox.Show("Deletion Failed");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
        }

        private bool DeleteEvent(int eventId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to continue?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Delete from Events where EventId=" + eventId;
                    using(SqlCommand cmd= new SqlCommand(query,conn))
                    {
                        int res = cmd.ExecuteNonQuery();
                        return res > 0;
                    }
                }
            }
            else
            {
                return false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int EventId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EventId"].Value);
                NavigateTo(new EditEventsById(EventId)); 
            }catch(Exception ex)
            {
                MessageBox.Show("Choose an Event to Edit please");
            }
                   
        }

        private void NavigateTo(Form form)
        {
            form.TopMost = true;
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string text = searchBox.Text;
            LoadEvents(text);
        }
    }
}
