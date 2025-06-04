using Models;

namespace DesctopSheduleManager.Forms
{
    public partial class WorkerScheduleEditorForm : Form
    {
        private List<CheckBox> workingCheckboxes = new();
        private List<DateTimePicker> startPickers = new();
        private List<DateTimePicker> endPickers = new();
        private List<Label> dayLabels = new();
        private List<string> dayNames = new() {
            "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс",
        };// должны циклически повторяться при добавлении нового дня

        private List<WorkerWorkSchedule4Day> schedule;

        public List<WorkerWorkSchedule4Day> UpdatedSchedule => schedule;

        public WorkerScheduleEditorForm(List<WorkerWorkSchedule4Day> existingSchedule)
        {
            InitializeComponent();
            schedule = existingSchedule.Select(ws => new WorkerWorkSchedule4Day
            {
                startOfWork = ws.startOfWork,
                endOfWork = ws.endOfWork,
                isWorking = ws.isWorking
            }).ToList();

            LoadScheduleIntoControls();
        }

        private void LoadScheduleIntoControls()
        {
            tableLayoutPanel1.RowCount = schedule.Count;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.AutoSize = true;

            for (int i = 0; i < schedule.Count; i++)
            {
                var label = new Label
                {
                    Text = dayNames[i % dayNames.Count],
                    AutoSize = true,
                    Anchor = AnchorStyles.Left
                };

                var checkbox = new CheckBox { Checked = schedule[i]?.isWorking ?? false };
                var start = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true };
                var end = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true };

                if (schedule[i] != null)
                {
                    start.Value = DateTime.Today.Add(schedule[i]!.startOfWork.ToTimeSpan());
                    end.Value = DateTime.Today.Add(schedule[i]!.endOfWork.ToTimeSpan());
                }

                // Добавляем в список для дальнейшего считывания
                dayLabels.Add(label);
                workingCheckboxes.Add(checkbox);
                startPickers.Add(start);
                endPickers.Add(end);

                // Добавляем на панель
                tableLayoutPanel1.Controls.Add(label, 0, i);
                tableLayoutPanel1.Controls.Add(checkbox, 1, i);
                tableLayoutPanel1.Controls.Add(start, 2, i);
                tableLayoutPanel1.Controls.Add(end, 3, i);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < schedule.Count; i++)
            {
                if (workingCheckboxes[i].Checked)
                {
                    schedule[i] = new WorkerWorkSchedule4Day
                    {
                        isWorking = true,
                        startOfWork = TimeOnly.FromDateTime(startPickers[i].Value),
                        endOfWork = TimeOnly.FromDateTime(endPickers[i].Value)
                    };
                }
                else
                {
                    schedule[i] = new WorkerWorkSchedule4Day
                    {
                        isWorking = false,
                        startOfWork = TimeOnly.MinValue,
                        endOfWork = TimeOnly.MinValue
                    };
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Добавим новый день по умолчанию
            schedule.Add(new WorkerWorkSchedule4Day
            {
                isWorking = false,
                startOfWork = TimeOnly.MinValue,
                endOfWork = TimeOnly.MinValue
            });

            // Перезагрузим UI
            LoadScheduleIntoControls();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (schedule.Count > 0)
            {
                // Удалим последний день
                schedule.RemoveAt(schedule.Count - 1);

                // Перезагрузим UI
                LoadScheduleIntoControls();
            }
        }
    }
}
