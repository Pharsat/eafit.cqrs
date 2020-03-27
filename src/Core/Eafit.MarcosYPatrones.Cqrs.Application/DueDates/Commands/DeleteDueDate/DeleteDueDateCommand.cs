using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.DeleteDueDate
{
    public class DeleteDueDateCommand : ICommand
    {
        [Required]
        public int Id { get; set; }
    }
}
