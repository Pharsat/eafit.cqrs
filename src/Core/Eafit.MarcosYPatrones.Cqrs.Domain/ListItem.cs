namespace Eafit.MarcosYPatrones.Cqrs.Domain
{
    public class ListItem : Entity
    {
        public string Name { get; set; }
        public ListItem() => Name = string.Empty;
        public ListItem(string name) => Name = name;
    }
}
