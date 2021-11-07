using System;

namespace Project.Source
{
    public class Timer : IUpdatable
    {
        private float _setSeconds;
        private float _elapsedSeconds;
        private Action _action;


        public Timer(TimeSpan timeSpan, Action action)
        {
            ChangeTimeSpan(timeSpan);
            _action = action;
        }

        public void ChangeTimeSpan(TimeSpan timeSpan) => _setSeconds = (float)timeSpan.TotalSeconds;

        public void DoUpdate(float deltaTime)
        {
            _elapsedSeconds += deltaTime;
            var invokeTimes = (int)(_elapsedSeconds / _setSeconds);

            for (int i = 0; i < invokeTimes; i++)
            {
                _action?.Invoke();
                _elapsedSeconds = 0.0f;
            }
        }
    }
}