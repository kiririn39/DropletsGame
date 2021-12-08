using System.Collections.Generic;
using UnityEngine;

namespace Project.Source.Progress
{
    public class FloatProgressCounter : MonoBehaviour
    {
        private float _accumulatedScore;
        private List<IProgressCountConsumer> _сonsumers;


        private void Awake() => _сonsumers = new List<IProgressCountConsumer>();

        public void TryAccumulateProgressFrom(GameObject obj)
        {
            var progressComponent = obj.GetComponent<ProgressComponent>();

            if (progressComponent != null)
                _accumulatedScore += progressComponent.progress;

            foreach (var сonsumer in _сonsumers)
                сonsumer.Consume(_accumulatedScore);
        }

        public void Accumulate(float progress)
        {
            _accumulatedScore += progress;

            foreach (var сonsumer in _сonsumers)
                сonsumer.Consume(_accumulatedScore);
        }

        public void AddConsumer(IProgressCountConsumer consumer)
        {
            _сonsumers.Add(consumer);
            consumer.Consume(_accumulatedScore);
        }
    }
}