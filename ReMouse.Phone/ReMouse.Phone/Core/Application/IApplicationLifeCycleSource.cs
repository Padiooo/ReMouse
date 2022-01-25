using System;

namespace ReMouse.Phone.Core.Application
{
    public interface IApplicationLifeCycleSource
    {
        /// <summary>
        /// Called once when application start.
        /// </summary>
        event Action OnStart;

        /// <summary>
        /// Called every time application become main focus.
        /// </summary>
        event Action OnResume;

        /// <summary>
        /// Called every time application lose main focus.
        /// </summary>
        event Action OnSleep;
    }
}
