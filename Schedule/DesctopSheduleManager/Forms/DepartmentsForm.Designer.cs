
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
            ((System.ComponentModel.ISupportInitialize)dataGridDepartments).BeginInit();
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
            dataGridDepartments.Size = new Size(500, 200);
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
            btnGenerateSchedule.Location = new Point(460, 230);
            btnGenerateSchedule.Name = "btnGenerateSchedule";
            btnGenerateSchedule.Size = new Size(93, 44);
            btnGenerateSchedule.TabIndex = 5;
            btnGenerateSchedule.Text = "Генерировать расписание";
            btnGenerateSchedule.UseVisualStyleBackColor = true;
            btnGenerateSchedule.Click += btnGenerateSchedule_Click;
            // 
            // DepartmentsForm
            // 
            ClientSize = new Size(600, 300);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}