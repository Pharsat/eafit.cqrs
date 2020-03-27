using Eafit.MarcosYPatrones.Cqrs.Application;
using System;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class DueDateSqlGenerator : BaseSqlGenerator, IDueDateSqlGenerator
    {
        public DueDateSqlGenerator(IDataComparer dataComparer) : base(dataComparer) { }

        public string BuildGetCompleteByIdSqlCommand()
        {
            return @"SELECT [dbo].[DueDate].[Id], [dbo].[DueDate].[StatutoryDueDate] 
                        , [dbo].[DueDate].[ExtensionDate], [dbo].[DueDate].[IsManuallyAdded] 
                        , [dbo].[TaxPayerType].[Id], [dbo].[TaxPayerType].[Name] 
                        , [dbo].[DeliverableType].[Id], [dbo].[DeliverableType].[Name] 
                        , [dbo].[Jurisdiction].[Id], [dbo].[Jurisdiction].[Name] 
                        , [dbo].[Form].[Id], [dbo].[Form].[Name] 
                        , [dbo].[TaxYear].[Id], [dbo].[TaxYear].[Year] 
                        , [dbo].[Quarter].[Id], [dbo].[Quarter].[Name] 
                    FROM [dbo].[DueDate] 
                        INNER JOIN [dbo].[TaxPayerType] 
                            ON [dbo].[DueDate].[TaxPayerTypeId] = [dbo].[TaxPayerType].[Id] 
                        INNER JOIN [dbo].[DeliverableType] 
                            ON [dbo].[DueDate].[DeliverableTypeId] = [dbo].[DeliverableType].[Id] 
                        INNER JOIN [dbo].[Jurisdiction] 
                            ON [dbo].[DueDate].[JurisdictionId] = [dbo].[Jurisdiction].[Id] 
                        INNER JOIN [dbo].[Form] 
                            ON [dbo].[DueDate].[FormId] = [dbo].[Form].[Id] 
                        INNER JOIN [dbo].[TaxYear] 
                            ON [dbo].[DueDate].[TaxYearId] = [dbo].[TaxYear].[Id] 
                        LEFT JOIN [dbo].[QuarterDueDate] 
                            ON [dbo].[DueDate].[Id] = [dbo].[QuarterDueDate].[Id] 
                        LEFT JOIN [dbo].[Quarter] 
                            ON [dbo].[QuarterDueDate].[QuarterId] = [dbo].[Quarter].[Id] 
                    WHERE [dbo].[DueDate].[Id] = @Id;";
        }
    }
}
