using DesctopSheduleManager.Utilities;
using Models;
using Models.DTO;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace DesctopSheduleManager.Forms
{
    public partial class ViewReceptionsForm : Form
    {
        private readonly HttpClient _client;
        private Department _department;
        private DateOnly _day;
        private Worker _worker;
        private WorkerWorkSchedule4Day _workerSchedule;
        private WorkSchedule4Day _deptSchedule;

        private TimeOnly _startWork;
        private TimeOnly _endWork;

        private List<Reception> _allReceptions;

        public ViewReceptionsForm(
            Department department, DateOnly day, Worker worker,
            WorkerWorkSchedule4Day workerWorkSchedule4Day, WorkSchedule4Day departmentSchedule)
        {
            InitializeComponent();
            _client = ApiClient.Instance;

            _department = department;
            _day = day;
            _worker = worker;
            _workerSchedule = workerWorkSchedule4Day;
            _deptSchedule = departmentSchedule;

            // Общее рабочее время: пересечение интервалов
            _startWork = TimeOnly.FromTimeSpan(
                TimeSpan.FromTicks(Math.Max(
                    _workerSchedule.startOfWork.Ticks,
                    _deptSchedule.startOfWork.Ticks
                )));

            _endWork = TimeOnly.FromTimeSpan(
                TimeSpan.FromTicks(Math.Min(
                    _workerSchedule.endOfWork.Ticks,
                    _deptSchedule.endOfWork.Ticks
                )));
        }

        private List<Reception> _availableTemplates;
        private TimeSpan _slotLength = TimeSpan.FromMinutes(30);

        private async void ViewReceptionsForm_Load(object sender, EventArgs e)
        {
            // 1. Загрузка всех приёмов (шаблонов)
            var allReceptions = await _client.GetFromJsonAsync<List<Reception>>("api/reception/get-all");

            // 2. Выбираем только шаблоны, относящиеся к отделению и без назначенного времени
            _availableTemplates = allReceptions
                .Where(r => r.department.id == _department.id && r.startTime == null)
                .ToList();

            comboBoxReceptions.Items.Clear();
            foreach (var r in _availableTemplates)
            {
                comboBoxReceptions.Items.Add($"{r.time} мин - {string.Join(", ", r.requiredPersonnel.Select(j => j.name))}");
            }

            if (comboBoxReceptions.Items.Count > 0)
                comboBoxReceptions.SelectedIndex = 0;

            // 3. Загрузка занятых приёмов на этот день
            var scheduledReceptions = allReceptions
                .Where(r => r.date == _day && r.startTime != null && r.personnel != null && r.personnel.Any(p => p.id == _worker.id))
                .OrderBy(r => r.startTime)
                .ToList();

            labelInfo.Text = $"Рабочее время: {_startWork} - {_endWork}";
            listBoxReceptions.Items.Clear();
            foreach (var r in scheduledReceptions)
            {
                listBoxReceptions.Items.Add($"{r.startTime} - {r.endTime}: {string.Join(", ", r.requiredPersonnel.Select(j => j.name))}");
            }

            LoadAvailableStartTimes(scheduledReceptions);
        }

        private void LoadAvailableStartTimes(List<Reception> existingReceptions)
        {
            comboBoxSlots.Items.Clear();
            var start = _startWork;

            while (start.Add(_slotLength) <= _endWork)
            {
                // Проверка пересечений
                bool conflict = existingReceptions.Any(r =>
                    r.startTime.HasValue &&
                    start >= r.startTime.Value &
                    start <= r.endTime
                );

                if (!conflict)
                {
                    comboBoxSlots.Items.Add(start.ToString("HH:mm"));
                }

                start = start.Add(_slotLength);
            }

            if (comboBoxSlots.Items.Count > 0)
                comboBoxSlots.SelectedIndex = 0;
        }

        private async void buttonAddReception_Click(object sender, EventArgs e)
        {
            if (comboBoxSlots.SelectedItem == null || comboBoxReceptions.SelectedIndex == -1)
                return;

            var selectedStart = TimeOnly.Parse(comboBoxSlots.SelectedItem.ToString());
            var template = _availableTemplates[comboBoxReceptions.SelectedIndex];

            // Проверка: подходит ли врач по должности
            bool canAssign = template.requiredPersonnel.Any(j => j.id == _worker.JobTitleId);
            if (!canAssign)
            {
                MessageBox.Show("У врача неподходящая должность для выбранного приёма.");
                return;
            }

            Reception oldReception = await _client.GetFromJsonAsync<Reception>($"api/reception/get/{template.id}");
            // Назначаем врача
            if (oldReception == null)
            {
                MessageBox.Show("не нашёлся приём.");
                return;
            }
            if (oldReception.personnel == null)
                oldReception.personnel = new();
            oldReception.personnel.Add(_worker);
            oldReception.date = _day;
            oldReception.startTime = selectedStart;

            var response = await _client.PutAsJsonAsync($"api/reception/update/{oldReception.id}", oldReception);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Приём успешно назначен.");
                ViewReceptionsForm_Load(this, EventArgs.Empty); // Обновим форму
            }
            else
            {
                MessageBox.Show("Ошибка при назначении приёма."); // вот здесь без каких либо причин оказываюсь на сервере даже метод контроллера не вызывается
            }
        }

        //private async void buttonAddReception_Click(object sender, EventArgs e)
        //{
        //    if (comboBoxSlots.SelectedItem == null || comboBoxReceptions.SelectedIndex == -1)
        //        return;

        //    var selectedStart = TimeOnly.Parse(comboBoxSlots.SelectedItem.ToString());
        //    var template = _availableTemplates[comboBoxReceptions.SelectedIndex];

        //    // Проверка должности
        //    bool canAssign = template.requiredPersonnel.Any(j => j.id == _worker.JobTitleId);
        //    if (!canAssign)
        //    {
        //        MessageBox.Show("У врача неподходящая должность для выбранного приёма.");
        //        return;
        //    }

        //    // 1. Удаляем шаблон
        //    var deleteResp = await _client.DeleteAsync($"api/reception/delete/{template.id}");
        //    if (!deleteResp.IsSuccessStatusCode)
        //    {
        //        MessageBox.Show("Не удалось удалить шаблон приёма.");
        //        return;
        //    }

        //    // 2. Создаём назначенный приём
        //    var dto = new ReceptionCreateDto
        //    {
        //        date = _day,
        //        startTime = selectedStart,
        //        time = template.time,
        //        departmentId = _department.id,
        //        requiredPersonnelIds = template.requiredPersonnel.Select(j => j.id).ToList(),
        //        personnelIds = new List<int> { _worker.id }
        //    };

        //    var createResp = await _client.PostAsJsonAsync("api/reception/add-dto", dto);
        //    if (createResp.IsSuccessStatusCode) // newReception в этот момент null какого то хрена 
        //    {
        //        MessageBox.Show("Приём успешно назначен.");
        //        ViewReceptionsForm_Load(this, EventArgs.Empty); // Обновить список
        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка при создании приёма.");
        //    }
        //}


    }
}
