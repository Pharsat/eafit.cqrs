using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System.Data;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates
{
    public class JurisdictionRepository : Repository<Jurisdiction>, IJurisdictionRepository
    {
        public JurisdictionRepository(
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
