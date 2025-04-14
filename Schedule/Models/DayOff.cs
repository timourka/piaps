
using Models.Interfaces;

namespace Models
{
    public class DayOff : IDayOff
    {
        public DateOnly date { get; set; }
        public int id { get; set; }
    }
}
