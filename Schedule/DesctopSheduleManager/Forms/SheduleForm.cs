using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;

namespace DesctopSheduleManager.Forms
{
    public partial class SheduleForm : Form
    {
        // на вьюхе dataGrid - датагридвью dtpStart - дататаймпикер dtpEnd - дататаймпикер btnGenerate - кнопка
        private readonly HttpClient _client;
        private int _departmentId;
        public SheduleForm(int departmentId)
        {
            _departmentId = departmentId;
            _client = ApiClient.Instance; // Используем ваш ApiClient
            InitializeComponent();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                var start = DateOnly.FromDateTime(dtpStart.Value.Date);
                var end = DateOnly.FromDateTime(dtpEnd.Value.Date);

                Department department = await _client.GetFromJsonAsync<Department>($"api/department/get/{_departmentId}");
                List<Worker> workers = department.workers;
                List<WorkSchedule4Day> departmentSchedule = department.workSchedules;
                List<Holiday> holidays = await _client.GetFromJsonAsync<List<Holiday>>("api/holiday/get-all");

                var allDays = Enumerable.Range(0, end.DayNumber - start.DayNumber + 1)
                                        .Select(offset => start.AddDays(offset))
                                        .ToList();

                dataGrid.Columns.Clear();
                dataGrid.Rows.Clear();

                // Добавим колонки
                dataGrid.Columns.Add("Name", "Сотрудник");
                foreach (var day in allDays)
                {
                    var col = new DataGridViewTextBoxColumn();
                    col.Name = day.ToString();
                    col.HeaderText = $"{day:dd.MM} ({day.DayOfWeek.ToString()[..2]})";
                    dataGrid.Columns.Add(col);
                }

                // Добавим строки с ячейками
                foreach (var worker in workers)
                {
                    worker.workSchedules.Sort((a, b) => a.id.CompareTo(b.id));
                    var rowIndex = dataGrid.Rows.Add();
                    var row = dataGrid.Rows[rowIndex];
                    row.Cells[0].Value = worker.name;

                    for (int i = 0; i < allDays.Count; i++)
                    {
                        var day = allDays[i];
                        var cell = row.Cells[i + 1];

                        bool isInteractive = false;

                        var weekday = (int)day.DayOfWeek;
                        if (weekday == 0) weekday = 7;

                        // Праздники
                        if (holidays.Any(h => h.date == day))
                        {
                            cell.Style.BackColor = Color.LightCoral;
                            cell.Value = "Праздник";
                            continue;
                        }

                        // Отдел не работает
                        var depSchedule = departmentSchedule.ElementAtOrDefault(weekday - 1);
                        if (depSchedule == null || !depSchedule.isWorking)
                        {
                            cell.Style.BackColor = Color.Red;
                            cell.Value = "Выходной";
                            continue;
                        }

                        // Отпуск
                        if (worker.vacations?.Any(v => v.days.Any(d => d.date == day)) == true)
                        {
                            cell.Style.BackColor = Color.Black;
                            cell.Style.ForeColor = Color.White;
                            cell.Value = "Отпуск";
                            continue;
                        }

                        // Личное расписание
                        if (worker.workSchedules?.Count > 0)
                        {
                            var scheduleIndex = day.DayNumber % worker.workSchedules.Count;
                            var ws = worker.workSchedules[scheduleIndex];

                            if (!ws.isWorking)
                            {
                                cell.Style.BackColor = Color.DarkRed;
                                cell.Value = "Не работает";
                                continue;
                            }

                            // Рабочий день
                            cell.Style.BackColor = Color.LightGreen;
                            isInteractive = true;
                        }

                        // Ячейка интерактивна только если день рабочий
                        cell.Tag = new { day, worker, interactive = isInteractive };
                    }
                }


                // Обработчик двойного клика
                dataGrid.CellDoubleClick += (s, args) =>
                {
                    if (args.RowIndex >= 0 && args.ColumnIndex > 0)
                    {
                        var cell = dataGrid.Rows[args.RowIndex].Cells[args.ColumnIndex];
                        if (cell.Tag is not { } tagObj) return;

                        var tagType = tagObj.GetType();
                        var interactiveProp = tagType.GetProperty("interactive");
                        if (interactiveProp == null || !(bool)interactiveProp.GetValue(tagObj)) return;

                        var dayProp = tagType.GetProperty("day");
                        var workerProp = tagType.GetProperty("worker");
                        if (dayProp == null || workerProp == null) return;

                        var day = (DateOnly)dayProp.GetValue(tagObj);
                        var worker = (Worker)workerProp.GetValue(tagObj);

                        var workSchedule = worker.workSchedules.Count > 0
                            ? worker.workSchedules[day.DayNumber % worker.workSchedules.Count]
                            : null;

                        var weekday = (int)day.DayOfWeek;
                        if (weekday == 0) weekday = 7;
                        var depSchedule = department.workSchedules[weekday - 1];

                        var view = new ViewReceptionsForm(department, day, worker, workSchedule, depSchedule);
                        view.ShowDialog();
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при генерации расписания: " + ex.Message);
            }
        }
    }
}
