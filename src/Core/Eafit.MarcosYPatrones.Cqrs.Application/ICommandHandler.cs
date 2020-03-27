using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface ICommandHandler<TCommand, TReturn> : IRequestHandler<TCommand> where TCommand : ICommand
    {
        Task<TReturn> HandleAsync(TCommand command);
    }
}
