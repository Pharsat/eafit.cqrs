using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface IVoidCommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
