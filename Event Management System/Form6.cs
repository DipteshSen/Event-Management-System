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
    public partial class RoleSelectionForm : Form
    {
        public enum UserRole
        {
            None,User,Admin
        }

        public UserRole SelectedRole { get; private set; }

        public RoleSelectionForm()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedRole = UserRole.User;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
