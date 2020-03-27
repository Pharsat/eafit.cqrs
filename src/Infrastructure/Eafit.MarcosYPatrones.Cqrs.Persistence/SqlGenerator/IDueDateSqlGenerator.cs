using System;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface IDueDateSqlGenerator : ISqlGenerator
    {
        string BuildGetCompleteByIdSqlCommand();
    }
}