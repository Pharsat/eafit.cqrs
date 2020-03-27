namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class Jurisdiction : Entity
    {
        public string Name { get; }

        public Jurisdiction() => Name = default!;

        public Jurisdiction(string name) => Name = name;
    }
}