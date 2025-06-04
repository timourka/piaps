using DesctopSheduleManager.Forms;
using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;

namespace DesctopSheduleManager
{
    public partial class WorkersForm : Form
    {
        private readonly HttpClient _client;
        private List<JobTitle> _jobTitles = new List<JobTitle>(); // Список должностей

        public WorkersForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance;
        }

        // Загружаем список должностей
        private async Task LoadJobTitlesAsync()
        {
            // Получаем все должности с API
            var jobTitles = await _client.GetFromJsonAsync<List<JobTitle>>("api/job-title/get-all");

            if (jobTitles != null)
            {
                _jobTitles = jobTitles;

                // Заполняем ComboBox объектами JobTitle
                cmbJobTitle.DataSource = _jobTitles;
                cmbJobTitle.DisplayMember = "name"; // Показываем название должности
                cmbJobTitle.ValueMember = "id"; // Сохраняем ID должности
            }
        }

        // Вызываем загрузку данных при загрузке формы
        private async void WorkersForm_Load(object sender, EventArgs e)
        {
            await LoadJobTitlesAsync(); // Загружаем должности
            await LoadWorkersAsync(); // Загружаем сотрудников
        }

        private async Task LoadWorkersAsync()
        {
            // Загружаем список работников
            var workers = await _client.GetFromJsonAsync<List<Worker>>("api/worker/get-all");

            dataGridWorkers.DataSource = workers?.Select(w => new
            {
                w.id,
                w.name,
                w.login,
                JobTitle = w.jobTitle?.name // Показываем название должности
            }).ToList();
        }

        // Обработчик добавления нового сотрудника
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedJobTitle = (JobTitle)cmbJobTitle.SelectedItem; // Получаем выбранную должность
            if (selectedJobTitle == null) return;

            var newWorker = new Worker
            {
                name = txtName.Text,
                login = txtLogin.Text,
                password = txtPassword.Text,
                jobTitle = selectedJobTitle // Используем объект JobTitle с id
            };

            // Отправляем POST запрос на добавление нового работника
            await _client.PostAsJsonAsync("api/worker/add", newWorker);
            await LoadWorkersAsync(); // Обновляем список работников
        }

        // Обработчик обновления сотрудника
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null) return;
            int index = dataGridWorkers.CurrentRow.Index;
            if (index < 0) return;

            var id = (int)dataGridWorkers.CurrentRow.Cells["id"].Value;
            var selectedJobTitle = (JobTitle)cmbJobTitle.SelectedItem;

            if (selectedJobTitle == null) return;

            var updatedWorker = new Worker
            {
                id = id,
                name = txtName.Text,
                login = txtLogin.Text,
                password = txtPassword.Text,
                jobTitle = selectedJobTitle
            };

            // Отправляем PUT запрос на обновление данных сотрудника
            await _client.PutAsJsonAsync($"api/worker/update/{id}", updatedWorker);
            await LoadWorkersAsync(); // Обновляем список работников
        }

        // Обработчик удаления сотрудника
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null) return;

            var id = (int)dataGridWorkers.CurrentRow.Cells["id"].Value;

            var result = MessageBox.Show("Удалить выбранного сотрудника?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await _client.DeleteAsync($"api/worker/delete/{id}");
                await LoadWorkersAsync(); // Обновляем список работников
            }
        }

        // Обработчик выбора строки в DataGridView
        private void dataGridWorkers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null) return;

            var selectedRow = dataGridWorkers.CurrentRow;

            txtName.Text = selectedRow.Cells["name"].Value.ToString();
            txtLogin.Text = selectedRow.Cells["login"].Value.ToString();

            // Присваиваем ComboBox выбранную должность
            var jobTitle = _jobTitles.FirstOrDefault(j => j.name == selectedRow.Cells["JobTitle"].Value.ToString());
            cmbJobTitle.SelectedItem = jobTitle;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadWorkersAsync();
        }

        private async void buttonNotificationTest_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите работника.");
                return;
            }

            // Предполагаем, что id хранится в первой колонке
            int selectedWorkerId = Convert.ToInt32(dataGridWorkers.SelectedRows[0].Cells["id"].Value);

            try
            {
                var response = await _client.GetAsync(
                    $"api/worker/test-notification/{selectedWorkerId}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Уведомление успешно отправлено.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"❌ Ошибка при отправке уведомления:\n{errorContent}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠️ Ошибка запроса:\n{ex.Message}");
            }
        }

        private async void buttonChangeShedule_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null)
            {
                MessageBox.Show("Выберите сотрудника.");
                return;
            }

            int id = (int)dataGridWorkers.CurrentRow.Cells["id"].Value;

            var worker = await _client.GetFromJsonAsync<Worker>($"api/worker/get/{id}");
            if (worker == null)
            {
                MessageBox.Show("Работник не найден.");
                return;
            }

            var scheduleEditor = new WorkerScheduleEditorForm(worker.workSchedules); // создайте такую форму
            if (scheduleEditor.ShowDialog() == DialogResult.OK)
            {
                worker.workSchedules = scheduleEditor.UpdatedSchedule;

                var response = await _client.PutAsJsonAsync($"api/worker/update/{id}", worker);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Расписание обновлено.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"❌ Ошибка при обновлении:\n{error}");
                }
            }
        }

        private async void buttonChangeVacations_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null)
            {
                MessageBox.Show("Выберите сотрудника.");
                return;
            }

            int id = (int)dataGridWorkers.CurrentRow.Cells["id"].Value;

            var worker = await _client.GetFromJsonAsync<Worker>($"api/worker/get/{id}");
            if (worker == null)
            {
                MessageBox.Show("Работник не найден.");
                return;
            }

            var vacationEditor = new VacationEditorForm(worker.vacations); // создайте такую форму
            if (vacationEditor.ShowDialog() == DialogResult.OK)
            {
                worker.vacations = vacationEditor.UpdatedVacations;

                var response = await _client.PutAsJsonAsync($"api/worker/update/{id}", worker);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Отпуска обновлены.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"❌ Ошибка при обновлении:\n{error}");
                }
            }
        }
    }
}
