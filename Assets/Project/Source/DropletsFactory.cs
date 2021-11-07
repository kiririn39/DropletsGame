using UnityEngine;

namespace Project.Source
{
    [CreateAssetMenu(menuName = "Droplets/" + nameof(DropletsFactory))]
    public class DropletsFactory : ScriptableObject
    {
        [SerializeField] private Droplet dropletPrefab;


        public Droplet GetDroplet() => Instantiate(dropletPrefab);

        public void DisposeInstanceOf(Droplet droplet) => Destroy(droplet);
    }
}