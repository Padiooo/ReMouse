using System;

namespace ReMouse.Phone.Core.Application
{
    public class ApplicationLifeCycleSource : IApplicationLifeCycleSource
    {
        public event Action OnStart;
        public event Action OnResume;
        public event Action OnSleep;

        public void Start() => OnStart?.Invoke();
        public void Resume() => OnResume?.Invoke();
        public void Sleep() => OnSleep?.Invoke();
    }
}
