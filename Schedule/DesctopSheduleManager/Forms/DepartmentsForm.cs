using DesctopSheduleManager.Forms;
using DesctopSheduleManager.Utilities;
using DocumentFormat.OpenXml.Wordprocessing;
using Models;
using System.Net.Http.Json;
using System.Text.Json;


namespace DesctopSheduleManager
{
    public partial class DepartmentsForm : Form
    {
        private readonly HttpClient _client;
        private List<Department> _departments = new();
        private List<Worker> _allWorkers = new();

        public DepartmentsForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance;
        }
        private async Task LoadAllWorkersAsync()
        {
            try
            {
                _allWorkers = await _client.GetFromJsonAsync<List<Worker>>("api/worker/get-all") ?? new();
                comboBoxWorkers.DataSource = _allWorkers;
                comboBoxWorkers.DisplayMember = "name";
                comboBoxWorkers.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки персонала: {ex.Message}");
            }
        }


        private async Task LoadDepartmentsAsync()
        {
            try
            {
                var departments = await _client.GetFromJsonAsync<List<Department>>("api/department/get-all");

                if (departments != null)
                {
                    _departments = departments;
                    dataGridDepartments.DataSource = _departments.Select(d => new
                    {
                        d.id,
                        d.name,
                        WorkersCount = d.workers?.Count ?? 0
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
        private void LoadDepartmentWorkers()
        {
            if (dataGridDepartments.CurrentRow == null) return;

            var selectedDepartment = _departments[dataGridDepartments.CurrentRow.Index];
            if (selectedDepartment == null) return;

            var workers = selectedDepartment.workers ?? new List<Worker>();

            dataGridViewWorkers.DataSource = workers.Select(w => new
            {
                w.id,
                w.name,
                Должность = w.jobTitle?.name
            }).ToList();
        }

        private void dataGridDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;
            txtName.Text = dataGridDepartments.CurrentRow.Cells["name"].Value?.ToString();
            LoadDepartmentWorkers(); // <-- обновляем работников
        }


        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название отделения.");
                return;
            }

            var newDepartment = new Department { name = txtName.Text };

            try
            {
                var response = await _client.PostAsJsonAsync("api/department/add", newDepartment);
                if (response.IsSuccessStatusCode)
                {
                    await LoadDepartmentsAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка добавления: {response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к API: {ex.Message}");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название отделения.");
                return;
            }

            var id = (int)dataGridDepartments.CurrentRow.Cells["id"].Value;

            var updatedDepartment = new Department
            {
                id = id,
                name = txtName.Text
            };

            try
            {
                var response = await _client.PutAsJsonAsync($"api/department/update/{id}", updatedDepartment);
                if (response.IsSuccessStatusCode)
                {
                    await LoadDepartmentsAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка обновления: {response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к API: {ex.Message}");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;

            var id = (int)dataGridDepartments.CurrentRow.Cells["id"].Value;

            var result = MessageBox.Show("Удалить выбранный департамент?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            try
            {
                var response = await _client.DeleteAsync($"api/department/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadDepartmentsAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка удаления: {response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к API: {ex.Message}");
            }
        }

        private async void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null)
            {
                MessageBox.Show("Выберите отделение.");
                return;
            }

            var id = (int)dataGridDepartments.CurrentRow.Cells["id"].Value;
            var result = MessageBox.Show($"Сгенерировать расписание для отделения ID {id}?", "Подтверждение", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes) return;

            try
            {
                var response = await _client.PostAsync($"api/department/generate-schedule/{id}", null);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Расписание успешно сгенерировано.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка генерации: {response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к API: {ex.Message}");
            }
        }

        private async void DepartmentsForm_Load(object sender, EventArgs e)
        {
            await LoadDepartmentsAsync();
            await LoadAllWorkersAsync();
        }

        private async void buttonChangeShedule_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null)
            {
                MessageBox.Show("Выберите отделение.");
                return;
            }

            var selectedDepartment = _departments[dataGridDepartments.CurrentRow.Index];
            if (selectedDepartment == null)
            {
                MessageBox.Show("Не удалось получить отделение.");
                return;
            }

            var editorForm = new ScheduleEditorForm(selectedDepartment.workSchedules);

            if (editorForm.ShowDialog() == DialogResult.OK)
            {
                var updatedSchedule = editorForm.UpdatedSchedule;
                foreach (var ws in updatedSchedule)
                {
                    if (ws != null)
                        ws.DepartmentId = selectedDepartment.id;
                }

                selectedDepartment.workSchedules = updatedSchedule;

                try
                {
                    var response = await _client.PutAsJsonAsync(
                        $"api/department/update/{selectedDepartment.id}", selectedDepartment);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Расписание обновлено.");
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Ошибка обновления: {response.StatusCode}\n{error}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к API:\n{ex.Message}");
                }
            }
        }

        private void buttonShedule_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null)
            {
                MessageBox.Show("Выберите отделение.");
                return;
            }

            var selectedDepartment = _departments[dataGridDepartments.CurrentRow.Index];
            if (selectedDepartment == null)
            {
                MessageBox.Show("Не удалось получить отделение.");
                return;
            }

            var sheduleForm = new SheduleForm(selectedDepartment.id);
            sheduleForm.ShowDialog();
        }
        private async void buttonAddWorker_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null)
            {
                MessageBox.Show("Выберите отделение.");
                return;
            }

            var selectedDepartment = _departments[dataGridDepartments.CurrentRow.Index];
            if (selectedDepartment == null)
            {
                MessageBox.Show("Не удалось получить отделение.");
                return;
            }

            var selectedWorker = comboBoxWorkers.SelectedItem as Worker;
            if (selectedWorker == null)
            {
                MessageBox.Show("Выберите работника для добавления.");
                return;
            }

            // Проверим, не добавлен ли уже
            if (selectedDepartment.workers?.Any(w => w.id == selectedWorker.id) == true)
            {
                MessageBox.Show("Работник уже добавлен в это отделение.");
                return;
            }

            selectedDepartment.workers ??= new List<Worker>();
            selectedDepartment.workers.Add(selectedWorker);

            try
            {
                var response = await _client.PutAsJsonAsync(
                    $"api/department/update/{selectedDepartment.id}", selectedDepartment);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Работник добавлен в отделение.");
                    await LoadDepartmentsAsync(); // перезагружаем отделения с обновленными данными
                    LoadDepartmentWorkers(); // обновляем отображение
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка обновления: {response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к API: {ex.Message}");
            }
        }

    }
}
