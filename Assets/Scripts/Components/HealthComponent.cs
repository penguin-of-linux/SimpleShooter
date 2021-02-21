using System;
using UnityEngine;

namespace Components
{
    public class HealthComponent : MonoBehaviour, IDisposable
    {
        public int Health { get; set; }
        
        void Start()
        {
            healthGameObject = Instantiate(ResourceLoader.GetHealthBarPrefab());
        }

        private void Update()
        {
            var position = transform.position;
            healthGameObject.transform.position = new Vector3(position.x, position.y + 0.4f, position.z);

            var health = healthGameObject.transform.GetChild(0);
            health.transform.localScale = new Vector3(Health / 100f, 1, 1);
        }

        private GameObject healthGameObject;

        public void Dispose()
        {
            Destroy(healthGameObject);
        }
    }
}