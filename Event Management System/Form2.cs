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
    public partial class Form2 : Form
    {
        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.TopMost = true;
            form1.Show();
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            string fname = textBox3.Text;
            string lname = textBox4.Text;

            if(SignUp(email, password, fname, lname)){
                MessageBox.Show("Sign-Up Successful");

            }
            else
            {
                MessageBox.Show("Sign-Up Failed");
            }

        }

        public bool SignUp(string email, string password, string fname, string lname)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkUserQuery = "Select count(1) from Users where Email=@email";
                    using(SqlCommand checkCmd=new SqlCommand(checkUserQuery, conn))
                    {
                       checkCmd.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            return false;//User alredy exists
                        }
                    }

                    string insertQuery = "INSERT INTO Users(Email,Password,FirstName,LastName) values(@Email, @Password, @FirstName, @LastName) ";
                    using(SqlCommand insertCmd= new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Email", email);
                        insertCmd.Parameters.AddWithValue("@Password", password); // Ensure to hash the password before storing
                        insertCmd.Parameters.AddWithValue("@FirstName", fname);
                        insertCmd.Parameters.AddWithValue("@LastName", lname);
                        int result = insertCmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
