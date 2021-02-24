using System;
using System.Collections.Generic;
using System.Linq;
using Core.MapDto;
using Core.MapDto.MapObjects;
using UnityEngine;
namespace Controllers.UnitsController
{
    public abstract class UnitBaseController : EntityBaseController
    {
        public virtual void Start()
        {
            base.Start();
            
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            bulletPrefab = ResourceLoader.GetBulletPrefab();
        }

        public virtual void FixedUpdate()
        {
            animator.SetFloat(speedAnimatorVariable, rigidBody.velocity.magnitude);
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
                bullet.GetComponent<BulletController>().MapObjectId = Entity.Id;
                bullet.transform.rotation = Geometry.GetQuaternionFromCathetuses(direction);

                bullet.transform.position = transform.position.ToVector2() + direction;

                lastShoot = now;
            }
        }

        protected (bool, Unit) UpdateTarget(Unit target, Map map)
        {
            if (target != null && map.Entities.ContainsKey(target.Id))
                return (false, target);

            var team = ((Unit)Entity).Team;
            var newTarget = map.Units.FirstOrDefault(x => x.Team != team && x.Team != Team.Neutral);
            return (true, newTarget);
        }
        
        protected bool CanShoot(Vector2 direction, Unit target)
        {
            var position = transform.position.ToVector2();
            var hits = new List<RaycastHit2D>();
            var filter = new ContactFilter2D();
            Physics2D.Raycast(position, direction, filter, hits);

            if (hits.Count < 2)
                return false;
        
            var hit = hits[1];
            var haveMapObjectIdController = hit.collider.gameObject.GetComponent<IHaveMapObjectId>();
            if (haveMapObjectIdController != null && haveMapObjectIdController.MapObjectId == target.Id)
            {
                return true;
            }

            return false;
        }

        private DateTime lastShoot = DateTime.MinValue;
        
        private readonly TimeSpan shootPeriod = TimeSpan.FromSeconds(0.25);
        private readonly float accelerate = 10f;
        private const float bulletForce = 500;
        
        private Animator animator;
        private Rigidbody2D rigidBody;
        private GameObject bulletPrefab;
        
        private static readonly int speedAnimatorVariable = Animator.StringToHash("Speed");
    }
}