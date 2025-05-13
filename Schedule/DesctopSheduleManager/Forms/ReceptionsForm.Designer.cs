
namespace DesctopSheduleManager
{
    partial class ReceptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridReceptions;
        private TextBox txtTime;
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
            this.dataGridReceptions = new System.Windows.Forms.DataGridView();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.cmbJobTitle = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddJobTitle = new System.Windows.Forms.Button();
            this.btnRemoveJobTitle = new System.Windows.Forms.Button();
            this.lstJobTitles = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridReceptions)).BeginInit();
            this.SuspendLayout();

            // dataGridReceptions
            this.dataGridReceptions.AllowUserToAddRows = false;
            this.dataGridReceptions.AllowUserToDeleteRows = false;
            this.dataGridReceptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReceptions.Location = new System.Drawing.Point(12, 12);
            this.dataGridReceptions.Name = "dataGridReceptions";
            this.dataGridReceptions.ReadOnly = true;
            this.dataGridReceptions.RowTemplate.Height = 25;
            this.dataGridReceptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridReceptions.Size = new System.Drawing.Size(500, 200);
            this.dataGridReceptions.SelectionChanged += new System.EventHandler(this.dataGridReceptions_SelectionChanged);

            // txtTime
            this.txtTime.Location = new System.Drawing.Point(12, 230);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(200, 23);
            this.txtTime.TabIndex = 1;

            // cmbDepartment
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(12, 270);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(200, 23);
            this.cmbDepartment.TabIndex = 2;

            // cmbJobTitle
            this.cmbJobTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobTitle.FormattingEnabled = true;
            this.cmbJobTitle.Location = new System.Drawing.Point(12, 310);
            this.cmbJobTitle.Name = "cmbJobTitle";
            this.cmbJobTitle.Size = new System.Drawing.Size(200, 23);
            this.cmbJobTitle.TabIndex = 3;

            // btnAddJobTitle
            this.btnAddJobTitle.Location = new System.Drawing.Point(220, 310);
            this.btnAddJobTitle.Name = "btnAddJobTitle";
            this.btnAddJobTitle.Size = new System.Drawing.Size(75, 23);
            this.btnAddJobTitle.TabIndex = 4;
            this.btnAddJobTitle.Text = "Добавить";
            this.btnAddJobTitle.UseVisualStyleBackColor = true;
            this.btnAddJobTitle.Click += new System.EventHandler(this.btnAddJobTitle_Click);

            // lstJobTitles
            this.lstJobTitles.FormattingEnabled = true;
            this.lstJobTitles.ItemHeight = 15;
            this.lstJobTitles.Location = new System.Drawing.Point(12, 350);
            this.lstJobTitles.Name = "lstJobTitles";
            this.lstJobTitles.Size = new System.Drawing.Size(200, 100);
            this.lstJobTitles.TabIndex = 5;

            // btnRemoveJobTitle
            this.btnRemoveJobTitle.Location = new System.Drawing.Point(220, 350);
            this.btnRemoveJobTitle.Name = "btnRemoveJobTitle";
            this.btnRemoveJobTitle.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveJobTitle.TabIndex = 6;
            this.btnRemoveJobTitle.Text = "Удалить";
            this.btnRemoveJobTitle.UseVisualStyleBackColor = true;
            this.btnRemoveJobTitle.Click += new System.EventHandler(this.btnRemoveJobTitle_Click);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(12, 460);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(100, 460);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(190, 460);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // ReceptionsForm
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstJobTitles);
            this.Controls.Add(this.btnRemoveJobTitle);
            this.Controls.Add(this.btnAddJobTitle);
            this.Controls.Add(this.cmbJobTitle);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.dataGridReceptions);
            this.Name = "ReceptionsForm";
            this.Text = "Приёмы";
            this.Load += new System.EventHandler(this.ReceptionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReceptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}