
namespace DesctopSheduleManager
{
    partial class ReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dataGridReport;
        private Button btnExportExcel;

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
            lblDepartment = new Label();
            cmbDepartment = new ComboBox();
            lblStart = new Label();
            dtpStart = new DateTimePicker();
            lblEnd = new Label();
            dtpEnd = new DateTimePicker();
            btnGenerate = new Button();
            dataGridReport = new DataGridView();
            btnExportExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridReport).BeginInit();
            SuspendLayout();
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(20, 20);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(82, 15);
            lblDepartment.TabIndex = 1;
            lblDepartment.Text = "Департамент:";
            // 
            // cmbDepartment
            // 
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(120, 16);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(200, 23);
            cmbDepartment.TabIndex = 2;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(20, 60);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(77, 15);
            lblStart.TabIndex = 3;
            lblStart.Text = "Дата начала:";
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(120, 56);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 4;
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(20, 100);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(98, 15);
            lblEnd.TabIndex = 5;
            lblEnd.Text = "Дата окончания:";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(120, 96);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 6;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(350, 56);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(120, 30);
            btnGenerate.TabIndex = 7;
            btnGenerate.Text = "Сформировать";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // dataGridReport
            // 
            dataGridReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReport.Location = new Point(20, 140);
            dataGridReport.Name = "dataGridReport";
            dataGridReport.Size = new Size(650, 300);
            dataGridReport.TabIndex = 8;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(550, 56);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(120, 30);
            btnExportExcel.TabIndex = 0;
            btnExportExcel.Text = "Экспорт в Excel";
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 460);
            Controls.Add(btnExportExcel);
            Controls.Add(lblDepartment);
            Controls.Add(cmbDepartment);
            Controls.Add(lblStart);
            Controls.Add(dtpStart);
            Controls.Add(lblEnd);
            Controls.Add(dtpEnd);
            Controls.Add(btnGenerate);
            Controls.Add(dataGridReport);
            Name = "ReportForm";
            Text = "Отчёт по приёмам";
            Load += ReportForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}