using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence
{
    public class ListRepository<TEnum> : IListRepository<TEnum> where TEnum : struct, IConvertible
    {
        private readonly IDbConnection _connection;
        private readonly IListItemSqlGenerator _sqlGeneratorBase;

        public ListRepository(
            IDbConnection connection,
            IListItemSqlGenerator sqlGeneratorBase)
        {
            _connection = connection;
            _sqlGeneratorBase = sqlGeneratorBase;
        }

        public async Task<IEnumerable<ListItem>> GetAllAsync()
        {
            string selectAllQuery = _sqlGeneratorBase.BuildGetAllSqlQuery(typeof(TEnum).Name);
            return await _connection.QueryAsync<ListItem>(selectAllQuery).ConfigureAwait(false);
        }
    }
}
