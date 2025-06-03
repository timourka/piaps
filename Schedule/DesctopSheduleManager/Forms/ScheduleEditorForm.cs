using Models;
using System.Data;

namespace DesctopSheduleManager.Forms
{
    public partial class ScheduleEditorForm : Form
    {
        public List<WorkSchedule4Day?> UpdatedSchedule => schedule;

        public ScheduleEditorForm(List<WorkSchedule4Day?> existingSchedule)
        {
            InitializeComponent();
            schedule = existingSchedule.Select(ws => ws != null ? new WorkSchedule4Day
            {
                startOfWork = ws.startOfWork,
                endOfWork = ws.endOfWork,
                isWorking = ws.isWorking
            } : null).ToList();

            LoadScheduleIntoControls();
        }

        private void LoadScheduleIntoControls()
        {
            // Пример: создайте элементы управления для каждого дня — DataGridView или TableLayoutPanel
            // Здесь можно инициализировать TextBoxes, DateTimePickers, CheckBoxes по дням
            // Например: checkboxMonday.Checked = schedule[0]?.isWorking ?? false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Считать изменения из UI обратно в schedule
            // Например: schedule[0].isWorking = checkboxMonday.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
