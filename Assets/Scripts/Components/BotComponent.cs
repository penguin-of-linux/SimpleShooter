using System.Collections.Generic;
using System.Linq;
using Controllers;
using Core.MapDto;
using UnityEngine;

namespace Components
{
    public class BotComponent : MonoBehaviour
    {
        public void Start()
        {
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();

            shootComponent = GetComponent<ShootComponent>();
            transformComponent = GetComponent<TransformComponent>();
            teamComponent = GetComponent<TeamComponent>();
        }
        
        public void FixedUpdate()
        {
            var botPosition = transform.position.ToVector2();
            var botTilePosition = botPosition.ToVectorInt();

            var (updated, newTarget) = UpdateTarget(target);
            target = newTarget;
            if (target == null) return;
        
            var targetPosition = target.transform.position.ToVector2();
            var targetTilePosition = targetPosition.ToVectorInt();

            var direction = (targetPosition - botPosition).normalized;
            if (CanShoot(direction, target))
            {
                Shoot(direction);
                return;
            }

            if (botPosition.CloseToPoint(targetPosition)) return;

            if (localPathTarget == null || botPosition.CloseToPoint(localPathTarget.Value) || updated)
            {
                var targetTile = map[targetTilePosition];
                var botTile = map[botTilePosition];
                var path = map.GetPath(botTile, targetTile);
                localPathTarget = path.Length > 0 ? path[0].Cords.TileCenter() : botTile.Cords;
            }

            if (localPathTarget.HasValue)
            {
                MoveTo(localPathTarget.Value);
            }
        }
        
        public void Update()
        {
            var velocity = transform.GetComponent<Rigidbody2D>().velocity;
            if (velocity.magnitude > 0)
            {
                var rotateDirection = velocity.ToVector3().normalized;
                if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
            }
        }
        
        private (bool, GameObject) UpdateTarget(GameObject prevTarget)
        {
            if (prevTarget != null && gameStateController.ContainsEntity(prevTarget.GetInstanceID()))
                return (false, prevTarget);

            var myTeam = teamComponent.Team;
            var newTarget = FindObjectsOfType<TeamComponent>()
                .FirstOrDefault(x => x.Team != myTeam && x.Team != Team.Neutral)
                ?.gameObject;
            return (true, newTarget);
        }

        private bool CanShoot(Vector2 direction, GameObject target)
        {
            var position = transform.position.ToVector2();
            var hits = new List<RaycastHit2D>();
            var filter = new ContactFilter2D();
            Physics2D.Raycast(position, direction, filter, hits);

            if (hits.Count < 2)
                return false;
        
            if (target == hits[1].collider.gameObject)
            {
                return true;
            }

            return false;
        }
        
        private void Shoot(Vector2 direction)
        {
            shootComponent.Direction = direction;
            
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = Geometry.GetQuaternionFromCathetuses(direction);
        }

        private void MoveTo(Vector2 position)
        {
            transformComponent.Direction = position - transform.position.ToVector2();
        }
        
        private GameObject target;
        private Vector2? localPathTarget;
        private Map map;        
        
        private GameStateController gameStateController;

        private ShootComponent shootComponent;
        private TransformComponent transformComponent;
        private TeamComponent teamComponent;
    }
}