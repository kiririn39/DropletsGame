using TMPro;
using UnityEngine;

namespace Project.Source.Progress
{
    public class ProgressLabel : MonoBehaviour, IProgressCountConsumer
    {
        [SerializeField] private string labelPrefix;
        [SerializeField] private string labelPostfix;
        [SerializeField] private TMP_Text label;

        [SerializeField] private FloatProgressCounter floatProgressCounter;


        private void Start()
        {
            floatProgressCounter.AddConsumer(this);
        }

        public void Consume(float progress)
        {
            label.text = labelPrefix + progress + labelPostfix;
        }
    }
}