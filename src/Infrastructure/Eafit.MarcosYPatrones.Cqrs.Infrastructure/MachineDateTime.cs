using Eafit.MarcosYPatrones.Cqrs.Common;
using System;

namespace Eafit.MarcosYPatrones.Cqrs.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
