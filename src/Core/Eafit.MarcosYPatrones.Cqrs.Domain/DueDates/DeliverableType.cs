namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class DeliverableType : Entity
    {
        public string Name { get; }

        public DeliverableType() => Name = default!;

        public DeliverableType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
