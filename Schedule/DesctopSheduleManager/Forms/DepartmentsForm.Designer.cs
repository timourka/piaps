
namespace DesctopSheduleManager
{
    partial class DepartmentsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridDepartments;
        private TextBox txtName;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnGenerateSchedule;

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
            dataGridDepartments = new DataGridView();
            txtName = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnGenerateSchedule = new Button();
            buttonChangeShedule = new Button();
            buttonShedule = new Button();
            dataGridViewWorkers = new DataGridView();
            buttonAddWorker = new Button();
            comboBoxWorkers = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridDepartments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewWorkers).BeginInit();
            SuspendLayout();
            // 
            // dataGridDepartments
            // 
            dataGridDepartments.AllowUserToAddRows = false;
            dataGridDepartments.AllowUserToDeleteRows = false;
            dataGridDepartments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDepartments.Location = new Point(12, 12);
            dataGridDepartments.Name = "dataGridDepartments";
            dataGridDepartments.ReadOnly = true;
            dataGridDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridDepartments.Size = new Size(759, 200);
            dataGridDepartments.TabIndex = 0;
            dataGridDepartments.SelectionChanged += dataGridDepartments_SelectionChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 230);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(220, 230);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(300, 230);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(380, 230);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnGenerateSchedule
            // 
            btnGenerateSchedule.Location = new Point(554, 230);
            btnGenerateSchedule.Name = "btnGenerateSchedule";
            btnGenerateSchedule.Size = new Size(108, 58);
            btnGenerateSchedule.TabIndex = 5;
            btnGenerateSchedule.Text = "Распределить задачи автоматически";
            btnGenerateSchedule.UseVisualStyleBackColor = true;
            btnGenerateSchedule.Click += btnGenerateSchedule_Click;
            // 
            // buttonChangeShedule
            // 
            buttonChangeShedule.Location = new Point(461, 230);
            buttonChangeShedule.Name = "buttonChangeShedule";
            buttonChangeShedule.Size = new Size(87, 44);
            buttonChangeShedule.TabIndex = 6;
            buttonChangeShedule.Text = "изменить расписание";
            buttonChangeShedule.UseVisualStyleBackColor = true;
            buttonChangeShedule.Click += buttonChangeShedule_Click;
            // 
            // buttonShedule
            // 
            buttonShedule.Location = new Point(668, 230);
            buttonShedule.Name = "buttonShedule";
            buttonShedule.Size = new Size(103, 58);
            buttonShedule.TabIndex = 7;
            buttonShedule.Text = "посмотреть расписание за период";
            buttonShedule.UseVisualStyleBackColor = true;
            buttonShedule.Click += buttonShedule_Click;
            // 
            // dataGridViewWorkers
            // 
            dataGridViewWorkers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewWorkers.Location = new Point(12, 287);
            dataGridViewWorkers.Name = "dataGridViewWorkers";
            dataGridViewWorkers.Size = new Size(200, 126);
            dataGridViewWorkers.TabIndex = 8;
            // 
            // buttonAddWorker
            // 
            buttonAddWorker.Location = new Point(220, 316);
            buttonAddWorker.Name = "buttonAddWorker";
            buttonAddWorker.Size = new Size(157, 23);
            buttonAddWorker.TabIndex = 9;
            buttonAddWorker.Text = "добавить работника";
            buttonAddWorker.UseVisualStyleBackColor = true;
            buttonAddWorker.Click += buttonAddWorker_Click;
            // 
            // comboBoxWorkers
            // 
            comboBoxWorkers.FormattingEnabled = true;
            comboBoxWorkers.Location = new Point(220, 287);
            comboBoxWorkers.Name = "comboBoxWorkers";
            comboBoxWorkers.Size = new Size(157, 23);
            comboBoxWorkers.TabIndex = 10;
            // 
            // DepartmentsForm
            // 
            ClientSize = new Size(783, 425);
            Controls.Add(comboBoxWorkers);
            Controls.Add(buttonAddWorker);
            Controls.Add(dataGridViewWorkers);
            Controls.Add(buttonShedule);
            Controls.Add(buttonChangeShedule);
            Controls.Add(dataGridDepartments);
            Controls.Add(txtName);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnGenerateSchedule);
            Name = "DepartmentsForm";
            Text = "Департаменты";
            Load += DepartmentsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridDepartments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewWorkers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonChangeShedule;
        private Button buttonShedule;
        private DataGridView dataGridViewWorkers;
        private Button buttonAddWorker;
        private ComboBox comboBoxWorkers;
    }
}