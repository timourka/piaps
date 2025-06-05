using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;
using System.Windows.Forms.DataVisualization.Charting;

namespace DesctopSheduleManager.Forms
{
    public partial class StatsForm : Form
    {
        private HttpClient _client;
        // на форме: groupBoxTypes:radioButtonByDepartments, radioButtonByWorkers; dtpStart; dtpEnd; buttonGenerate
        public StatsForm()
        {
            _client = ApiClient.Instance;
            InitializeComponent();
        }

        private async void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (radioButtonByWorkers.Checked)
            {
                flowLayoutPanel.Controls.Clear();
                flowLayoutPanel.AutoScroll = true;

                var start = DateOnly.FromDateTime(dtpStart.Value.Date);
                var end = DateOnly.FromDateTime(dtpEnd.Value.Date);
                var allDays = Enumerable.Range(0, end.DayNumber - start.DayNumber + 1)
                                        .Select(offset => start.AddDays(offset))
                                        .ToList();

                List<Reception> receptions = await _client.GetFromJsonAsync<List<Reception>>("api/reception/get-all");
                List<Holiday> holidays = await _client.GetFromJsonAsync<List<Holiday>>("api/holiday/get-all");
                List<Department> departments = await _client.GetFromJsonAsync<List<Department>>("api/department/get-all");
                List<Worker> workers = await _client.GetFromJsonAsync<List<Worker>>("api/worker/get-all");

                foreach (var worker in workers)
                {
                    int workingDays = 0;
                    int offDays = 0;
                    int vacationDays = 0;

                    foreach (var day in allDays)
                    {
                        var weekday = (int)day.DayOfWeek;
                        if (weekday == 0) weekday = 7; // Sunday → 7

                        bool isHoliday = holidays.Any(h => h.date == day);
                        bool isDepartmentDayOff = departments
                            .Where(
                                d => d.workers != null &&
                                d.workers
                                .Select(w => w.id)
                                .Contains(worker.id)
                            )
                            .Any(
                                d => d.workSchedules
                                .ElementAtOrDefault(weekday - 1)?.isWorking == false
                            );
                        bool isVacation = worker.vacations?.Any(v => v.days.Any(d => d.date == day)) == true;

                        // Проверяем личное расписание
                        var ws = worker.workSchedules.Count > 0
                                 ? worker.workSchedules[day.DayNumber % worker.workSchedules.Count]
                                 : null;
                        bool isPersonalDayOff = ws != null && !ws.isWorking;

                        if (isHoliday || isDepartmentDayOff || isVacation || isPersonalDayOff)
                            offDays++;
                        else
                            workingDays++;

                        if (isVacation) vacationDays++;
                    }

                    int receptionsCount = receptions.Count(r =>
                        r.personnel?.Any(p => p.id == worker.id) == true &&
                        r.date >= start && r.date <= end
                    );

                    // График для одного сотрудника
                    Chart chart = new Chart
                    {
                        Width = 400,
                        Height = 250,
                        BackColor = Color.White
                    };

                    Legend legend = new Legend
                    {
                        Docking = Docking.Right,
                        Alignment = StringAlignment.Far
                    };
                    chart.Legends.Add(legend);

                    ChartArea area = new ChartArea();
                    area.AxisX.Interval = 0;
                    area.AxisX.LabelStyle.Angle = -45;
                    chart.ChartAreas.Add(area);

                    Series series1 = new Series("Рабочие")
                    {
                        ChartType = SeriesChartType.Column
                    };
                    Series series2 = new Series("Нерабочие")
                    {
                        ChartType = SeriesChartType.Column
                    };
                    Series series3 = new Series("Отпуск")
                    {
                        ChartType = SeriesChartType.Column
                    };
                    Series series4 = new Series("Приёмы")
                    {
                        ChartType = SeriesChartType.Column
                    };

                    series1.Points.AddXY("", workingDays);
                    series2.Points.AddXY("", offDays);
                    series3.Points.AddXY("", vacationDays);
                    series4.Points.AddXY("", receptionsCount);

                    chart.Series.Add(series1);
                    chart.Series.Add(series2);
                    chart.Series.Add(series3);
                    chart.Series.Add(series4);
                    chart.Titles.Add(worker.name ?? "Без имени");

                    flowLayoutPanel.Controls.Add(chart);
                }
            }
            else if (radioButtonByDepartments.Checked)
            {
                flowLayoutPanel.Controls.Clear();
                flowLayoutPanel.AutoScroll = true;

                var start = DateOnly.FromDateTime(dtpStart.Value.Date);
                var end = DateOnly.FromDateTime(dtpEnd.Value.Date);
                var allDays = Enumerable.Range(0, end.DayNumber - start.DayNumber + 1)
                                        .Select(offset => start.AddDays(offset))
                                        .ToList();

                List<Reception> receptions = await _client.GetFromJsonAsync<List<Reception>>("api/reception/get-all");
                List<Holiday> holidays = await _client.GetFromJsonAsync<List<Holiday>>("api/holiday/get-all");
                List<Department> departments = await _client.GetFromJsonAsync<List<Department>>("api/department/get-all");

                foreach (var department in departments)
                {
                    int workingDays = 0;
                    int offDays = 0;
                    int holidayDays = 0;

                    foreach (var day in allDays)
                    {
                        var weekday = (int)day.DayOfWeek;
                        if (weekday == 0) weekday = 7; // Sunday → 7

                        bool isHoliday = holidays.Any(h => h.date == day);
                        bool isDepDayOff = department.workSchedules.ElementAtOrDefault(weekday - 1)?.isWorking == false;

                        if (isHoliday)
                            holidayDays++;

                        if (isDepDayOff || isHoliday)
                            offDays++;
                        else
                            workingDays++;
                    }

                    int receptionsCount = receptions.Count(r =>
                        r.department?.id == department.id &&
                        r.date >= start && r.date <= end
                    );

                    // График для одного отделения
                    Chart chart = new Chart
                    {
                        Width = 400,
                        Height = 250,
                        BackColor = Color.White
                    };

                    Legend legend = new Legend
                    {
                        Docking = Docking.Right,
                        Alignment = StringAlignment.Far
                    };
                    chart.Legends.Add(legend);

                    ChartArea area = new ChartArea();
                    area.AxisX.Interval = 0;
                    area.AxisX.LabelStyle.Angle = -45;
                    chart.ChartAreas.Add(area);

                    Series series1 = new Series("Рабочие дни") { ChartType = SeriesChartType.Column };
                    Series series2 = new Series("Нерабочие дни") { ChartType = SeriesChartType.Column };
                    Series series3 = new Series("Праздники") { ChartType = SeriesChartType.Column };
                    Series series4 = new Series("Приёмы") { ChartType = SeriesChartType.Column };

                    series1.Points.AddXY("", workingDays);
                    series2.Points.AddXY("", offDays);
                    series3.Points.AddXY("", holidayDays);
                    series4.Points.AddXY("", receptionsCount);

                    chart.Series.Add(series1);
                    chart.Series.Add(series2);
                    chart.Series.Add(series3);
                    chart.Series.Add(series4);
                    chart.Titles.Add(department.name ?? "Без названия");

                    flowLayoutPanel.Controls.Add(chart);
                }
            }
        }
    }
}
