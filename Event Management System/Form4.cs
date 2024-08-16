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
    public partial class BookTicketForm : Form
    {
        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";


        public BookTicketForm()
        {
            InitializeComponent();
        }

        private void BookTicketForm_Load(object sender, EventArgs e)
        {
            LoadAvailableEvents();
        }

        private void LoadAvailableEvents()
        {
            try
            {
                using(SqlConnection conn= new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select EventId, EventName from Events";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query,conn))
                    {
                        DataTable eventsTable = new DataTable();
                        adapter.Fill(eventsTable);
                        comboBox1.DataSource = eventsTable;
                        comboBox1.DisplayMember = "EventName";
                        comboBox1.ValueMember = "EventId";
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string fname = textBox2.Text;
            string lname = textBox3.Text;
            int eventId = Convert.ToInt32(comboBox1.SelectedValue);

            if (BookTicket(email, fname, lname, eventId)){
                MessageBox.Show("Ticket Booked Successfully");
                NavigateToViewTickets();
            }
            else
            {
                MessageBox.Show("Ticket Booking Failed");
            }
        }

        private void NavigateToViewTickets()
        {
            MainForm mainForm = (MainForm)this.ParentForm;
            mainForm.LoadFormIntoPanel(new ViewTicketsForm());
        }


        private bool BookTicket(string email,string fname,string lname, int eventId)
        {
            try
            {
                using(SqlConnection conn=new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkIfExists = "SELECT count(*) FROM Tickets where Email=@Email and EventId=@eventId";
                    using (SqlCommand cmdo = new SqlCommand(checkIfExists, conn))
                    {
                        cmdo.Parameters.AddWithValue("@Email", email);
                        cmdo.Parameters.AddWithValue("@FirstName", fname);
                        cmdo.Parameters.AddWithValue("@LastName", lname);
                        cmdo.Parameters.AddWithValue("@EventId", eventId);
                        cmdo.Parameters.AddWithValue("@SeatNo", GetNextSeatNumber(eventId));
                        int res = Convert.ToInt32(cmdo.ExecuteScalar());
                        if (res > 0)
                        {
                            MessageBox.Show("Already Registered for this Event");
                            return false;
                        }
                        else
                        {
                            string query = "INSERT INTO Tickets(Email,Firstname,LastName,EventId,SeatNo) values(@Email, @FirstName, @LastName, @EventId, @SeatNo)";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@Email", email);
                                cmd.Parameters.AddWithValue("@FirstName", fname);
                                cmd.Parameters.AddWithValue("@LastName", lname);
                                cmd.Parameters.AddWithValue("@EventId", eventId);
                                cmd.Parameters.AddWithValue("@SeatNo", GetNextSeatNumber(eventId));

                                int result = cmd.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    GeneratePdfTicket(email, fname, lname, eventId);
                                    return true;
                                }
                                return false;

                            }
                        }

                    }
                       
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
                return false;
            }
        }

        public int GetNextSeatNumber(int eventId)
        {
            try
            {
                using(SqlConnection conn= new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select count(*) from Tickets where EventId= @EventId ";
                    using(SqlCommand cmd= new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EventId", eventId);
                        int seatCount = (int)cmd.ExecuteScalar();
                        return seatCount + 1;
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1;
            }
        }


        private void GeneratePdfTicket(string email, string firstName, string lastName, int eventId)
        {
            try
            {
                using(SqlConnection conn=new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT EventName,Venue,Date from Events where EventId=@EventId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EventId", eventId);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string eventName = reader["EventName"].ToString();
                                string venue = reader["Venue"].ToString();
                                DateTime eventDate = Convert.ToDateTime(reader["Date"]);


                                //CREATE PDF
                                Document document = new Document();
                                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), email+"Ticket.pdf");
                                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                                document.Open();
                                document.Add(new Paragraph("Ticket Details"));
                                document.Add(new Paragraph($"Event: {eventName}"));
                                document.Add(new Paragraph($"Venue: {venue}"));
                                document.Add(new Paragraph($"Date: {eventDate.ToString("yyyy-MM-dd HH:mm:ss")}"));
                                document.Add(new Paragraph($"Name: {firstName} {lastName}"));
                                document.Add(new Paragraph($"Email: {email}"));
                                document.Add(new Paragraph($"Seat No: {GetNextSeatNumber(eventId)}"));
                                document.Close();

                                MessageBox.Show($"Ticket PDF generated at: {filePath}");

                            }
                        }
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

    }
}
