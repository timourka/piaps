using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class Vacation : IModel
    {
        public int id { get; set; }
        public List<DayOff> days {  get; set; } 

        public int WorkerId {  get; set; }
        [JsonIgnore] public Worker Worker { get; set; }
    }
}
