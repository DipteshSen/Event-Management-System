using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event_Management_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new BookTicketForm());
        }

        private void bookTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new BookTicketForm());
        }

        private void viewAvailableTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new ViewTicketsForm());
        }

        public void LoadFormIntoPanel(Form form)
        {
            Panel mainPanel = this.Controls["mainPanel"] as Panel;
            mainPanel.Controls.Clear();
            form.TopLevel = false;
            form.AutoScroll = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(form);
            form.Show();
        }

       
    }
}
