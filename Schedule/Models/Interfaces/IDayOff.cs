namespace Models.Interfaces
{
    public interface IDayOff : IModel
    {
        public DateOnly date { get; set; }
    }
}
