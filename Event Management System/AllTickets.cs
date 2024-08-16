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
    public partial class AllTickets : Form
    {

        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";
        public AllTickets()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int ticketId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TicketId"].Value);
                    if (DeleteTicket(ticketId))
                    {
                        MessageBox.Show("Ticket Deleted Successfully");
                        LoadAllTickets();
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }

        private void AllTickets_Load(object sender, EventArgs e)
        {
            LoadAllTickets();
        }

        private bool DeleteTicket(int ticketId)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string deleteQuery = "delete from Tickets where TicketId=" + ticketId;
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        int res = cmd.ExecuteNonQuery();
                        return res > 0;
                    }

                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void LoadAllTickets(string s="")
        {
            try
            {
                string query="";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (s == "")
                    {
                        query = "select * from Tickets";
                    } 
                    else
                    {
                        query = "SELECT TicketId, Email, FirstName, LastName, EventId, SeatNo FROM Tickets Where Email like '%"+s+"%' or FirstName like '%"+s+"%' or LastName like '%"+s+"%' or EventId like '%"+s+"%'" ;
                    }
                    using(SqlDataAdapter adapter= new SqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadAllTickets(textBox1.Text);
        }
    }
}
