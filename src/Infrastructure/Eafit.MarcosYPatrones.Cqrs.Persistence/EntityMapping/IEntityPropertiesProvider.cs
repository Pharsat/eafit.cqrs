using System;
using System.Collections.Generic;
using System.Reflection;
using Eafit.MarcosYPatrones.Cqrs.Domain;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.EntityMapping
{
    public interface IEntityPropertiesProvider
    {
        IEnumerable<PropertyInfo> GetPropertiesFrom<TEntity>(TEntity entity) where TEntity : IEntity;

        IEnumerable<PropertyInfo> GetPropertiesFrom(Type entityType);
    }
}
