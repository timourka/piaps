using Models.Interfaces;

namespace Models
{
    public class JobTitle : IModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
