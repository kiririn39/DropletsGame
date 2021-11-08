using System;
using System.Collections.Generic;

namespace Project.Source
{
    public class ObjectsPool<T>
    {
        public bool HasFreeInstance => _objects.Count > 0;
        private Action<T> _retrievalAction;
        private Action<T> _poolAction;
        private Queue<T> _objects;


        public ObjectsPool() => _objects = new Queue<T>();

        public void SetRetrievalAction(Action<T> action) => _retrievalAction = action;
        public void SetPoolAction(Action<T> action) => _poolAction = action;

        public T RetrieveInstance()
        {
            var instance = _objects.Dequeue();
            _retrievalAction.Invoke(instance);

            return instance;
        }

        public void PoolInstance(T instance)
        {
            _poolAction.Invoke(instance);
            _objects.Enqueue(instance);
        }
    }
}