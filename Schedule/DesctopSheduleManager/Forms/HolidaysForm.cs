using DesctopSheduleManager.Utilities;
using Models;
using System.Data;
using System.Net.Http.Json;

namespace DesctopSheduleManager
{
    public partial class HolidaysForm : Form
    {
        private readonly HttpClient _client;
        private List<Holiday> _holidays = new();

        public HolidaysForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance;
        }

        private async void HolidaysForm_Load(object sender, EventArgs e)
        {
            await LoadHolidaysAsync();
        }

        private async Task LoadHolidaysAsync()
        {
            try
            {
                _holidays = await _client.GetFromJsonAsync<List<Holiday>>("api/holiday/get-all") ?? new();
                dataGridHolidays.DataSource = _holidays.Select(h => new
                {
                    h.id,
                    h.name,
                    date = h.date.ToString("yyyy-MM-dd")
                }).ToList();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки праздников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            dtpDate.Value = DateTime.Today;
        }

        private void dataGridHolidays_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridHolidays.CurrentRow == null || dataGridHolidays.CurrentRow.Index < 0) return;

            int index = dataGridHolidays.CurrentRow.Index;
            if (index >= _holidays.Count) return;

            var selected = _holidays[index];
            txtName.Text = selected.name;
            dtpDate.Value = selected.date.ToDateTime(TimeOnly.MinValue);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название праздника не может быть пустым.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var holiday = new Holiday
            {
                name = txtName.Text.Trim(),
                date = DateOnly.FromDateTime(dtpDate.Value)
            };

            try
            {
                var response = await _client.PostAsJsonAsync("api/holiday/add", holiday);
                if (response.IsSuccessStatusCode)
                {
                    await LoadHolidaysAsync();
                    MessageBox.Show("Праздник успешно добавлен.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при добавлении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Сетевая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridHolidays.CurrentRow == null)
            {
                MessageBox.Show("Выберите праздник для обновления.");
                return;
            }

            int index = dataGridHolidays.CurrentRow.Index;
            if (index >= _holidays.Count) return;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название праздника не может быть пустым.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = _holidays[index].id;

            var updatedHoliday = new Holiday
            {
                id = id,
                name = txtName.Text.Trim(),
                date = DateOnly.FromDateTime(dtpDate.Value)
            };

            try
            {
                var response = await _client.PutAsJsonAsync($"api/holiday/update/{id}", updatedHoliday);
                if (response.IsSuccessStatusCode)
                {
                    await LoadHolidaysAsync();
                    MessageBox.Show("Праздник обновлён.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при обновлении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Сетевая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridHolidays.CurrentRow == null)
            {
                MessageBox.Show("Выберите праздник для удаления.");
                return;
            }

            int index = dataGridHolidays.CurrentRow.Index;
            if (index >= _holidays.Count) return;

            var id = _holidays[index].id;

            var confirm = MessageBox.Show("Удалить выбранный праздник?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var response = await _client.DeleteAsync($"api/holiday/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadHolidaysAsync();
                    MessageBox.Show("Праздник удалён.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при удалении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Сетевая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadHolidaysAsync();
        }
    }
}
