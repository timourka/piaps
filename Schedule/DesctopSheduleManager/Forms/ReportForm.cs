using ClosedXML.Excel;
using DesctopSheduleManager.Utilities;
using Models;
using System.Net.Http.Json;

namespace DesctopSheduleManager
{
    public partial class ReportForm : Form
    {
        private readonly HttpClient _client;
        private List<Department> _departments = new();

        public ReportForm()
        {
            InitializeComponent();
            _client = ApiClient.Instance;
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            await LoadDepartments();
        }

        private async Task LoadDepartments()
        {
            _departments = await _client.GetFromJsonAsync<List<Department>>("api/department/get-all");

            cmbDepartment.Items.Clear();
            foreach (var dept in _departments)
                cmbDepartment.Items.Add(dept);

            cmbDepartment.DisplayMember = "name"; // отображается имя
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedItem is not Department selectedDept)
            {
                MessageBox.Show("Выберите департамент.");
                return;
            }

            var start = DateOnly.FromDateTime(dtpStart.Value);
            var end = DateOnly.FromDateTime(dtpEnd.Value);

            try
            {
                var startStr = start.ToString("yyyy-MM-dd");
                var endStr = end.ToString("yyyy-MM-dd");

                var query = $"api/department/get-schedule-for-period?departmentId={selectedDept.id}&start={startStr}&end={endStr}";
                var response = await ApiClient.Instance.GetAsync(query);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка: {response.StatusCode}\n{errorContent}");
                    return;
                }

                var data = await response.Content.ReadFromJsonAsync<List<Reception>>();
                if (data != null)
                {
                    dataGridReport.DataSource = data.Select(r => new
                    {
                        r.id,
                        Дата = r.date?.ToString("yyyy-MM-dd"),
                        Начало = r.startTime?.ToString(),
                        Конец = r.endTime?.ToString(),
                        Отделение = r.department?.name,
                        ВремяПриёма = r.time.ToString(@"hh\:mm"),
                        Персонал = r.personnel != null ? string.Join(", ", r.personnel.Select(p => p.name)) : ""
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запроса:\n{ex.Message}");
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли данные в DataGridView
            if (dataGridReport.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            // Создание нового Excel документа
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Отчёт");

                // Заголовки для столбцов
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Дата";
                worksheet.Cell(1, 3).Value = "Начало";
                worksheet.Cell(1, 4).Value = "Конец";
                worksheet.Cell(1, 5).Value = "Отделение";
                worksheet.Cell(1, 6).Value = "Время Приёма";
                worksheet.Cell(1, 7).Value = "Персонал";

                // Заполнение данных из DataGridView в Excel
                int row = 2;
                foreach (DataGridViewRow gridRow in dataGridReport.Rows)
                {
                    if (gridRow.IsNewRow) continue; // Пропускаем строку добавления

                    worksheet.Cell(row, 1).Value = gridRow.Cells["id"].Value.ToString();
                    worksheet.Cell(row, 2).Value = gridRow.Cells["Дата"].Value.ToString();
                    worksheet.Cell(row, 3).Value = gridRow.Cells["Начало"].Value.ToString();
                    worksheet.Cell(row, 4).Value = gridRow.Cells["Конец"].Value.ToString();
                    worksheet.Cell(row, 5).Value = gridRow.Cells["Отделение"].Value.ToString();
                    worksheet.Cell(row, 6).Value = gridRow.Cells["ВремяПриёма"].Value.ToString();
                    worksheet.Cell(row, 7).Value = gridRow.Cells["Персонал"].Value.ToString();
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                // Сохранение файла
                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = "Отчёт.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Файл успешно сохранён.");
                    }
                }
            }
        }

    }
}
