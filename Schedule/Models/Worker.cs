using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class Worker : IModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? login { get; set; }
        public string? password { get; set; }
        public int? JobTitleId { get; set; }
        public JobTitle? jobTitle { get; set; }
        public List<Vacation>? vacations { get; set; }
        public List<WorkerWorkSchedule4Day> workSchedules { get; set; }

        [JsonIgnore] public List<Department>? departments { get; set; }
        [JsonIgnore] public List<Reception>? receptions { get; set; }
    }
}
