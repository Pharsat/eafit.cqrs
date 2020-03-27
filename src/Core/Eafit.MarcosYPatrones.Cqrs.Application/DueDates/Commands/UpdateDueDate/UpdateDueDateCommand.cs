using System;
using System.ComponentModel.DataAnnotations;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.UpdateDueDate
{
    public class UpdateDueDateCommand : ICommand
    {
        [Required]
        public int DueDateId { get; set; }

        [Required]
        public int TaxPayerTypeId { get; set; }

        [Required]
        public int DeliverableTypeId { get; set; }

        [Required]
        public int JurisdictionId { get; set; }

        public string FormName { get; set; }

        [Required]
        [Range(1900, 9999)]
        public int TaxYear { get; set; }

        [Required]
        public DateTime StatutoryDueDate { get; set; }

        [Required]
        public DateTime ExtensionDate { get; set; }

        [Required]
        public int QuarterId { get; set; }

        [Required]
        public bool Addition { get; set; }

        public UpdateDueDateCommand()
        {
            FormName = default!;
        }
    }
}
