using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using Eafit.MarcosYPatrones.Cqrs.Persistence.EntityMapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence
{
    public class DataComparer : IDataComparer
    {
        private readonly IEntityPropertiesProvider _entityPropertiesProvider;

        public DataComparer(IEntityPropertiesProvider entityPropertiesProvider) => _entityPropertiesProvider = entityPropertiesProvider;

        public IDictionary<string, string> ConvertObjectToPropertiesDictionary<TEntity>(TEntity entity) where TEntity : IEntity
        {
            var dictionaryResult = new Dictionary<string, string>();
            foreach (var property in _entityPropertiesProvider.GetPropertiesFrom(entity))
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsPrimitive || propertyType == typeof(string) || propertyType == typeof(DateTime))
                {
                    string propertyName = property.Name;
                    string propertyValue = propertyType == typeof(DateTime) ? ((DateTime)property.GetValue(entity)!).ToString("yyyy-MM-dd")! : property.GetValue(entity)!.ToString()!;
                    dictionaryResult.Add(propertyName, propertyValue);
                }
                else
                {
                    if (property.GetValue(entity) != null)
                    {
                        string propertyName = $"{propertyType.Name}{nameof(entity.Id)}";
                        string propertyValue = ((Entity)property.GetValue(entity)!).Id.ToString();
                        dictionaryResult.Add(propertyName, propertyValue);
                    }
                }
            }
            return dictionaryResult;
        }

        public IDictionary<string, string> GetChangedValues<TEntity>(TEntity curentEntity, IDictionary<string, string> cleanValues) where TEntity : IEntity
        {
            IDictionary<string, string> currentValues = ConvertObjectToPropertiesDictionary(curentEntity);

            return (from cleanValue in cleanValues
                    where cleanValue.Value != currentValues[cleanValue.Key]
                    select new { cleanValue.Key, Value = currentValues[cleanValue.Key] }).ToDictionary(p => p.Key, p => p.Value);
        }

        public DynamicParameters TransformPropertiesToDynamicParameters(IDictionary<string, string> properties)
        {
            var dbArguments = new DynamicParameters();
            foreach (KeyValuePair<string, string> property in properties)
            {
                dbArguments.Add(property.Key, property.Value);
            }
            return dbArguments;
        }
    }
}
