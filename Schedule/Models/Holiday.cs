using Models.Interfaces;

namespace Models
{
    public class Holiday : IDayOff
    {
        public int id { get; set; }
        public DateOnly date { get; set; }
        public string name { get; set; }
    }
}
