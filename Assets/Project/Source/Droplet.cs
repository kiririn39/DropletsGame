using System;
using Project.Source.DropletsExtensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Source
{
    [RequireComponent(typeof(Collider2D))]
    public class Droplet : MonoBehaviour, IUpdatable, IPointerClickHandler
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private DropletExtension[] _extensions;
        private Vector3 _moveFrom;
        private Vector3 _moveTo;
        private float _movementProgress;

        private Action<Droplet> _transitInterrupted;
        private Action<Droplet> _transited;
        private Action<Droplet> _lifecycleEnded;


        private void Awake()
        {
            foreach (var dropletExtension in _extensions)
                dropletExtension.Extend(this);
        }

        public void Transit(Vector3 from, Vector3 to)
        {
            _movementProgress = 0.0f;
            transform.position = _moveFrom;
            _moveFrom = from;
            _moveTo = to;

            foreach (var dropletExtension in _extensions)
                dropletExtension.TransitStarted(from, to);
        }

        public void DoUpdate(float deltaTime)
        {
            _movementProgress += movementSpeed * deltaTime;
            transform.position = Vector3.Lerp(_moveFrom, _moveTo, _movementProgress);

            foreach (var dropletExtension in _extensions)
                dropletExtension.DoUpdate(deltaTime, _movementProgress);

            if (_movementProgress < 1.0f)
                return;

            InvokeLifecycleEnd(_transited);
        }

        public void OnTransitFinished(Action<Droplet> action)
        {
            foreach (var dropletExtension in _extensions)
                dropletExtension.TransitFinished();

            _transited = action;
        }

        public void OnTransitInterrupted(Action<Droplet> action)
        {
            foreach (var dropletExtension in _extensions)
                dropletExtension.TransitInterrupted();

            _transitInterrupted = action;
        }

        public void OnLifecycleEndAction(Action<Droplet> action) => _lifecycleEnded = action;

        public void OnPointerClick(PointerEventData eventData) => InvokeLifecycleEnd(_transitInterrupted);

        protected virtual void InvokeLifecycleEnd(Action<Droplet> preEndAction)
        {
            preEndAction?.Invoke(this);
            _lifecycleEnded?.Invoke(this);
        }
    }
}