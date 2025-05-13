namespace DesctopSheduleManager
{
    partial class JobTitlesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridJobTitles;
        private TextBox txtName;
        private Label lblName;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;

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
            this.dataGridJobTitles = new DataGridView();
            this.txtName = new TextBox();
            this.lblName = new Label();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridJobTitles)).BeginInit();
            this.SuspendLayout();

            // dataGridJobTitles
            this.dataGridJobTitles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridJobTitles.Location = new Point(12, 12);
            this.dataGridJobTitles.Name = "dataGridJobTitles";
            this.dataGridJobTitles.RowTemplate.Height = 25;
            this.dataGridJobTitles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridJobTitles.Size = new Size(360, 200);
            this.dataGridJobTitles.TabIndex = 0;
            this.dataGridJobTitles.SelectionChanged += new EventHandler(this.dataGridJobTitles_SelectionChanged);

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(12, 230);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(95, 15);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Название должн.";

            // txtName
            this.txtName.Location = new Point(12, 250);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(360, 23);
            this.txtName.TabIndex = 2;

            // btnAdd
            this.btnAdd.Location = new Point(12, 290);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 30);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.Location = new Point(102, 290);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(75, 30);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.Location = new Point(192, 290);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 30);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // btnRefresh
            this.btnRefresh.Location = new Point(282, 290);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(90, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Обновить список";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // JobTitlesForm
            this.ClientSize = new Size(384, 341);
            this.Controls.Add(this.dataGridJobTitles);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Name = "JobTitlesForm";
            this.Text = "Управление должностями";
            this.Load += new EventHandler(this.JobTitlesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridJobTitles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}