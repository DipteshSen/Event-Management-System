namespace Event_Management_System
{
    partial class AdminDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEventsToolStripMenuItem,
            this.allEventsToolStripMenuItem,
            this.allTicketsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1143, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addEventsToolStripMenuItem
            // 
            this.addEventsToolStripMenuItem.Name = "addEventsToolStripMenuItem";
            this.addEventsToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.addEventsToolStripMenuItem.Text = "Add Events";
            this.addEventsToolStripMenuItem.Click += new System.EventHandler(this.addEventsToolStripMenuItem_Click);
            // 
            // allEventsToolStripMenuItem
            // 
            this.allEventsToolStripMenuItem.Name = "allEventsToolStripMenuItem";
            this.allEventsToolStripMenuItem.Size = new System.Drawing.Size(87, 24);
            this.allEventsToolStripMenuItem.Text = "All Events";
            this.allEventsToolStripMenuItem.Click += new System.EventHandler(this.allEventsToolStripMenuItem_Click);
            // 
            // allTicketsToolStripMenuItem
            // 
            this.allTicketsToolStripMenuItem.Name = "allTicketsToolStripMenuItem";
            this.allTicketsToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.allTicketsToolStripMenuItem.Text = "All Tickets";
            this.allTicketsToolStripMenuItem.Click += new System.EventHandler(this.allTicketsToolStripMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 28);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1143, 592);
            this.mainPanel.TabIndex = 1;
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 620);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminDashboard";
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addEventsToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem allEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allTicketsToolStripMenuItem;
    }
}