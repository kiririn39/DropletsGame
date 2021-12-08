using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Source
{
    [CreateAssetMenu(menuName = "Droplets/" + nameof(DropletsFactory))]
    public class DropletsFactory : ScriptableObject
    {
        [SerializeField] private PooledDroplet[] dropletPrefabs;
        private ObjectsPool<Droplet> _pool;
        private const string NoDropletToSpawnError = "Factory instance has no droplets to spawn from";


        private void OnEnable()
        {
            _pool = new ObjectsPool<Droplet>();
            _pool.SetPoolAction(droplet => droplet.gameObject.SetActive(false));
            _pool.SetRetrievalAction(droplet => droplet.gameObject.SetActive(true));
        }

        public Droplet GetDroplet()
        {
            if (_pool.HasFreeInstance)
                return _pool.RetrieveInstance();

            var instance = Instantiate(RandomDroplet());
            instance.SetPool(_pool);

            return instance;
        }

        private PooledDroplet RandomDroplet()
        {
            int count = dropletPrefabs.Length;

            if (count == 0)
                Debug.LogException(new NullReferenceException(NoDropletToSpawnError), this);

            int index = Random.Range(0, count);
            return dropletPrefabs[index];
        }
    }
}