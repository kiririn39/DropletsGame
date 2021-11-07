using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Source
{
    public class DropletsGameRoot : MonoBehaviour
    {
        [SerializeField] private DropletsFactory dropletsFactory;
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
            _transitTimer = new Timer(TimeSpan.FromSeconds(0.5f), TransitDroplet);
            _deltaTimeIncreaseTimer = new Timer(TimeSpan.FromSeconds(1.5f), SpeedUpGame);
        }

        private void TransitDroplet()
        {
            var droplet = dropletsFactory.GetDroplet();
            var startPoint = _transitBoundsCalculator.TransitStartPoint();
            var endPoint = _transitBoundsCalculator.TransitEndPoint();

            _updatables.Add(droplet);
            droplet.OnLifecycleEnds(instance =>
            {
                _updatables.Remove(instance);
                dropletsFactory.DisposeInstanceOf(instance);
            });

            droplet.Transit(startPoint, endPoint);
        }

        private void SpeedUpGame()
        {
            _deltaTimeFactor += 0.2f;
        }

        private void Update()
        {
            _deltaTimeIncreaseTimer.DoUpdate(DeltaTime);
            _transitTimer.DoUpdate(DeltaTime);

            for (int i = _updatables.Count - 1; i >= 0; i--)
                _updatables[i].DoUpdate(DeltaTime);
        }
    }
}