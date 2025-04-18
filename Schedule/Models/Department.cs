using Models.Interfaces;

namespace Models
{
    public class Department : IModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public List<WorkSchedule4Day>? workSchedules { get; set; }
        public List<Worker>? workers { get; set; }
    }
}
