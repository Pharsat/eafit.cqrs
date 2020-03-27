namespace Eafit.MarcosYPatrones.Cqrs.Domain.DueDates
{
    public class QuarterDueDate : NotAutoIdentityEntity
    {
        public int QuarterId { get; set; }

        public QuarterDueDate() { }

        public QuarterDueDate(int id, int quarterId) => (Id, QuarterId) = (id, quarterId);
    }
}
