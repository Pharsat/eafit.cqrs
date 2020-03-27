using System;

namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class TaxYear : Entity, IEquatable<TaxYear>
    {
        public int Year { get; }

        public TaxYear() { }

        public TaxYear(int year) => Year = year;

        public bool Equals(TaxYear taxYear) => Year == taxYear.Year;
    }
}