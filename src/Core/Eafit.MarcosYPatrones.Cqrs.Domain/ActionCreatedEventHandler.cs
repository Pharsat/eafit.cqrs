namespace Eafit.MarcosYPatrones.Cqrs.Domain
{
    public delegate void ActionCreatedEventHandler<TEventArguments>(object sender, TEventArguments e);
}
