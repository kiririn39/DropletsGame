using System;
using System.Collections.Generic;
using Project.Source.Progress;
using UnityEngine;

namespace Project.Source
{
    public class DropletsGameRoot : MonoBehaviour
    {
        [SerializeField] private DropletsFactory dropletsFactory;
        [SerializeField] private FloatProgressCounter scoreCounter;
        [SerializeField] private FloatProgressCounter dropletsSpeedCounter;
        [SerializeField] private FloatProgressCounter transitedDropletsCounter;
        private TransitBoundsCalculator _transitBoundsCalculator;
        public float DeltaTime => _deltaTimeFactor * Time.deltaTime;
        private float _deltaTimeFactor = 1.0f;
        private Timer _transitTimer;
        private Timer _deltaTimeIncreaseTimer;
        private List<IUpdatable> _updatables;


        private void Start()
        {
            _updatables = new List<IUpdatable>();
            _transitBoundsCalculator = new TransitBoundsCalculator(Camera.main);
            _transitTimer = new Timer(TimeSpan.FromSeconds(.5f), TransitDroplet);
            _deltaTimeIncreaseTimer = new Timer(TimeSpan.FromSeconds(2.5f), SpeedUpGame);

            dropletsSpeedCounter.Accumulate(_deltaTimeFactor);
        }

        private void TransitDroplet()
        {
            var droplet = dropletsFactory.GetDroplet();
            var startPoint = _transitBoundsCalculator.TransitStartPoint();
            var endPoint = _transitBoundsCalculator.TransitEndPoint();

            _updatables.Add(droplet);
            droplet.OnTransitInterrupted(CountScoreAndRemoveDroplet);
            droplet.OnTransitFinished(CountTransitedAndRemoveDroplet);

            droplet.Transit(startPoint, endPoint);
        }

        private void SpeedUpGame()
        {
            float accumulator = 0.2f;
            _deltaTimeFactor += accumulator;

            dropletsSpeedCounter.Accumulate(accumulator);
        }

        private void Update()
        {
            _deltaTimeIncreaseTimer.DoUpdate(DeltaTime);
            _transitTimer.DoUpdate(DeltaTime);

            for (int i = _updatables.Count - 1; i >= 0; i--)
                _updatables[i].DoUpdate(DeltaTime);
        }

        private void CountScoreAndRemoveDroplet(Droplet droplet)
        {
            scoreCounter.TryAccumulateProgressFrom(droplet.gameObject);
            _updatables.Remove(droplet);
        }

        private void CountTransitedAndRemoveDroplet(Droplet droplet)
        {
            transitedDropletsCounter.Accumulate(1);
            _updatables.Remove(droplet);
        }
    }
}