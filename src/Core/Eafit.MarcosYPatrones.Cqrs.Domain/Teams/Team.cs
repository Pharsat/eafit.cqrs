namespace Eafit.MarcosYPatrones.Cqrs.Domain.Teams
{
    public class Team : Entity
    {
        public string Name { get; }

        public Team()
        {
            Name = string.Empty;
        }

        public Team(string name)
        {
            Name = name;
        }
    }
}