using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class UnitBaseController : MonoBehaviour
    {
        public void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            bulletPrefab = ResourceLoader.GetBulletPrefab();
        }

        public void FixedUpdate()
        {
            animator.SetFloat("Speed", rigidBody.velocity.magnitude);
        }

        protected void MoveTo(Vector2 cords)
        {
            var direction = cords - rigidBody.transform.position.ToVector2();
            MoveToDirection(direction);
        }
        
        protected void MoveToDirection(Vector2 direction)
        {
            rigidBody.velocity = direction.normalized * accelerate;
        }

        protected void Shoot(Vector2 direction)
        {
            var now = DateTime.Now;
            if (lastShoot + shootPeriod < now)
            {
                direction = direction.normalized;
                var bullet = Instantiate(bulletPrefab, null);
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce);
                bullet.transform.rotation = Geometry.GetQuaternionFromCathetuses(direction);

                bullet.transform.position = transform.position.ToVector2() + direction;

                lastShoot = now;
            }
        }

        private DateTime lastShoot = DateTime.MinValue;
        
        private readonly TimeSpan shootPeriod = TimeSpan.FromSeconds(0.25);
        private readonly float accelerate = 10f;
        private const float bulletForce = 500;
        
        private Animator animator;
        private Rigidbody2D rigidBody;
        private GameObject bulletPrefab;
    }
}