﻿using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace DesctopSheduleManager
{
    public partial class DepartmentsForm : Form
    {
        private readonly HttpClient _client;
        private List<Department> _departments = new List<Department>(); // Список департаментов

        public DepartmentsForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance; // Используем ваш ApiClient
        }

        // Загружаем департаменты
        private async Task LoadDepartmentsAsync()
        {
            var departments = await _client.GetFromJsonAsync<List<Department>>("api/department/get-all");

            if (departments != null)
            {
                _departments = departments;

                dataGridDepartments.DataSource = _departments.Select(d => new
                {
                    d.id,
                    d.name,
                    WorkersCount = d.workers.Count
                }).ToList();
            }
        }

        // Загружаем департамент по ID для редактирования
        private void dataGridDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;

            var selectedRow = dataGridDepartments.CurrentRow;

            txtName.Text = selectedRow.Cells["name"].Value.ToString();
        }

        // Добавляем новый департамент
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var newDepartment = new Department
            {
                name = txtName.Text
            };

            await _client.PostAsJsonAsync("api/department/add", newDepartment);
            await LoadDepartmentsAsync(); // Обновляем список департаментов
        }

        // Обновляем департамент
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;

            var id = (int)dataGridDepartments.CurrentRow.Cells["id"].Value;

            var updatedDepartment = new Department
            {
                id = id,
                name = txtName.Text
            };

            await _client.PutAsJsonAsync($"api/department/update/{id}", updatedDepartment);
            await LoadDepartmentsAsync(); // Обновляем список департаментов
        }

        // Удаляем департамент
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;

            var id = (int)dataGridDepartments.CurrentRow.Cells["id"].Value;

            var result = MessageBox.Show("Удалить выбранный департамент?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await _client.DeleteAsync($"api/department/delete/{id}");
                await LoadDepartmentsAsync(); // Обновляем список департаментов
            }
        }

        // Генерация расписания для департамента
        private async void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            if (dataGridDepartments.CurrentRow == null) return;

            var id = (int)dataGridDepartments.CurrentRow.Cells["id"].Value;

            var result = MessageBox.Show($"Вы уверены, что хотите сгенерировать расписание для департамента с ID {id}?",
                "Подтверждение", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var response = await _client.PostAsync($"api/department/generate-schedule/{id}", null);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Расписание для департамента успешно сгенерировано.", "Успех");
                }
                else
                {
                    MessageBox.Show("Ошибка при генерации расписания.", "Ошибка");
                }
            }
        }

        // Обработчик события загрузки формы
        private async void DepartmentsForm_Load(object sender, EventArgs e)
        {
            await LoadDepartmentsAsync(); // Загружаем департаменты
        }
    }
}
