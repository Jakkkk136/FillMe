using UnityEngine;

namespace DefaultNamespace
{
    public class Glass : MonoBehaviour
    {
        public delegate void onWaterCollided();
        public static event onWaterCollided OnWaterCollided;

        public bool isFilled = false;

        private bool isCollided = false;
        

        private void OnParticleCollision(GameObject other)
        {
            if (isCollided) return;
            if (other.tag == "WaterOrigin")
            {
                this.isCollided = true;
                this.isFilled = true;
                OnWaterCollided?.Invoke();
            }
        }
    }
}