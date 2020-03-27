using Eafit.MarcosYPatrones.Cqrs.Domain;
using System;
using System.Collections.Generic;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface ISqlGenerator
    {
        string BuildAddSqlCommand(IEntity entity);
        string BuildAddNotAutoidentitySqlCommand(IEntity entity);
        string BuildDeleteSqlCommand(IEntity entity, string typeName);
        string BuildUpdateSqlCommand(IEntity entity, IDictionary<string, string> changes);
        string BuildGetByIdSqlCommand(Type type, string identifierName);
        string BuildExistsByIdSqlCommand(Type type, string identifierName);
        string BuildGetAllSqlQuery(Type type);
    }
}