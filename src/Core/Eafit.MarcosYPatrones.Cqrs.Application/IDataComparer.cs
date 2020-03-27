using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using System.Collections.Generic;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface IDataComparer
    {
        IDictionary<string, string> ConvertObjectToPropertiesDictionary<TEntity>(TEntity entity) where TEntity : IEntity;

        IDictionary<string, string> GetChangedValues<TEntity>(TEntity curentEntity, IDictionary<string, string> cleanValues) where TEntity : IEntity;

        DynamicParameters TransformPropertiesToDynamicParameters(IDictionary<string, string> properties);
    }
}
