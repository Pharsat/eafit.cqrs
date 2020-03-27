using System;

namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class DueDate : Entity
    {
        public TaxPayerType TaxPayerType { get; set; }
        public DeliverableType DeliverableType { get; set; }
        public Jurisdiction Jurisdiction { get; set; }
        public Form Form { get; set; }
        public TaxYear TaxYear { get; set; }
        public DateTime StatutoryDueDate { get; set; }
        public DateTime ExtensionDate { get; set; }
        public Quarter? Quarter { get; set; }
        public bool IsManuallyAdded { get; set; }

        public DueDate()
        {
            TaxPayerType = default!;
            DeliverableType = default!;
            Jurisdiction = default!;
            Form = default!;
            TaxYear = default!;
        }

        public DueDate(
            TaxPayerType taxPayerType,
            DeliverableType deliverableType,
            Jurisdiction jurisdiction,
            Form form,
            TaxYear taxYear,
            DateTime statutoryDueDate,
            DateTime extensionDate,
            Quarter? quarter,
            bool isManuallyAdded )
        {
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