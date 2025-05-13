
using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class DayOff : IDayOff
    {
        public DateOnly date { get; set; }
        public int id { get; set; }

        public int VacationId { get; set; }
        [JsonIgnore] public Vacation Vacation { get; set;}
    }
}
