using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.QuarterDueDates;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System.Data;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates
{
    public class QuarterDueDateRepository : Repository<QuarterDueDate>, IQuarterDueDateRepository
    {
        public QuarterDueDateRepository(
            IDbConnection connection,
            IDataComparer datacomparer,
            ISqlGenerator sqlGeneratorBase) :
            base(
                connection,
                datacomparer,
                sqlGeneratorBase)
        {
        }
    }
}
