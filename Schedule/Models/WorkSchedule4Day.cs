using Models.Interfaces;

namespace Models
{
    public class WorkSchedule4Day : IModel
    {
        public int id { get; set; }
        public TimeOnly startOfWork { get; set; }
        public TimeOnly endOfWork { get; set; }
        public bool isWorking { get; set; }
    }
}
