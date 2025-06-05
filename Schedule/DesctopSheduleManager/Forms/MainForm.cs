using DesctopSheduleManager.Utilities;

namespace DesctopSheduleManager.Forms
{
    public partial class MainForm : Form
    {
        private readonly HttpClient _client;

        public MainForm()
        {
            InitializeComponent();

            _client = ApiClient.Instance;

            if (!string.IsNullOrEmpty(SessionManager.SID))
            {
                _client.DefaultRequestHeaders.Add("sid", SessionManager.SID);
            }
        }
        private void btnWorkers_Click(object sender, EventArgs e)
        {
            var form = new WorkersForm();
            form.ShowDialog();
        }
        private void btnDepartment_Click(object sender, EventArgs e)
        {
            var form = new DepartmentsForm();
            form.ShowDialog();
        }
        private void btnReceptions_Click(object sender, EventArgs e)
        {
            var form = new ReceptionsForm();
            form.ShowDialog();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            var form = new ReportForm();
            form.ShowDialog();
        }
        private void btnJobTitles_Click(object sender, EventArgs e)
        {
            var form = new JobTitlesForm();
            form.ShowDialog();
        }
        private void btnHolidays_Click(object sender, EventArgs e)
        {
            var form = new HolidaysForm();
            form.ShowDialog();
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/logout");
                request.Headers.Add("sid", SessionManager.SID);
                await _client.SendAsync(request);
            }
            catch
            {
                // Можно добавить логирование
            }

            Application.Exit();
        }

        private void buttonStats_Click(object sender, EventArgs e)
        {
            var form = new StatsForm();
            form.ShowDialog();
        }
    }
}
