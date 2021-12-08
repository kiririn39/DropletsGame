using UnityEngine;

namespace Project.Source.DropletsExtensions
{
    public abstract class DropletExtension : MonoBehaviour
    {
        protected Droplet Droplet;


        public abstract void DoUpdate(float deltaTime, float transitProgress);
        public virtual void Extend(Droplet droplet) => Droplet = droplet;
        public abstract void TransitStarted(Vector3 from, Vector3 to);
        public abstract void TransitFinished();
        public abstract void TransitInterrupted();
    }
}