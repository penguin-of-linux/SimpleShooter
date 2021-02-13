using System;
using Core.MapDto;
using Core.MapDto.MapObjects;
using UnityEngine;

namespace Controllers.UnitsController
{
    public class BotController : UnitBaseController, IHaveMapObjectId
    {
        public Guid MapObjectId { get; set; }

        protected override Unit Unit { get; set; }

        public override void Start()
        {
            base.Start();
        
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            Unit = (Bot)map.Units[MapObjectId];
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var botTileCords = Unit.Cords.ToVectorInt();

            var (updated, newTarget) = UpdateTarget(target, map);
            target = newTarget;
            if (target == null) return;
        
            var targetCords = target.Cords;
            var targetTileCords = targetCords.ToVectorInt();

            var direction = (target.Cords - Unit.Cords).normalized;
            if (CanShoot(direction, target))
            {
                Shoot(direction);
                return;
            }

            if (Unit.Cords.CloseToPoint(targetCords)) return;

            if (localPathTarget == null || Unit.Cords.CloseToPoint(localPathTarget.Value) || updated)
            {
                var playerTile = map[targetTileCords];
                var botTile = map[botTileCords];
                var path = map.GetPath(botTile, playerTile);
                localPathTarget = path.Length > 0 ? path[0].Cords.TileCenter() : botTile.Cords;
            }

            if (localPathTarget.HasValue)
                MoveTo(localPathTarget.Value);
        }

        private Unit target;
        private Vector2? localPathTarget;
        private Map map;
    }
}