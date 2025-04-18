using System.Text.Json;
using System.Text;
using DesctopSheduleManager.Forms;
using DesctopSheduleManager.Utilities;

namespace DesctopSheduleManager
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }
        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                lblStatus.Text = "Введите логин и пароль";
                return;
            }

            var request = new
            {
                Login = login,
                Password = password
            };

            var client = ApiClient.Instance;

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var json = JsonDocument.Parse(responseContent);
                    var sid = json.RootElement.GetProperty("sid").GetString();

                    SessionManager.SID = sid;

                    // перейти к главной форме или сообщить об успехе
                    MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    var mainForm = new MainForm(); // например, главная форма
                    mainForm.Show();
                }
                else
                {
                    lblStatus.Text = "Неверный логин или пароль";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}
