using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.CheckTeam
{
    public class CheckIfExitsByNameQuery : IQuery
    {
        [StringLength(64)]
        public string Name { get; set; }

        public CheckIfExitsByNameQuery(string name) => Name = name;
    }
}
