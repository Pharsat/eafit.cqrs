using Eafit.MarcosYPatrones.Cqrs.Application.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.QuarterDueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Quarters;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxPayerTypes;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface IUnitOfWork
    {
        ITeamRepository Teams { get; }

        IDueDateRepository DueDates { get; }

        IProjectRepository Projects { get; }

        ITaxPayerTypeRepository TaxPayerTypes { get; }

        IDeliverableTypeRepository DeliverableTypes { get; }

        IJurisdictionRepository Jurisdictions { get; }

        IFormRepository Forms { get; }

        ITaxYearRepository TaxYears { get; }

        IQuarterRepository Quarters { get; }

        IQuarterDueDateRepository QuarterDueDates { get; }

        void ActionCreated(object sender, ActionCreatedEventArgs e);

        void EnqueueAction(EntityAction action);

        Task<int> SaveChangesAsync();
    }
}
