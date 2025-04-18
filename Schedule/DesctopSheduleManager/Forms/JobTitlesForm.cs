using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;
using System.Xml.Linq;

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
            _jobTitles = await _client.GetFromJsonAsync<List<JobTitle>>("api/job-title/get-all");

            dataGridJobTitles.DataSource = _jobTitles.Select(j => new
            {
                j.id,
                j.name
            }).ToList();

            ClearInputs();
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
            var jobTitle = new JobTitle
            {
                name = txtName.Text
            };

            await _client.PostAsJsonAsync("api/job-title/add", jobTitle);
            await LoadJobTitlesAsync();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridJobTitles.CurrentRow == null) return;
            int index = dataGridJobTitles.CurrentRow.Index;
            if (index >= _jobTitles.Count) return;

            var id = _jobTitles[index].id;

            var updatedJobTitle = new JobTitle
            {
                id = id,
                name = txtName.Text
            };

            await _client.PutAsJsonAsync($"api/job-title/update/{id}", updatedJobTitle);
            await LoadJobTitlesAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridJobTitles.CurrentRow == null) return;
            int index = dataGridJobTitles.CurrentRow.Index;
            if (index >= _jobTitles.Count) return;

            var id = _jobTitles[index].id;

            var result = MessageBox.Show("Удалить выбранную должность?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await _client.DeleteAsync($"api/job-title/delete/{id}");
                await LoadJobTitlesAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadJobTitlesAsync();
        }
    }
}
