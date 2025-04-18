namespace DesctopSheduleManager.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button btnHolidays;
        private Button btnLogout;
        private Button btnJobTitles;
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
            btnHolidays = new Button();
            btnLogout = new Button();
            btnJobTitles = new Button();
            btnWorkers = new Button();
            btnDepartment = new Button();
            btnReceptions = new Button();
            btnReport = new Button();
            SuspendLayout();
            // 
            // btnHolidays
            // 
            btnHolidays.Location = new Point(50, 30);
            btnHolidays.Name = "btnHolidays";
            btnHolidays.Size = new Size(180, 40);
            btnHolidays.TabIndex = 2;
            btnHolidays.Text = "Управление праздниками";
            btnHolidays.UseVisualStyleBackColor = true;
            btnHolidays.Click += btnHolidays_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(50, 315);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(180, 40);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Выйти";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnJobTitles
            // 
            btnJobTitles.Location = new Point(50, 76);
            btnJobTitles.Name = "btnJobTitles";
            btnJobTitles.Size = new Size(180, 40);
            btnJobTitles.TabIndex = 0;
            btnJobTitles.Text = "Должности";
            btnJobTitles.UseVisualStyleBackColor = true;
            btnJobTitles.Click += btnJobTitles_Click;
            // 
            // btnWorkers
            // 
            btnWorkers.Location = new Point(50, 122);
            btnWorkers.Name = "btnWorkers";
            btnWorkers.Size = new Size(180, 40);
            btnWorkers.TabIndex = 3;
            btnWorkers.Text = "Работники";
            btnWorkers.UseVisualStyleBackColor = true;
            btnWorkers.Click += btnWorkers_Click;
            // 
            // btnDepartment
            // 
            btnDepartment.Location = new Point(50, 168);
            btnDepartment.Name = "btnDepartment";
            btnDepartment.Size = new Size(180, 40);
            btnDepartment.TabIndex = 4;
            btnDepartment.Text = "Департаменты";
            btnDepartment.UseVisualStyleBackColor = true;
            btnDepartment.Click += btnDepartment_Click;
            // 
            // btnReceptions
            // 
            btnReceptions.Location = new Point(50, 214);
            btnReceptions.Name = "btnReceptions";
            btnReceptions.Size = new Size(180, 40);
            btnReceptions.TabIndex = 5;
            btnReceptions.Text = "Записи";
            btnReceptions.UseVisualStyleBackColor = true;
            btnReceptions.Click += btnReceptions_Click;
            // 
            // btnReport
            // 
            btnReport.Location = new Point(50, 260);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(180, 40);
            btnReport.TabIndex = 6;
            btnReport.Text = "Отчёты";
            btnReport.UseVisualStyleBackColor = true;
            btnReport.Click += btnReport_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(286, 367);
            Controls.Add(btnReport);
            Controls.Add(btnReceptions);
            Controls.Add(btnDepartment);
            Controls.Add(btnWorkers);
            Controls.Add(btnJobTitles);
            Controls.Add(btnLogout);
            Controls.Add(btnHolidays);
            Name = "MainForm";
            Text = "Главное меню";
            ResumeLayout(false);
        }

        #endregion

        private Button btnWorkers;
        private Button btnDepartment;
        private Button btnReceptions;
        private Button btnReport;
    }
}