using UnityEngine;

namespace Project.Source
{
    [CreateAssetMenu(menuName = "Droplets/" + nameof(DropletsFactory))]
    public class DropletsFactory : ScriptableObject
    {
        [SerializeField] private PooledDroplet dropletPrefab;
        private ObjectsPool<Droplet> _pool;


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

            var instance = Instantiate(dropletPrefab);
            instance.SetPool(_pool);

            return instance;
        }
    }
}