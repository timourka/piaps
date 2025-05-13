namespace DesctopSheduleManager
{
    partial class WorkersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private ComboBox cmbJobTitle;
        private DataGridView dataGridWorkers;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Label lblName;
        private Label lblLogin;
        private Label lblPassword;
        private Label lblJobTitle;

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
            txtName = new TextBox();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            cmbJobTitle = new ComboBox();
            dataGridWorkers = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            lblName = new Label();
            lblLogin = new Label();
            lblPassword = new Label();
            lblJobTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridWorkers).BeginInit();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(120, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 0;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(120, 60);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(200, 23);
            txtLogin.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(120, 100);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 2;
            // 
            // cmbJobTitle
            // 
            cmbJobTitle.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJobTitle.Location = new Point(120, 140);
            cmbJobTitle.Name = "cmbJobTitle";
            cmbJobTitle.Size = new Size(200, 23);
            cmbJobTitle.TabIndex = 3;
            // 
            // dataGridWorkers
            // 
            dataGridWorkers.Location = new Point(20, 200);
            dataGridWorkers.Name = "dataGridWorkers";
            dataGridWorkers.Size = new Size(500, 200);
            dataGridWorkers.TabIndex = 4;
            dataGridWorkers.SelectionChanged += dataGridWorkers_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(340, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 30);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Добавить";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(340, 60);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 30);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Обновить";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(340, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 30);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Удалить";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(340, 140);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Обновить список";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 20);
            lblName.TabIndex = 9;
            lblName.Text = "Имя:";
            // 
            // lblLogin
            // 
            lblLogin.Location = new Point(20, 60);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(100, 20);
            lblLogin.TabIndex = 10;
            lblLogin.Text = "Логин:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(20, 100);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 20);
            lblPassword.TabIndex = 11;
            lblPassword.Text = "Пароль:";
            // 
            // lblJobTitle
            // 
            lblJobTitle.Location = new Point(20, 140);
            lblJobTitle.Name = "lblJobTitle";
            lblJobTitle.Size = new Size(100, 20);
            lblJobTitle.TabIndex = 12;
            lblJobTitle.Text = "Должность:";
            // 
            // WorkersForm
            // 
            ClientSize = new Size(550, 450);
            Controls.Add(txtName);
            Controls.Add(txtLogin);
            Controls.Add(txtPassword);
            Controls.Add(cmbJobTitle);
            Controls.Add(dataGridWorkers);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(lblName);
            Controls.Add(lblLogin);
            Controls.Add(lblPassword);
            Controls.Add(lblJobTitle);
            Name = "WorkersForm";
            Text = "Управление сотрудниками";
            Load += WorkersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridWorkers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}