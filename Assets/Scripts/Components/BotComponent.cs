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

            transformComponent = GetComponent<TransformComponent>();
        }
        
        public void FixedUpdate()
        {
        }
        
        public void Update()
        {
            var botPosition = transform.position.ToVector2();
            var botTilePosition = botPosition.ToVectorInt();

            var target = GetTarget(botPosition, prevTarget);

            var targetTile = map[target.ToVectorInt()];
            var botTile = map[botTilePosition];
            var path = map.GetPath(botTile, targetTile);
            var localPathTarget = path.Length > 0 ? path[0].Cords.TileCenter() : botTile.Cords;

            prevTarget = target;
            MoveTo(localPathTarget);
            
            var dir = transform.GetComponent<TransformComponent>().Direction;
            if (dir.magnitude > 0)
            {
                var rotateDirection = dir.ToVector3().normalized;
                if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
            }
        }
        
        private Vector2 GetTarget(Vector2 currentPosition, Vector2? prevTarget)
        {
            if (prevTarget != null && !currentPosition.CloseToPoint(prevTarget.Value))
                return prevTarget.Value;

            var targets = Targets();
            var rnd = new System.Random();

            var result = prevTarget;
            while (result == prevTarget)
            {
                result = targets[rnd.Next(4)];
            }

            return result.Value;
        }

        private Vector2[] Targets()
        {
            return new[]
            {
                new Vector2(1f, 1f),
                new Vector2(18f, 1f),
                new Vector2(18f, 13f),
                new Vector2(1f, 13f)
            };
        }

        private void MoveTo(Vector2 position)
        {
            transformComponent.Direction = position - transform.position.ToVector2();
        }
        
        private Vector2? prevTarget;
        private Map map;

        private TransformComponent transformComponent;
    }
}