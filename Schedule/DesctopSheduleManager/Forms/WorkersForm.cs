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
            txtPassword.UseSystemPasswordChar = true;
        }

        // Загружаем список должностей
        private async Task LoadJobTitlesAsync()
        {
            try
            {
                var jobTitles = await _client.GetFromJsonAsync<List<JobTitle>>("api/job-title/get-all");

                if (jobTitles != null)
                {
                    _jobTitles = jobTitles;
                    cmbJobTitle.DataSource = _jobTitles;
                    cmbJobTitle.DisplayMember = "name";
                    cmbJobTitle.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки должностей:\n{ex.Message}");
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
            try
            {
                var workers = await _client.GetFromJsonAsync<List<Worker>>("api/worker/get-all");

                if (workers != null)
                {
                    dataGridWorkers.DataSource = workers.Select(w => new
                    {
                        w.id,
                        w.name,
                        w.login,
                        JobTitle = w.jobTitle?.name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников:\n{ex.Message}");
            }
        }


        // Обработчик добавления нового сотрудника
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                cmbJobTitle.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите должность.");
                return;
            }

            var selectedJobTitle = (JobTitle)cmbJobTitle.SelectedItem;

            var newWorker = new Worker
            {
                name = txtName.Text.Trim(),
                login = txtLogin.Text.Trim(),
                password = txtPassword.Text,
                jobTitle = selectedJobTitle,
                workSchedules = new()
            };

            try
            {
                var response = await _client.PostAsJsonAsync("api/worker/add", newWorker);
                if (response.IsSuccessStatusCode)
                {
                    await LoadWorkersAsync();
                    MessageBox.Show("Сотрудник успешно добавлен.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при добавлении:\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}");
            }
        }


        // Обработчик обновления сотрудника
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null)
            {
                MessageBox.Show("Выберите сотрудника для обновления.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                cmbJobTitle.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите должность.");
                return;
            }

            var id = (int)dataGridWorkers.CurrentRow.Cells["id"].Value;
            var selectedJobTitle = (JobTitle)cmbJobTitle.SelectedItem;

            var updatedWorker = new Worker
            {
                id = id,
                name = txtName.Text.Trim(),
                login = txtLogin.Text.Trim(),
                password = txtPassword.Text,
                jobTitle = selectedJobTitle
            };

            try
            {
                var response = await _client.PutAsJsonAsync($"api/worker/update/{id}", updatedWorker);
                if (response.IsSuccessStatusCode)
                {
                    await LoadWorkersAsync();
                    MessageBox.Show("Сотрудник успешно обновлён.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при обновлении:\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:\n{ex.Message}");
            }
        }


        // Обработчик удаления сотрудника
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridWorkers.CurrentRow == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
                return;
            }

            var id = (int)dataGridWorkers.CurrentRow.Cells["id"].Value;

            if (MessageBox.Show("Удалить выбранного сотрудника?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var response = await _client.DeleteAsync($"api/worker/delete/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadWorkersAsync();
                        MessageBox.Show("Сотрудник удалён.");
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Ошибка при удалении:\n{error}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка:\n{ex.Message}");
                }
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

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBoxShowPass.Checked;
        }
    }
}
