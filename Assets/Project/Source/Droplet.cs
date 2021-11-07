using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Source
{
    public class Droplet : MonoBehaviour, IUpdatable, IPointerClickHandler
    {
        [SerializeField] private float movementSpeed = 1.0f;
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

            gameObject.SetActive(false);
            _transited?.Invoke();
            _lifecycleEnded?.Invoke(this);
        }

        public void OnTransitFinished(Action action) => _transited = action;

        public void OnTransitInterrupted(Action action) => _transitInterrupted = action;

        public void OnLifecycleEnds(Action<Droplet> action) => _lifecycleEnded = action;

        public void OnPointerClick(PointerEventData eventData)
        {
            _transitInterrupted?.Invoke();
            _lifecycleEnded?.Invoke(this);
        }
    }
}