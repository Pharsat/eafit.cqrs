using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.UpdateDueDate
{
    public class UpdateDueDateCommandHandler : IVoidCommandHandler<UpdateDueDateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDueDateCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task HandleAsync(UpdateDueDateCommand command)
        {
            if (!await _unitOfWork.TaxPayerTypes.ExistsAsync(command.TaxPayerTypeId).ConfigureAwait(false))
            {
                throw new EntityDoesNotExistsException("Tax payer type requested does not exist.");
            }

            if (!await _unitOfWork.DeliverableTypes.ExistsAsync(command.DeliverableTypeId).ConfigureAwait(false))
            {
                throw new EntityDoesNotExistsException("Deliverable type requested does not exist.");
            }

            Quarter quarter = await _unitOfWork.Quarters.GetByIdAsync(command.QuarterId).ConfigureAwait(false);

            if (!await _unitOfWork.Jurisdictions.ExistsAsync(command.JurisdictionId).ConfigureAwait(false))
            {
                throw new EntityDoesNotExistsException("Jurisdiction requested does not exist.");
            }

            IEnumerable<Form> forms = await _unitOfWork.Forms.GetByNameAsync(command.FormName).ConfigureAwait(false);
            if (!forms.Any())
            {
                throw new EntityDoesNotExistsException("No form matched the form name.");
            }
            if (forms.Count() > 1)
            {
                throw new MultipleResultsForRequestedSingleEntityException("Many forms matched the form name.");
            }
            Form matchedForm = forms.Single();

            IEnumerable<TaxYear> taxYears = await _unitOfWork.TaxYears.GetByYearAsync(command.TaxYear).ConfigureAwait(false);
            if (!taxYears.Any())
            {
                throw new EntityDoesNotExistsException("No tax year matched the form name.");
            }
            if (taxYears.Count() > 1)
            {
                throw new MultipleResultsForRequestedSingleEntityException("Many tax year matched the form name.");
            }
            TaxYear matchedTaxYear = taxYears.Single();

            DueDate dueDate = await _unitOfWork.DueDates.GetCompleteByIdAsync(command.DueDateId).ConfigureAwait(false);
            dueDate.TaxPayerType.Id = command.TaxPayerTypeId;
            dueDate.DeliverableType.Id = command.DeliverableTypeId;
            dueDate.Jurisdiction.Id = command.JurisdictionId;
            dueDate.Form.Id = matchedForm.Id;
            dueDate.TaxYear.Id = matchedTaxYear.Id;
            dueDate.StatutoryDueDate = command.StatutoryDueDate;
            dueDate.ExtensionDate = command.ExtensionDate;
            _unitOfWork.DueDates.CreateUpdateActions();

            //Check if exists because if it performs simple get it will retrieve an non exits error, and it is intended to exists or not (optional).
            if (await _unitOfWork.QuarterDueDates.ExistsAsync(command.DueDateId))
            {
                var quarterDueDate = await _unitOfWork.QuarterDueDates.GetByIdAsync(command.DueDateId);
                quarterDueDate.QuarterId = command.QuarterId;
                _unitOfWork.QuarterDueDates.CreateUpdateActions();
            }
            else
            {
                var quarterDueDate = new QuarterDueDate(command.DueDateId, command.QuarterId);
                _unitOfWork.QuarterDueDates.Add(quarterDueDate);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
