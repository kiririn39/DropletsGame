using System;

namespace Project.Source
{
    public class PooledDroplet : Droplet
    {
        private ObjectsPool<Droplet> _pool;


        public void SetPool(ObjectsPool<Droplet> pool) => _pool = pool;

        protected override void InvokeLifecycleEnd(Action<Droplet> action)
        {
            base.InvokeLifecycleEnd(action);
            _pool?.PoolInstance(this);
        }
    }
}