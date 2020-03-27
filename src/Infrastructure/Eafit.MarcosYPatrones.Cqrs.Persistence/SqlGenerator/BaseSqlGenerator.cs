using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class BaseSqlGenerator : ISqlGenerator
    {
        private const string _selectScopeIdentity = "SELECT SCOPE_IDENTITY();";
        private readonly IDataComparer _dataComparer;

        public BaseSqlGenerator(IDataComparer dataComparer) => _dataComparer = dataComparer;

        public string BuildAddSqlCommand(IEntity entity)
        {

            string tableName = entity.GetType().Name;

            IDictionary<string, string> keyValuePair = _dataComparer.ConvertObjectToPropertiesDictionary(entity);
            
            keyValuePair.Remove(nameof(entity.Id));

            var columnField = GenerateColumnFields(keyValuePair);
            var valueField = GenerateValueFields(keyValuePair);

            return $"INSERT INTO [{tableName}] ({columnField}) Values ({valueField}); {_selectScopeIdentity};";
        }

        public string BuildAddNotAutoidentitySqlCommand(IEntity entity)
        {
            string tableName = entity.GetType().Name;

            IDictionary<string, string> keyValuePair = _dataComparer.ConvertObjectToPropertiesDictionary(entity);

            var columnField = GenerateColumnFields(keyValuePair);
            var valueField = GenerateValueFields(keyValuePair);

            return $"INSERT INTO [{tableName}] ({columnField}) Values ({valueField});";
        }

        public string BuildDeleteSqlCommand(IEntity entity, string typeName)
        {
            return $"DELETE FROM [{typeName}] WHERE ID = @{nameof(entity.Id)}";
        }

        public string BuildUpdateSqlCommand(IEntity entity, IDictionary<string, string> changes)
        {
            string tableName = entity.GetType().Name;

            var valuePairs = string.Join(',', changes.Keys.Select(p => $"[{p}] = @{p}"));

            return $"UPDATE [{tableName}] SET {valuePairs} WHERE ID = @{nameof(entity.Id)};";
        }

        public string BuildGetByIdSqlCommand(Type type, string identifierName)
        {
            return $"SELECT * FROM [{type.Name}] WHERE Id = @{identifierName};";
        }

        public string BuildExistsByIdSqlCommand(Type type, string identifierName)
        {
            return $"SELECT 1 FROM [{type.Name}] WHERE Id = @{identifierName};";
        }

        public string BuildGetAllSqlQuery(Type type)
        {
            return $"SELECT * FROM [{type.Name}] WHERE [Id] NOT IN @ExcludedIds";
        }

        private string GenerateValueFields(IDictionary<string, string> keyValuePair)
        {
            return string.Join(',', keyValuePair.Keys.Select(s => "@" + s));
        }

        private string GenerateColumnFields(IDictionary<string, string> keyValuePair)
        {
            return string.Join(',', keyValuePair.Keys.Select(p => $"[{p}]"));
        }
    }
}