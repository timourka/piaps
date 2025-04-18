using DesctopSheduleManager.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            _holidays = await _client.GetFromJsonAsync<List<Holiday>>("api/holiday/get-all");
            dataGridHolidays.DataSource = _holidays.Select(h => new
            {
                h.id,
                h.name,
                date = h.date.ToString("yyyy-MM-dd")
            }).ToList();

            ClearInputs();
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
            var holiday = new Holiday
            {
                name = txtName.Text,
                date = DateOnly.FromDateTime(dtpDate.Value)
            };

            await _client.PostAsJsonAsync("api/holiday/add", holiday);
            await LoadHolidaysAsync();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridHolidays.CurrentRow == null) return;
            int index = dataGridHolidays.CurrentRow.Index;
            if (index >= _holidays.Count) return;

            var id = _holidays[index].id;

            var updatedHoliday = new Holiday
            {
                id = id,
                name = txtName.Text,
                date = DateOnly.FromDateTime(dtpDate.Value)
            };

            await _client.PutAsJsonAsync($"api/holiday/update/{id}", updatedHoliday);
            await LoadHolidaysAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridHolidays.CurrentRow == null) return;
            int index = dataGridHolidays.CurrentRow.Index;
            if (index >= _holidays.Count) return;

            var id = _holidays[index].id;

            var result = MessageBox.Show("Удалить выбранный праздник?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await _client.DeleteAsync($"api/holiday/delete/{id}");
                await LoadHolidaysAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadHolidaysAsync();
        }
    }
}
