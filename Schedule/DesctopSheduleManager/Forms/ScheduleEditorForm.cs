using Models;
using System.Data;

namespace DesctopSheduleManager.Forms
{
    public partial class ScheduleEditorForm : Form
    {
        private readonly List<CheckBox> workingCheckboxes = new();
        private readonly List<DateTimePicker> startPickers = new();
        private readonly List<DateTimePicker> endPickers = new();
        private readonly List<Label> dayLabels = new();
        private readonly List<string> dayNames = new() { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };

        private readonly List<WorkSchedule4Day?> schedule;
        public List<WorkSchedule4Day?> UpdatedSchedule => schedule;

        public ScheduleEditorForm(List<WorkSchedule4Day?>? existingSchedule)
        {
            InitializeComponent();

            // 🧠 Инициализируем пустое расписание, если null или < 7
            schedule = new List<WorkSchedule4Day?>();
            for (int i = 0; i < 7; i++)
            {
                if (existingSchedule != null && i < existingSchedule.Count && existingSchedule[i] != null)
                {
                    var ws = existingSchedule[i]!;
                    schedule.Add(new WorkSchedule4Day
                    {
                        isWorking = ws.isWorking,
                        startOfWork = ws.startOfWork,
                        endOfWork = ws.endOfWork
                    });
                }
                else
                {
                    schedule.Add(new WorkSchedule4Day
                    {
                        isWorking = false,
                        startOfWork = TimeOnly.MinValue,
                        endOfWork = TimeOnly.MinValue
                    });
                }
            }

            LoadScheduleIntoControls();
        }

        private void LoadScheduleIntoControls()
        {
            try
            {
                tableLayoutPanel1.RowCount = 7;
                tableLayoutPanel1.ColumnCount = 4;
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.AutoSize = true;

                for (int i = 0; i < 7; i++)
                {
                    var label = new Label { Text = dayNames[i], AutoSize = true, Anchor = AnchorStyles.Left };
                    var checkbox = new CheckBox { Checked = schedule[i]?.isWorking ?? false };
                    var start = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true };
                    var end = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true };

                    if (schedule[i] != null && schedule[i]!.isWorking)
                    {
                        start.Value = DateTime.Today.Add(schedule[i]!.startOfWork.ToTimeSpan());
                        end.Value = DateTime.Today.Add(schedule[i]!.endOfWork.ToTimeSpan());
                    }

                    dayLabels.Add(label);
                    workingCheckboxes.Add(checkbox);
                    startPickers.Add(start);
                    endPickers.Add(end);

                    tableLayoutPanel1.Controls.Add(label, 0, i);
                    tableLayoutPanel1.Controls.Add(checkbox, 1, i);
                    tableLayoutPanel1.Controls.Add(start, 2, i);
                    tableLayoutPanel1.Controls.Add(end, 3, i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке расписания: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 7; i++)
                {
                    if (workingCheckboxes[i].Checked)
                    {
                        var start = TimeOnly.FromDateTime(startPickers[i].Value);
                        var end = TimeOnly.FromDateTime(endPickers[i].Value);

                        // ❗ Валидация времени
                        if (start >= end)
                        {
                            MessageBox.Show($"Ошибка в дне \"{dayNames[i]}\": начало позже окончания.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        schedule[i] = new WorkSchedule4Day
                        {
                            isWorking = true,
                            startOfWork = start,
                            endOfWork = end
                        };
                    }
                    else
                    {
                        schedule[i] = new WorkSchedule4Day
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении расписания: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
