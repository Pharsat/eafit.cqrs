using Eafit.MarcosYPatrones.Cqrs.Domain;
using System.Collections.Generic;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public class CleanEntity<TEntity> where TEntity : IEntity
    {
        public TEntity Entity { get; }

        public IDictionary<string, string> CleanProperties { get; private set; }

        public CleanEntity(TEntity entity, IDictionary<string, string> cleanProperties)
        {
            Entity = entity;
            CleanProperties = cleanProperties;
        }

        public void RefreshCleanProperties(IDictionary<string, string> cleanProperties)
        {
            CleanProperties = cleanProperties;
        }
    }
}
