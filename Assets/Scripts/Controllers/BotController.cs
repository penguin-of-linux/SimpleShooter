using System;
using System.Collections.Generic;
using System.Linq;
using Core.MapDto;
using DefaultNamespace;
using DefaultNamespace.Core.MapDto;
using DefaultNamespace.Core.MapDto.MapObjects;
using UnityEngine;

public class BotController : UnitBaseController, IHaveMapObjectId
{
    public Guid MapObjectId { get; set; }
    public Bot hostBot;
    
    void Start()
    {
        base.Start();
        var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
        if (gameController != null) map = gameController.Map;

        hostBot = (Bot)map.Units[MapObjectId];
    }

    void FixedUpdate()
    {
        base.FixedUpdate();

        var botTileCords = hostBot.Cords.ToVectorInt();

        var updated = UpdateTarget();
        if (target == null) return;
        
        var targetCords = target.Cords;
        var targetTileCords = targetCords.ToVectorInt();

        var direction = (target.Cords - hostBot.Cords).normalized;
        if (CanShoot(direction, target))
        {
            //Debug.Log("pew pew");
            Shoot(direction);
            return;
        }

        if (hostBot.Cords.CloseToPoint(targetCords)) return;

        if (localPathTarget == null || hostBot.Cords.CloseToPoint(localPathTarget.Value) || updated)
        {
            var playerTile = map[targetTileCords];
            var botTile = map[botTileCords];
            var path = map.GetPath(botTile, playerTile);
            localPathTarget = path.Length > 0 ? path[0].Cords.TileCenter() : botTile.Cords;
        }

        MoveTo(localPathTarget.Value);
        
    }

    private bool CanShoot(Vector2 direction, Unit target)
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

    private bool UpdateTarget()
    {
        if (target != null && map.Units.ContainsKey(target.Id))
            return false;

        target = map.Units.Values.FirstOrDefault(x => x.Team != hostBot.Team && x.Team != Team.Neutral);
        return true;
    }

    private Unit target;
    private Vector2? localPathTarget;
    private Map map;
}