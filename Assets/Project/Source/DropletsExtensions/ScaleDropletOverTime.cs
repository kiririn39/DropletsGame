using UnityEngine;

namespace Project.Source.DropletsExtensions
{
    public class ScaleDropletOverTime : DropletExtension
    {
        [SerializeField] private AnimationCurve uniformScalingCurve;


        public override void DoUpdate(float deltaTime, float transitProgress)
        {
            var scale = uniformScalingCurve.Evaluate(transitProgress) * Vector3.one;

            Droplet.transform.localScale = scale;
        }

        public override void TransitStarted(Vector3 @from, Vector3 to)
        {
        }

        public override void TransitFinished()
        {
        }

        public override void TransitInterrupted()
        {
        }
    }
}