using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Project.Source
{
    public class TransitBoundsCalculator
    {
        private Camera _camera;
        private Random _random;


        public TransitBoundsCalculator(Camera camera)
        {
            _random = new Random();
            _random.InitState();
            _camera = camera;
        }

        public Vector3 TransitStartPoint()
        {
            Vector3 topLeft = _camera.ViewportToWorldPoint(new Vector3(0, 1, 0));
            Vector3 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
            topLeft.z = 0;
            topRight.z = 0;

            return Vector3.Lerp(topLeft, topRight, _random.NextFloat());
        }

        public Vector3 TransitEndPoint()
        {
            Vector3 bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
            Vector3 bottomRight = _camera.ViewportToWorldPoint(new Vector3(1, 0, _camera.nearClipPlane));
            bottomLeft.z = 0;
            bottomRight.z = 0;

            return Vector3.Lerp(bottomLeft, bottomRight, _random.NextFloat());
        }
    }
}