namespace DesctopSheduleManager.Forms
{
    partial class ViewReceptionsForm
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
            labelInfo = new Label();
            listBoxReceptions = new ListBox();
            comboBoxSlots = new ComboBox();
            buttonAddReception = new Button();
            comboBoxReceptions = new ComboBox();
            SuspendLayout();
            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Location = new Point(12, 9);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(38, 15);
            labelInfo.TabIndex = 0;
            labelInfo.Text = "label1";
            // 
            // listBoxReceptions
            // 
            listBoxReceptions.FormattingEnabled = true;
            listBoxReceptions.ItemHeight = 15;
            listBoxReceptions.Location = new Point(12, 27);
            listBoxReceptions.Name = "listBoxReceptions";
            listBoxReceptions.Size = new Size(313, 169);
            listBoxReceptions.TabIndex = 1;
            // 
            // comboBoxSlots
            // 
            comboBoxSlots.FormattingEnabled = true;
            comboBoxSlots.Location = new Point(12, 202);
            comboBoxSlots.Name = "comboBoxSlots";
            comboBoxSlots.Size = new Size(313, 23);
            comboBoxSlots.TabIndex = 2;
            // 
            // buttonAddReception
            // 
            buttonAddReception.Location = new Point(192, 260);
            buttonAddReception.Name = "buttonAddReception";
            buttonAddReception.Size = new Size(133, 23);
            buttonAddReception.TabIndex = 3;
            buttonAddReception.Text = "добавить приём";
            buttonAddReception.UseVisualStyleBackColor = true;
            buttonAddReception.Click += buttonAddReception_Click;
            // 
            // comboBoxReceptions
            // 
            comboBoxReceptions.FormattingEnabled = true;
            comboBoxReceptions.Location = new Point(12, 231);
            comboBoxReceptions.Name = "comboBoxReceptions";
            comboBoxReceptions.Size = new Size(313, 23);
            comboBoxReceptions.TabIndex = 4;
            // 
            // ViewReceptionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 298);
            Controls.Add(comboBoxReceptions);
            Controls.Add(buttonAddReception);
            Controls.Add(comboBoxSlots);
            Controls.Add(listBoxReceptions);
            Controls.Add(labelInfo);
            Name = "ViewReceptionsForm";
            Text = "ViewReceptionsForm";
            Load += ViewReceptionsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelInfo;
        private ListBox listBoxReceptions;
        private ComboBox comboBoxSlots;
        private Button buttonAddReception;
        private ComboBox comboBoxReceptions;
    }
}