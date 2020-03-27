using System;
using System.Collections.Generic;
using System.Reflection;
using Eafit.MarcosYPatrones.Cqrs.Domain;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.EntityMapping
{
    public class EntityPropertiesProvider : IEntityPropertiesProvider
    {
        private readonly IDictionary<string, IEnumerable<PropertyInfo>> _propertiesCollection;

        public EntityPropertiesProvider() => _propertiesCollection = new Dictionary<string, IEnumerable<PropertyInfo>>();

        public IEnumerable<PropertyInfo> GetPropertiesFrom<TEntity>(TEntity entity) where TEntity : IEntity
        {
            return GetPropertiesFrom(entity.GetType());
        }

        public IEnumerable<PropertyInfo> GetPropertiesFrom(Type entityType)
        {
            var entityTypeName = entityType.Name;

            if (_propertiesCollection.TryGetValue(entityTypeName, out var entityProperties))
            {
                return entityProperties;
            }

            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            _propertiesCollection.Add(entityTypeName, properties);

            return properties;
        }
    }
}
