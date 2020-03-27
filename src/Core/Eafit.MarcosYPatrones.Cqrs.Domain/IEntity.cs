using System;
using System.Collections.Generic;
using System.Text;

namespace Eafit.MarcosYPatrones.Cqrs.Domain
{
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
