namespace DesctopSheduleManager.Forms
{
    partial class VacationEditorForm
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
            buttonSave = new Button();
            dateTimePickerStart = new DateTimePicker();
            buttonAdd = new Button();
            buttonDelete = new Button();
            dataGridViewDays = new DataGridView();
            dateTimePickerEnd = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDays).BeginInit();
            SuspendLayout();
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(383, 139);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Location = new Point(258, 12);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(200, 23);
            dateTimePickerStart.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(383, 70);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(258, 70);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // dataGridViewDays
            // 
            dataGridViewDays.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDays.Location = new Point(12, 12);
            dataGridViewDays.Name = "dataGridViewDays";
            dataGridViewDays.Size = new Size(240, 150);
            dataGridViewDays.TabIndex = 4;
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Location = new Point(258, 41);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(200, 23);
            dateTimePickerEnd.TabIndex = 5;
            // 
            // VacationEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 177);
            Controls.Add(dateTimePickerEnd);
            Controls.Add(dataGridViewDays);
            Controls.Add(buttonDelete);
            Controls.Add(buttonAdd);
            Controls.Add(dateTimePickerStart);
            Controls.Add(buttonSave);
            Name = "VacationEditorForm";
            Text = "VacationEditorForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDays).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonSave;
        private DateTimePicker dateTimePickerStart;
        private Button buttonAdd;
        private Button buttonDelete;
        private DataGridView dataGridViewDays;
        private DateTimePicker dateTimePickerEnd;
    }
}