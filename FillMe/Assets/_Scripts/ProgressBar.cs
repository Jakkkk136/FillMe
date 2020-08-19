using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ProgressBar : MonoBehaviour
    {
        public static ProgressBar S;
            
        private Slider slider;

        
        public float fillSpeed = 0.5f;
        private float targetProgress = 0;
        private ParticleSystem particleSystem;
        
        private void Awake()
        {
            S = this;
            slider = GetComponent<Slider>();
            particleSystem = GameObject.Find("ProgressBarParticles").GetComponent<ParticleSystem>();
        }


        public void IncrementProgress(float newProgress)
        {
            targetProgress = slider.value + newProgress;
        }

        private void Update()
        {
            if (slider.value < targetProgress)
            {
                slider.value += fillSpeed * Time.deltaTime;
                if (!particleSystem.isPlaying)
                    particleSystem.Play();
                
            return;
            }

            particleSystem.Stop();

        }

        public void ClearProgressOnSlider()
        {
            slider.value = 0;
            targetProgress = 0;
        }
    }
}