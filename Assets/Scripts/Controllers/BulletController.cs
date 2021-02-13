using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.GetComponent<TargetController>();
            if (target != null) target.Hit();
            Destroy(gameObject);
        }
    }
}