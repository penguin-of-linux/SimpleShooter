using UnityEngine;
namespace Controllers.UnitsController
{
    public abstract class UnitBaseController : EntityBaseController
    {
        public override void Start()
        {
            base.Start();
            
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
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

        private readonly float accelerate = 10f;
        
        private Animator animator;
        private Rigidbody2D rigidBody;
        
        private static readonly int speedAnimatorVariable = Animator.StringToHash("Speed");
    }
}