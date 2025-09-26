using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ball"))
        {
            Destroy(gameObject);
        }
    }
}
