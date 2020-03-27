namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class Form : Entity
    {
        public string Name { get; }

        public Form() => Name = default!;

        public Form(string name) => Name = name;
    }
}