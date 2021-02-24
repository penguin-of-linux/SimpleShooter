using Core.MapDto;
using Core.MapDto.MapObjects;
using UnityEngine;

namespace Controllers.UnitsController
{
    public class BotController : UnitBaseController
    {
        public override void Start()
        {
            base.Start();
        
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            Entity = (Bot)map.Entities[MapObjectId];
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var botTileCords = Entity.Cords.ToVectorInt();

            var (updated, newTarget) = UpdateTarget(target, map);
            target = newTarget;
            if (target == null) return;
        
            var targetCords = target.Cords;
            var targetTileCords = targetCords.ToVectorInt();

            var direction = (target.Cords - Entity.Cords).normalized;
            if (CanShoot(direction, target))
            {
                Shoot(direction);
                return;
            }

            if (Entity.Cords.CloseToPoint(targetCords)) return;

            if (localPathTarget == null || Entity.Cords.CloseToPoint(localPathTarget.Value) || updated)
            {
                var playerTile = map[targetTileCords];
                var botTile = map[botTileCords];
                var path = map.GetPath(botTile, playerTile);
                localPathTarget = path.Length > 0 ? path[0].Cords.TileCenter() : botTile.Cords;
            }

            if (localPathTarget.HasValue)
                MoveTo(localPathTarget.Value);
        }
        
        public override void Update()
        {
            base.Update();

            var velocity = transform.GetComponent<Rigidbody2D>().velocity;
            if (velocity.magnitude > 0)
            {
                var rotateDirection = velocity.ToVector3().normalized;
                if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
            }
        }

        private new void Shoot(Vector2 direction)
        {
            base.Shoot(direction);
            
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = Geometry.GetQuaternionFromCathetuses(direction);
        }

        private Unit target;
        private Vector2? localPathTarget;
        private Map map;
    }
}