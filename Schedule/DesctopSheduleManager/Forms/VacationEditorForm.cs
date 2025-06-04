using Models;
using System.ComponentModel;
using System.Windows.Forms;

namespace DesctopSheduleManager.Forms
{
    public partial class VacationEditorForm : Form
    {
        public class VacationPeriod
        {
            public DateOnly Start { get; set; }
            public DateOnly End { get; set; }
        }

        private List<Vacation> _vacations;
        private BindingList<VacationPeriod> _vacationPeriods;


        public List<Vacation?> UpdatedVacations => _vacationPeriods
            .Select(period =>
            {
                var days = Enumerable.Range(0, (period.End.DayNumber - period.Start.DayNumber) + 1)
                    .Select(offset => new DayOff { date = period.Start.AddDays(offset) })
                    .ToList();

                return new Vacation { days = days };
            }).ToList();


        public VacationEditorForm(List<Vacation> existingVacations)
        {
            InitializeComponent();

            // Преобразуем существующие отпуска в пары (Start, End)
            var vacationPeriods = existingVacations?
             .Select(v =>
             {
                 var sortedDays = v.days.OrderBy(d => d.date).ToList();
                 if (sortedDays.Count == 0)
                     return null;

                 return new VacationPeriod
                 {
                     Start = sortedDays.First().date,
                     End = sortedDays.Last().date
                 };
             })
             .Where(p => p != null)
             .ToList() ?? new();

            _vacationPeriods = new BindingList<VacationPeriod>(vacationPeriods);
            dataGridViewDays.DataSource = _vacationPeriods;

            // Настрой заголовки
            dataGridViewDays.Columns["Start"].HeaderText = "Начало отпуска";
            dataGridViewDays.Columns["End"].HeaderText = "Конец отпуска";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DateOnly start = DateOnly.FromDateTime(dateTimePickerStart.Value.Date);
            DateOnly end = DateOnly.FromDateTime(dateTimePickerEnd.Value.Date);

            if (end < start)
            {
                MessageBox.Show("Дата окончания не может быть раньше даты начала.");
                return;
            }

            _vacationPeriods.Add(new VacationPeriod { Start = start, End = end });
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDays.CurrentRow?.DataBoundItem is VacationPeriod selectedPeriod)
            {
                _vacationPeriods.Remove(selectedPeriod);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

}
