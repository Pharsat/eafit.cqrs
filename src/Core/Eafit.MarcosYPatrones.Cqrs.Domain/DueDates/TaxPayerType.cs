using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class TaxPayerType : Entity
    {
        public string  Name { get; }

        public TaxPayerType() => Name = default!;

        public TaxPayerType(int id, string name) => (Id, Name) = (id, name);

    }
}
