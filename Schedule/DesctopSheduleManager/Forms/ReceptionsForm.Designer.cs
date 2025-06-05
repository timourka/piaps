
namespace DesctopSheduleManager
{
    partial class ReceptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridReceptions;
        private ComboBox cmbDepartment;
        private ComboBox cmbJobTitle;
        private DateTimePicker dtpDate;
        private Button btnAddJobTitle;
        private Button btnRemoveJobTitle;
        private ListBox lstJobTitles;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

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
            dataGridReceptions = new DataGridView();
            cmbDepartment = new ComboBox();
            cmbJobTitle = new ComboBox();
            dtpDate = new DateTimePicker();
            btnAddJobTitle = new Button();
            btnRemoveJobTitle = new Button();
            lstJobTitles = new ListBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            dateTimePickerTime = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dataGridReceptions).BeginInit();
            SuspendLayout();
            // 
            // dataGridReceptions
            // 
            dataGridReceptions.AllowUserToAddRows = false;
            dataGridReceptions.AllowUserToDeleteRows = false;
            dataGridReceptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReceptions.Location = new Point(12, 12);
            dataGridReceptions.Name = "dataGridReceptions";
            dataGridReceptions.ReadOnly = true;
            dataGridReceptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridReceptions.Size = new Size(500, 200);
            dataGridReceptions.TabIndex = 10;
            dataGridReceptions.SelectionChanged += dataGridReceptions_SelectionChanged;
            // 
            // cmbDepartment
            // 
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(12, 270);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(200, 23);
            cmbDepartment.TabIndex = 2;
            // 
            // cmbJobTitle
            // 
            cmbJobTitle.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJobTitle.FormattingEnabled = true;
            cmbJobTitle.Location = new Point(12, 310);
            cmbJobTitle.Name = "cmbJobTitle";
            cmbJobTitle.Size = new Size(200, 23);
            cmbJobTitle.TabIndex = 3;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(0, 0);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 23);
            dtpDate.TabIndex = 0;
            // 
            // btnAddJobTitle
            // 
            btnAddJobTitle.Location = new Point(220, 310);
            btnAddJobTitle.Name = "btnAddJobTitle";
            btnAddJobTitle.Size = new Size(75, 23);
            btnAddJobTitle.TabIndex = 4;
            btnAddJobTitle.Text = "Добавить";
            btnAddJobTitle.UseVisualStyleBackColor = true;
            btnAddJobTitle.Click += btnAddJobTitle_Click;
            // 
            // btnRemoveJobTitle
            // 
            btnRemoveJobTitle.Location = new Point(220, 350);
            btnRemoveJobTitle.Name = "btnRemoveJobTitle";
            btnRemoveJobTitle.Size = new Size(75, 23);
            btnRemoveJobTitle.TabIndex = 6;
            btnRemoveJobTitle.Text = "Удалить";
            btnRemoveJobTitle.UseVisualStyleBackColor = true;
            btnRemoveJobTitle.Click += btnRemoveJobTitle_Click;
            // 
            // lstJobTitles
            // 
            lstJobTitles.FormattingEnabled = true;
            lstJobTitles.ItemHeight = 15;
            lstJobTitles.Location = new Point(12, 350);
            lstJobTitles.Name = "lstJobTitles";
            lstJobTitles.Size = new Size(200, 94);
            lstJobTitles.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 460);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(100, 460);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(190, 460);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // dateTimePickerTime
            // 
            dateTimePickerTime.Format = DateTimePickerFormat.Time;
            dateTimePickerTime.Location = new Point(12, 230);
            dateTimePickerTime.Name = "dateTimePickerTime";
            dateTimePickerTime.ShowUpDown = true;
            dateTimePickerTime.Size = new Size(200, 23);
            dateTimePickerTime.TabIndex = 11;
            // 
            // ReceptionsForm
            // 
            ClientSize = new Size(800, 500);
            Controls.Add(dateTimePickerTime);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(lstJobTitles);
            Controls.Add(btnRemoveJobTitle);
            Controls.Add(btnAddJobTitle);
            Controls.Add(cmbJobTitle);
            Controls.Add(cmbDepartment);
            Controls.Add(dataGridReceptions);
            Name = "ReceptionsForm";
            Text = "Приёмы";
            Load += ReceptionsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridReceptions).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dateTimePickerTime;
    }
}