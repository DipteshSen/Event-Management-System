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
    public partial class LoginForm : Form
    {
        string connectionString = "Data Source=LAPTOP-USE37V4Q\\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void NavigateTo(Form form)
        {
            form.TopMost = true;
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            if (comboBox1.SelectedItem != null)
            {
                string role = comboBox1.SelectedItem.ToString();
                // Proceed with the role value
                if (email == "" || password == "" || role == null)
                {
                    MessageBox.Show("All fields are required");
                    return;
                }

                if (role.Equals("User"))
                {
                    if (UserLogin(email, password))
                    {
                        MessageBox.Show("Login Successful");
                        NavigateTo(new MainForm());
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Login Failed");
                        return;
                    }
                }
                else if (role.Equals("Administrator"))
                {
                    if (AdminLogin(email, password))
                    {
                        MessageBox.Show("Admin Login Successful");
                        NavigateTo(new AdminDashboard());
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Admin Login Failed");
                        
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a role.");
            }

            
            
        }

        private bool UserLogin(string email, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select count(1) from Users where Email=@Email and Password=@Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 1;

                    }


                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured: " + e.Message);
                return false;

            }
            
        }


        private bool AdminLogin(string email,string password)
        {
            try
            {
                using(SqlConnection conn= new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select count(*) from admin where Email=@Email and Password=@Password";
                    using(SqlCommand cmd=new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email",email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Authentication Failed");
                            return false;
                        }
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.TopMost=true;
            this.Hide();
            form2.Show();
            
        }
    }
}
