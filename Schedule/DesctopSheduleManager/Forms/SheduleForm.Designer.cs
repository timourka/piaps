namespace DesctopSheduleManager.Forms
{
    partial class SheduleForm
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
            lblStart = new Label();
            dtpStart = new DateTimePicker();
            lblEnd = new Label();
            dtpEnd = new DateTimePicker();
            btnGenerate = new Button();
            dataGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
            SuspendLayout();
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(75, 37);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(77, 15);
            lblStart.TabIndex = 9;
            lblStart.Text = "Дата начала:";
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(175, 33);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 10;
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(75, 77);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(98, 15);
            lblEnd.TabIndex = 11;
            lblEnd.Text = "Дата окончания:";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(175, 73);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 12;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(405, 33);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(120, 30);
            btnGenerate.TabIndex = 13;
            btnGenerate.Text = "Сформировать";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // dataGrid
            // 
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGrid.Location = new Point(75, 117);
            dataGrid.Name = "dataGrid";
            dataGrid.Size = new Size(650, 300);
            dataGrid.TabIndex = 14;
            // 
            // SheduleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 473);
            Controls.Add(lblStart);
            Controls.Add(dtpStart);
            Controls.Add(lblEnd);
            Controls.Add(dtpEnd);
            Controls.Add(btnGenerate);
            Controls.Add(dataGrid);
            Name = "SheduleForm";
            Text = "Shedule";
            ((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStart;
        private DateTimePicker dtpStart;
        private Label lblEnd;
        private DateTimePicker dtpEnd;
        private Button btnGenerate;
        private DataGridView dataGrid;
    }
}