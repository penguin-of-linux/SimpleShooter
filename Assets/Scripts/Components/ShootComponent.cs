using System;
using UnityEngine;

namespace Components
{
    public class ShootComponent : MonoBehaviour
    {
        public Vector2 Direction { get; set; }
        public int Damage { get; set; }

        void Start()
        {
            bulletPrefab = ResourceLoader.GetBulletPrefab();
        }
        
        void Update()
        {
            var now = DateTime.Now;
            if (Direction != Vector2.zero && lastShoot + shootPeriod < now)
            {
                Direction = Direction.normalized;
                var bullet = Instantiate(bulletPrefab, null);
                bullet.GetComponent<Rigidbody2D>().AddForce(Direction * bulletForce);
                bullet.transform.rotation = Geometry.GetQuaternionFromCathetuses(Direction);

                bullet.transform.position = transform.position.ToVector2() + Direction;

                lastShoot = now;
                Direction = Vector2.zero;
            }
        }

        private DateTime lastShoot = DateTime.MinValue;
        
        private readonly TimeSpan shootPeriod = TimeSpan.FromSeconds(0.25);
        private const float bulletForce = 500;
        private GameObject bulletPrefab;
    }
}