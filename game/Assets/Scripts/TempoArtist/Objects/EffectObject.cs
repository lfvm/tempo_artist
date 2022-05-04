using UnityEngine;

namespace TempoArtist.Objects
{
    public class EffectObject : MonoBehaviour
    {
        public float lifeTime = 0.05f;

        private void Update()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
