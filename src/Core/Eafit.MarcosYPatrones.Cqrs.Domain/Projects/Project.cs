namespace Eafit.MarcosYPatrones.Cqrs.Domain.Projects
{
    public class Project : Entity
    {
        public string Name { get; }
        
        public Project()
        {
            Name = string.Empty;
        }
        public Project(string name)
        {
            Name = name;
        }
    }
}