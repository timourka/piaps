using Models.Interfaces;

namespace Models
{
    public class Reception : IModel
    {
        public int id { get; set; }
        /// <summary>
        /// время на приём
        /// </summary>
        public TimeSpan time { get; set; }
        /// <summary>
        /// персонал необходимый для проведения
        /// </summary>
        public List<JobTitle> requiredPersonnel { get; set; }
        /// <summary>
        /// отделение приёма
        /// </summary>
        public Department department { get; set; }

        //----------------------- после составления расписания
        /// <summary>
        /// день приёма
        /// </summary>
        public DateOnly? date { get; set; }
        /// <summary>
        /// время начала приёма
        /// </summary>
        public TimeOnly? startTime {  get; set; }
        /// <summary>
        /// время конца приёма 
        /// </summary>
        public TimeOnly? endTime => startTime.HasValue ? startTime.Value.Add(time) : null;
        /// <summary>
        /// работники приписанные к приёму 
        /// </summary>
        public List<Worker>? personnel { get; set; }
    }
}
