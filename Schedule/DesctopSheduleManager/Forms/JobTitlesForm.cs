using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;

namespace DesctopSheduleManager
{
    public partial class JobTitlesForm : Form
    {
        private readonly HttpClient _client;
        private List<JobTitle> _jobTitles = new();

        public JobTitlesForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance;
        }

        private async void JobTitlesForm_Load(object sender, EventArgs e)
        {
            await LoadJobTitlesAsync();
        }

        private async Task LoadJobTitlesAsync()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<List<JobTitle>>("api/job-title/get-all");
                if (result != null)
                    _jobTitles = result;
                else
                    _jobTitles = new();

                dataGridJobTitles.DataSource = _jobTitles.Select(j => new { j.id, j.name }).ToList();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
        }

        private void dataGridJobTitles_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridJobTitles.CurrentRow == null || dataGridJobTitles.CurrentRow.Index < 0) return;
            int index = dataGridJobTitles.CurrentRow.Index;
            if (index >= _jobTitles.Count) return;

            var selected = _jobTitles[index];
            txtName.Text = selected.name;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название должности не может быть пустым.");
                return;
            }

            var jobTitle = new JobTitle { name = txtName.Text.Trim() };

            try
            {
                var response = await _client.PostAsJsonAsync("api/job-title/add", jobTitle);
                if (response.IsSuccessStatusCode)
                {
                    await LoadJobTitlesAsync();
                    MessageBox.Show("Должность добавлена.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при добавлении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridJobTitles.CurrentRow == null)
            {
                MessageBox.Show("Выберите должность для обновления.");
                return;
            }

            int index = dataGridJobTitles.CurrentRow.Index;
            if (index >= _jobTitles.Count) return;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название должности не может быть пустым.");
                return;
            }

            var id = _jobTitles[index].id;

            var updatedJobTitle = new JobTitle
            {
                id = id,
                name = txtName.Text.Trim()
            };

            try
            {
                var response = await _client.PutAsJsonAsync($"api/job-title/update/{id}", updatedJobTitle);
                if (response.IsSuccessStatusCode)
                {
                    await LoadJobTitlesAsync();
                    MessageBox.Show("Должность обновлена.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при обновлении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridJobTitles.CurrentRow == null)
            {
                MessageBox.Show("Выберите должность для удаления.");
                return;
            }

            int index = dataGridJobTitles.CurrentRow.Index;
            if (index >= _jobTitles.Count) return;

            var id = _jobTitles[index].id;

            var result = MessageBox.Show("Удалить выбранную должность?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            try
            {
                var response = await _client.DeleteAsync($"api/job-title/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadJobTitlesAsync();
                    MessageBox.Show("Должность удалена.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при удалении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadJobTitlesAsync();
        }
    }
}
