using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Source
{
    [RequireComponent(typeof(Collider2D))]
    public class Droplet : MonoBehaviour, IUpdatable, IPointerClickHandler
    {
        [SerializeField] private float movementSpeed;
        private Vector3 _moveFrom;
        private Vector3 _moveTo;
        private float _movementProgress;

        private Action _transitInterrupted;
        private Action _transited;
        private Action<Droplet> _lifecycleEnded;


        public void Transit(Vector3 from, Vector3 to)
        {
            _movementProgress = 0.0f;
            transform.position = _moveFrom;
            _moveFrom = from;
            _moveTo = to;
        }

        public void DoUpdate(float deltaTime)
        {
            _movementProgress += movementSpeed * deltaTime;
            transform.position = Vector3.Lerp(_moveFrom, _moveTo, _movementProgress);

            if (_movementProgress < 1.0f)
                return;

            InvokeLifecycleEnd(_transited);
        }

        public void OnTransitFinished(Action action) => _transited = action;

        public void OnTransitInterrupted(Action action) => _transitInterrupted = action;

        public void OnLifecycleEndAction(Action<Droplet> action) => _lifecycleEnded = action;

        public void OnPointerClick(PointerEventData eventData) => InvokeLifecycleEnd(_transitInterrupted);

        protected virtual void InvokeLifecycleEnd(Action preEndAction)
        {
            preEndAction?.Invoke();
            _lifecycleEnded?.Invoke(this);
        }
    }
}