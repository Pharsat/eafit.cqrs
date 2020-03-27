using System;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Queries.GetDueDate
{
    public class DueDateViewModel
    {
        public int Id { get; }
        public string TaxPayerType { get; }
        public string DeliverableType { get; }
        public string Jurisdiction { get; }
        public string Form { get; }
        public int TaxYear { get; }
        public DateTime StatutoryDueDate { get; }
        public DateTime ExtensionDate { get; }
        public string? Quarter { get; }
        public bool IsManuallyAdded { get; }

        public DueDateViewModel()
        {
            TaxPayerType = default!;
            DeliverableType = default!;
            Jurisdiction = default!;
            Form = default!;
        }

        public DueDateViewModel(int id, string taxPayerType, string deliverableType
                                , string jurisdiction, string form
                                , int taxYear, DateTime statutoryDueDate
                                , DateTime extensionDate
                                , string? quarter, bool isManuallyAdded)

        {
            Id = id;
            TaxPayerType = taxPayerType;
            DeliverableType = deliverableType;
            Jurisdiction = jurisdiction;
            Form = form;
            TaxYear = taxYear;
            StatutoryDueDate = statutoryDueDate;
            ExtensionDate = extensionDate;
            Quarter = quarter;
            IsManuallyAdded = isManuallyAdded;
        }
    }
}
