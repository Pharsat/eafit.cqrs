using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System.Data;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates
{
    public class DeliverableTypeRepository : Repository<DeliverableType>, IDeliverableTypeRepository
    {
        public DeliverableTypeRepository(
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
