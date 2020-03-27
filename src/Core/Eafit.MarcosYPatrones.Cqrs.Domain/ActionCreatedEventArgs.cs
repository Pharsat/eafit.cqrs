using System;

namespace Eafit.MarcosYPatrones.Cqrs.Domain
{
    public class ActionCreatedEventArgs : EventArgs
    {
        public EntityAction Action { get; }

        public ActionCreatedEventArgs(EntityAction action)
        {
            Action = action;
        }
    }
}
