namespace Models.DTO
{
    public class ReceptionCreateDto
    {
        public DateOnly date { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeSpan time { get; set; }

        public int departmentId { get; set; }
        public List<int> requiredPersonnelIds { get; set; }
        public List<int> personnelIds { get; set; }
    }

}
