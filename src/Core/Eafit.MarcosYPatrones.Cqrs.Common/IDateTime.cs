using System;

namespace Eafit.MarcosYPatrones.Cqrs.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
