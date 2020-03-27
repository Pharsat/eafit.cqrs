using System.Collections.Generic;

namespace Eafit.MarcosYPatrones.Cqrs.Domain
{
    public class EntityAction
    {
        public string EntityTypeName { get; set; }

        public IEntity Entity { get; }

        public EntityState EntityState { get; }

        public IDictionary<string, string> Changes { get; }

        public EntityAction(IEntity entity, EntityState entityState, string entityTypeName, IDictionary<string, string> changes)
        {
            Entity = entity;
            EntityState = entityState;
            Changes = changes;
            EntityTypeName = entityTypeName;
        }

        public EntityAction(IEntity entity, EntityState entityState, string entityTypeName)
        {
            Entity = entity;
            EntityState = entityState;
            Changes = new Dictionary<string, string>();
            EntityTypeName = entityTypeName;
        }
    }
}
