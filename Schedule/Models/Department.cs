using Models.Interfaces;

namespace Models
{
    public class Department : IModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public List<WorkSchedule4Day?> workSchedules { get; set; } = new List<WorkSchedule4Day?>(7);
        public List<Worker>? workers { get; set; }
    }
}
