namespace DesctopSheduleManager.Forms
{
    partial class StatsForm
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
            radioButtonByDepartments = new RadioButton();
            groupBoxTypes = new GroupBox();
            radioButtonByWorkers = new RadioButton();
            buttonGenerate = new Button();
            lblStart = new Label();
            dtpStart = new DateTimePicker();
            lblEnd = new Label();
            dtpEnd = new DateTimePicker();
            flowLayoutPanel = new FlowLayoutPanel();
            groupBoxTypes.SuspendLayout();
            SuspendLayout();
            // 
            // radioButtonByDepartments
            // 
            radioButtonByDepartments.AutoSize = true;
            radioButtonByDepartments.Location = new Point(6, 22);
            radioButtonByDepartments.Name = "radioButtonByDepartments";
            radioButtonByDepartments.Size = new Size(108, 19);
            radioButtonByDepartments.TabIndex = 1;
            radioButtonByDepartments.TabStop = true;
            radioButtonByDepartments.Text = "по отделениям";
            radioButtonByDepartments.UseVisualStyleBackColor = true;
            // 
            // groupBoxTypes
            // 
            groupBoxTypes.Controls.Add(radioButtonByWorkers);
            groupBoxTypes.Controls.Add(radioButtonByDepartments);
            groupBoxTypes.Location = new Point(12, 12);
            groupBoxTypes.Name = "groupBoxTypes";
            groupBoxTypes.Size = new Size(131, 100);
            groupBoxTypes.TabIndex = 2;
            groupBoxTypes.TabStop = false;
            groupBoxTypes.Text = "типы";
            // 
            // radioButtonByWorkers
            // 
            radioButtonByWorkers.AutoSize = true;
            radioButtonByWorkers.Location = new Point(6, 47);
            radioButtonByWorkers.Name = "radioButtonByWorkers";
            radioButtonByWorkers.Size = new Size(109, 19);
            radioButtonByWorkers.TabIndex = 2;
            radioButtonByWorkers.TabStop = true;
            radioButtonByWorkers.Text = "по работникам";
            radioButtonByWorkers.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(326, 89);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(125, 23);
            buttonGenerate.TabIndex = 3;
            buttonGenerate.Text = "сгенерировать";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(151, 16);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(77, 15);
            lblStart.TabIndex = 7;
            lblStart.Text = "Дата начала:";
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(251, 12);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 8;
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(151, 56);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(98, 15);
            lblEnd.TabIndex = 9;
            lblEnd.Text = "Дата окончания:";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(251, 52);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 10;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.Location = new Point(12, 118);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(439, 320);
            flowLayoutPanel.TabIndex = 11;
            // 
            // StatsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 450);
            Controls.Add(flowLayoutPanel);
            Controls.Add(lblStart);
            Controls.Add(dtpStart);
            Controls.Add(lblEnd);
            Controls.Add(dtpEnd);
            Controls.Add(buttonGenerate);
            Controls.Add(groupBoxTypes);
            Name = "StatsForm";
            Text = "StatsForm";
            groupBoxTypes.ResumeLayout(false);
            groupBoxTypes.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton radioButton1;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButtonByDepartments;
        private GroupBox groupBoxTypes;
        private RadioButton radioButtonByWorkers;
        private Button buttonGenerate;
        private Label lblStart;
        private DateTimePicker dtpStart;
        private Label lblEnd;
        private DateTimePicker dtpEnd;
        private FlowLayoutPanel flowLayoutPanel;
    }
}