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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void addEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new AddEventForm());
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

        private void allEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new AllEvents());
        }

        private void allTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new AllTickets());
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new AllEvents());
        }
    }
}
