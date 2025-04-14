using Models.Interfaces;

namespace Models
{
    public class Vacation : IModel
    {
        public int id { get; set; }
        public List<DayOff> days {  get; set; } 
    }
}
