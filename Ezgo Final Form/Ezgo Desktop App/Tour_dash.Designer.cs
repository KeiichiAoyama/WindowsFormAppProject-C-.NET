namespace Ezgo_Desktop_App
{
    partial class TourGuide
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tourScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobDoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tourPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tourScheduleToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.tourPackageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tourScheduleToolStripMenuItem
            // 
            this.tourScheduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myScheduleToolStripMenuItem,
            this.applyScheduleToolStripMenuItem});
            this.tourScheduleToolStripMenuItem.Name = "tourScheduleToolStripMenuItem";
            this.tourScheduleToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.tourScheduleToolStripMenuItem.Text = "Tour Schedule";
            // 
            // myScheduleToolStripMenuItem
            // 
            this.myScheduleToolStripMenuItem.Name = "myScheduleToolStripMenuItem";
            this.myScheduleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.myScheduleToolStripMenuItem.Text = "My Schedule";
            this.myScheduleToolStripMenuItem.Click += new System.EventHandler(this.myScheduleToolStripMenuItem_Click);
            // 
            // applyScheduleToolStripMenuItem
            // 
            this.applyScheduleToolStripMenuItem.Name = "applyScheduleToolStripMenuItem";
            this.applyScheduleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.applyScheduleToolStripMenuItem.Text = "Apply Schedule";
            this.applyScheduleToolStripMenuItem.Click += new System.EventHandler(this.applyScheduleToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myReportsToolStripMenuItem,
            this.jobDoneToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // myReportsToolStripMenuItem
            // 
            this.myReportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sentToolStripMenuItem,
            this.receivedToolStripMenuItem});
            this.myReportsToolStripMenuItem.Name = "myReportsToolStripMenuItem";
            this.myReportsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.myReportsToolStripMenuItem.Text = "My Reports";
            // 
            // sentToolStripMenuItem
            // 
            this.sentToolStripMenuItem.Name = "sentToolStripMenuItem";
            this.sentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sentToolStripMenuItem.Text = "Sent";
            this.sentToolStripMenuItem.Click += new System.EventHandler(this.sentToolStripMenuItem_Click);
            // 
            // receivedToolStripMenuItem
            // 
            this.receivedToolStripMenuItem.Name = "receivedToolStripMenuItem";
            this.receivedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.receivedToolStripMenuItem.Text = "Received";
            this.receivedToolStripMenuItem.Click += new System.EventHandler(this.receivedToolStripMenuItem_Click);
            // 
            // jobDoneToolStripMenuItem
            // 
            this.jobDoneToolStripMenuItem.Name = "jobDoneToolStripMenuItem";
            this.jobDoneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.jobDoneToolStripMenuItem.Text = "Job Done";
            this.jobDoneToolStripMenuItem.Click += new System.EventHandler(this.jobDoneToolStripMenuItem_Click);
            // 
            // tourPackageToolStripMenuItem
            // 
            this.tourPackageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberListToolStripMenuItem,
            this.clearMembersToolStripMenuItem});
            this.tourPackageToolStripMenuItem.Name = "tourPackageToolStripMenuItem";
            this.tourPackageToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.tourPackageToolStripMenuItem.Text = "Tour Package";
            // 
            // memberListToolStripMenuItem
            // 
            this.memberListToolStripMenuItem.Name = "memberListToolStripMenuItem";
            this.memberListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.memberListToolStripMenuItem.Text = "Member List";
            this.memberListToolStripMenuItem.Click += new System.EventHandler(this.memberListToolStripMenuItem_Click);
            // 
            // clearMembersToolStripMenuItem
            // 
            this.clearMembersToolStripMenuItem.Name = "clearMembersToolStripMenuItem";
            this.clearMembersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearMembersToolStripMenuItem.Text = "Clear Members";
            this.clearMembersToolStripMenuItem.Click += new System.EventHandler(this.clearMembersToolStripMenuItem_Click);
            // 
            // TourGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TourGuide";
            this.Text = "Form4";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tourScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jobDoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tourPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receivedToolStripMenuItem;
    }
}