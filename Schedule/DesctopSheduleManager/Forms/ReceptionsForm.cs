﻿using DesctopSheduleManager.Utilities;
using Microsoft.VisualBasic;
using Models;
using System.Net.Http.Json;

namespace DesctopSheduleManager
{
    public partial class ReceptionsForm : Form
    {
        private readonly HttpClient _client;
        private List<Reception> _receptions = new List<Reception>();
        private List<JobTitle> _jobTitles = new List<JobTitle>();
        private List<Department> _departments = new List<Department>();
        private List<JobTitle> _selectedJobTitles = new List<JobTitle>(); // Для выбранных должностей

        public ReceptionsForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance; // Используем ваш ApiClient
        }

        // Загружаем данные для комбобоксов (Персонал и Отделение)
        private async Task LoadComboBoxDataAsync()
        {
            _jobTitles = await _client.GetFromJsonAsync<List<JobTitle>>("api/job-title/get-all");
            _departments = await _client.GetFromJsonAsync<List<Department>>("api/department/get-all");

            cmbJobTitle.DataSource = _jobTitles;
            cmbJobTitle.DisplayMember = "name"; // Отображаем имя должности
            cmbJobTitle.ValueMember = "id"; // ID для отправки на сервер

            cmbDepartment.DataSource = _departments;
            cmbDepartment.DisplayMember = "name"; // Отображаем имя департамента
            cmbDepartment.ValueMember = "id"; // ID для отправки на сервер
        }

        // Загружаем список всех приёмов
        private async Task LoadReceptionsAsync()
        {
            var receptions = await _client.GetFromJsonAsync<List<Reception>>("api/reception/get-all");

            if (receptions != null)
            {
                _receptions = receptions;

                dataGridReceptions.DataSource = _receptions.Select(r => new
                {
                    r.id,
                    ВремяПриёма = r.time.ToString(@"hh\:mm"),
                    Отделение = r.department?.name,
                    ТребуемыйПерсонал = r.requiredPersonnel != null ? string.Join(", ", r.requiredPersonnel.Select(p => p.name)) : "",
                    Дата = r.date?.ToString("yyyy-MM-dd") ?? "",
                    Начало = r.startTime?.ToString() ?? "",
                    Конец = r.endTime?.ToString() ?? "",
                    НазначенныйПерсонал = r.personnel != null ? string.Join(", ", r.personnel.Select(p => p.name)) : ""
                }).ToList();
            }
        }

        // Загружаем выбранный приём для редактирования
        private void dataGridReceptions_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridReceptions.CurrentRow == null) return;

            var selectedRow = dataGridReceptions.CurrentRow;
            var selectedReception = _receptions.FirstOrDefault(r => r.id == (int)selectedRow.Cells["id"].Value);

            if (selectedReception == null) return;

            // Заполняем данные
            dateTimePickerTime.Value = DateTime.Today.Add(selectedReception.time);
            cmbDepartment.SelectedValue = selectedReception.department?.id;

            // Заполняем список должностей
            _selectedJobTitles = selectedReception.requiredPersonnel.ToList();
            UpdateJobTitleList(); // Обновляем отображение должностей
        }

        // Обновляем список должностей в списке
        private void UpdateJobTitleList()
        {
            lstJobTitles.Items.Clear();
            foreach (var jobTitle in _selectedJobTitles)
            {
                lstJobTitles.Items.Add(jobTitle.name);
            }
        }

        // Добавляем выбранную должность в список requiredPersonnel
        private void btnAddJobTitle_Click(object sender, EventArgs e)
        {
            var selectedJobTitle = cmbJobTitle.SelectedItem as JobTitle;
            if (selectedJobTitle != null && !_selectedJobTitles.Contains(selectedJobTitle))
            {
                _selectedJobTitles.Add(selectedJobTitle);
                UpdateJobTitleList();
            }
        }

        // Удаляем выбранную должность из списка requiredPersonnel
        private void btnRemoveJobTitle_Click(object sender, EventArgs e)
        {
            var selectedJobTitle = lstJobTitles.SelectedItem as string;
            if (selectedJobTitle != null)
            {
                var jobTitleToRemove = _selectedJobTitles.FirstOrDefault(jt => jt.name == selectedJobTitle);
                if (jobTitleToRemove != null)
                {
                    _selectedJobTitles.Remove(jobTitleToRemove);
                    UpdateJobTitleList();
                }
            }
        }

        // Добавляем новый приём
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите отделение.");
                    return;
                }

                if (_selectedJobTitles.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, добавьте хотя бы одну требуемую должность.");
                    return;
                }

                TimeSpan time = dateTimePickerTime.Value.TimeOfDay;
                if (time == TimeSpan.Zero)
                {
                    MessageBox.Show("Пожалуйста, выберите корректное время.");
                    return;
                }

                var newReception = new Reception
                {
                    time = time,
                    department = new Department { id = (int)cmbDepartment.SelectedValue },
                    requiredPersonnel = _selectedJobTitles
                };

                var response = await _client.PostAsJsonAsync("api/reception/add", newReception);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Прием успешно добавлен.");
                    await LoadReceptionsAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при добавлении: {response.ReasonPhrase}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении: {ex.Message}");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridReceptions.CurrentRow == null)
                {
                    MessageBox.Show("Выберите прием для обновления.");
                    return;
                }

                var id = (int)dataGridReceptions.CurrentRow.Cells["id"].Value;

                if (cmbDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите отделение.");
                    return;
                }

                if (_selectedJobTitles.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, добавьте хотя бы одну требуемую должность.");
                    return;
                }

                TimeSpan time = dateTimePickerTime.Value.TimeOfDay;
                if (time == TimeSpan.Zero)
                {
                    MessageBox.Show("Пожалуйста, выберите корректное время.");
                    return;
                }

                var updatedReception = new Reception
                {
                    id = id,
                    time = time,
                    department = new Department { id = (int)cmbDepartment.SelectedValue },
                    requiredPersonnel = _selectedJobTitles,
                    date = DateOnly.FromDateTime(dtpDate.Value)
                };

                var response = await _client.PutAsJsonAsync($"api/reception/update/{id}", updatedReception);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Прием успешно обновлен.");
                    await LoadReceptionsAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при обновлении: {response.ReasonPhrase}\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении: {ex.Message}");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridReceptions.CurrentRow == null)
                {
                    MessageBox.Show("Выберите прием для удаления.");
                    return;
                }

                var id = (int)dataGridReceptions.CurrentRow.Cells["id"].Value;

                var result = MessageBox.Show("Удалить выбранный приём?", "Подтверждение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var response = await _client.DeleteAsync($"api/reception/delete/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadReceptionsAsync();
                        MessageBox.Show("Прием успешно удален.");
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Ошибка при удалении: {response.ReasonPhrase}\n{error}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}");
            }
        }


        // Обработчик события загрузки формы
        private async void ReceptionsForm_Load(object sender, EventArgs e)
        {
            await LoadComboBoxDataAsync(); // Загружаем данные для комбобоксов
            await LoadReceptionsAsync(); // Загружаем список приёмов
        }
    }
}
