using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class WorkSchedule4Day : IModel
    {
        public int id { get; set; }
        public TimeOnly startOfWork { get; set; }
        public TimeOnly endOfWork { get; set; }
        public bool isWorking { get; set; }

        public int DepartmentId { get; set; }            // 🔥 Добавлено
        [JsonIgnore] public Department? Department { get; set; }       // 🔥 Добавлено
    }
}
