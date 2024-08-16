using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;

namespace Event_Management_System
{
    public partial class ViewTicketsForm : Form
    {
        string connectionString= "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";
        public ViewTicketsForm()
        {
            InitializeComponent();
        }

        private void ViewTicketsForm_Load(object sender, EventArgs e)
        {
            LoadTickets();
        }

        private void LoadTickets(string s="")
        {
            string query = "";
            try
            {
                using (SqlConnection conn=new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (s == "")
                    {
                        query= "SELECT TicketId, Email, FirstName, LastName, EventId, SeatNo FROM Tickets";
                    }
                    else
                    {
                        query = "SELECT TicketId, Email, FirstName, LastName, EventId, SeatNo FROM Tickets Where Email like '%"+s+"%' or FirstName like '%"+s+"%' or LastName like '%"+s+"%' or EventId like '%"+s+"%'" ;
                    }
                     
                    using (SqlDataAdapter adapter=new SqlDataAdapter(query, conn))
                    {
                        DataTable ticketsTable = new DataTable();
                        adapter.Fill(ticketsTable);
                        dataGridView1.DataSource = ticketsTable;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ticketId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TicketId"].Value);
                DownloadTicket(ticketId);
            }
            else
            {
                MessageBox.Show("Please Select a ticket to download.");
            }
        }

        private void DownloadTicket(int ticketId)
        {
            try
            {
                using(SqlConnection conn= new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT t.Email, t.FirstName, t.LastName, t.SeatNo, e.EventName, e.Venue, e.Date " +
                                   "FROM Tickets t JOIN Events e ON t.EventId = e.EventId WHERE t.TicketId = @TicketId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TicketId", ticketId);
                        using(SqlDataReader reader= cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string email = reader["Email"].ToString();
                                string firstName = reader["FirstName"].ToString();
                                string lastName = reader["LastName"].ToString();
                                string eventName = reader["EventName"].ToString();
                                string venue = reader["Venue"].ToString();

                                DateTime eventDate = Convert.ToDateTime(reader["Date"]);
                                int seatNo = Convert.ToInt32(reader["SeatNo"]);

                                SaveFileDialog saveFileDialog = new SaveFileDialog();

                                saveFileDialog.Filter = "PDF files (*.pdf|*.pdf";
                                saveFileDialog.FileName = $"Ticket_{ticketId}.pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    // Create PDF
                                    Document document = new Document();
                                    PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                                    document.Open();
                                    document.Add(new Paragraph("Ticket Details"));
                                    document.Add(new Paragraph($"Event: {eventName}"));
                                    document.Add(new Paragraph($"Venue: {venue}"));
                                    document.Add(new Paragraph($"Date: {eventDate.ToString("yyyy-MM-dd HH:mm:ss")}"));
                                    document.Add(new Paragraph($"Name: {firstName} {lastName}"));
                                    document.Add(new Paragraph($"Email: {email}"));
                                    document.Add(new Paragraph($"Seat No: {seatNo}"));
                                    document.Close();


                                    MessageBox.Show($"Ticket PDF generated at: {saveFileDialog.FileName}");
                                }


                            }
                        }
                    }
                }



            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadTickets(textBox1.Text);
        }
    }
}
