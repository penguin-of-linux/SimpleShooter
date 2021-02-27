using UnityEngine;

namespace Components
{
    public class TransformComponent : MonoBehaviour
    {
        public Vector2 Direction { get; set; }
        
        public void Start()
        {
            animator = GetComponent<Animator>();    
        }
        
        public void FixedUpdate()
        {
            //rigidBody.velocity = Direction.normalized * accelerate;
            var impulse = Direction * accelerate * Time.deltaTime;
            if (impulse.magnitude > 0)
            {
                gameObject.transform.position += impulse.ToVector3();
                animator.SetFloat(speedAnimatorVariable, impulse.magnitude);
            }
        }

        private readonly float accelerate = 3f;        
        private Animator animator;
        private GameObject bulletPrefab;
        
        private static readonly int speedAnimatorVariable = Animator.StringToHash("Speed");
    }
}