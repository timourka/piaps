using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class WorkerWorkSchedule4Day : IModel
    {
        public int id { get; set; }
        public TimeOnly startOfWork { get; set; }
        public TimeOnly endOfWork { get; set; }
        public bool isWorking { get; set; }

        public int WorkerId { get; set;}
        [JsonIgnore] public Worker? Worker { get; set; }
    }
}
