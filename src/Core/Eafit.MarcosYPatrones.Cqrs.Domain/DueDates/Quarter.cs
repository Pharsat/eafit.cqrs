namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class Quarter : Entity
    {
        public string Name { get; }

        public Quarter() => Name = default!;

        public Quarter(int id, string name) => (Id, Name) = (id, name);
    }
}
