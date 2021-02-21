using UnityEngine;

namespace Components
{
    public class TransformComponent : MonoBehaviour
    {
        public Vector2 Direction { get; set; }
        
        public void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();    
        }
        
        public void FixedUpdate()
        {
            rigidBody.velocity = Direction.normalized * accelerate;
            animator.SetFloat(speedAnimatorVariable, rigidBody.velocity.magnitude);
        }

        private readonly float accelerate = 10f;        
        private Animator animator;
        private Rigidbody2D rigidBody;
        private GameObject bulletPrefab;
        
        private static readonly int speedAnimatorVariable = Animator.StringToHash("Speed");
    }
}