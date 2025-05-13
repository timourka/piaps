using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class JobTitle : IModel
    {
        public int id { get; set; }
        public string? name { get; set; }

        [JsonIgnore] public List<Reception>? receptions { get; set; }
    }
}
