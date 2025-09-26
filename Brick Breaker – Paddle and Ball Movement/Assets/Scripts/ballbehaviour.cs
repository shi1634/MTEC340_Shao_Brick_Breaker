using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ballbehaviour : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] float speed = 8f;
    [SerializeField, Range(0f, 0.6f)] float minXFraction = 0.15f;
    [SerializeField, Range(0f, 0.2f)] float collisionJitter = 0.05f; 
    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {
    
        Vector2 dir = new Vector2(1f, 1f).normalized;
        rb.linearVelocity = dir * speed;
    }

    void FixedUpdate()
    {
        
        if (rb.linearVelocity.sqrMagnitude > 0.001f)
            rb.linearVelocity = rb.linearVelocity.normalized * speed;

       
        EnforceMinX();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collisionJitter > 0f)
        {
            Vector2 v = rb.linearVelocity;
            v += new Vector2(Random.Range(-collisionJitter, collisionJitter),
                             Random.Range(-collisionJitter, collisionJitter));
            rb.linearVelocity = v.normalized * speed;
        }

        EnforceMinX();
    }

    void EnforceMinX()
    {
        Vector2 v = rb.linearVelocity;
        float minX = speed * minXFraction;

        
        if (Mathf.Abs(v.x) < minX)
        {
            float sign = v.x >= 0f ? 1f : -1f;
            if (Mathf.Approximately(v.x, 0f)) sign = Random.value < 0.5f ? -1f : 1f;

            v.x = sign * minX;
            v = v.normalized * speed;
            rb.linearVelocity = v;
        }
    }
}
