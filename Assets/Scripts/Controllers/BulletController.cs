using DefaultNamespace;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<TargetController>();
        if (target != null) target.Hit();
        //Destroy(other.gameObject);
        Destroy(gameObject);
    }
}