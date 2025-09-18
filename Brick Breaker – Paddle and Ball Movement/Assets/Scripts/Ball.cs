using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f;     
    Vector2 direction;           

    [Header("Walls (required)")]
    [SerializeField] Transform leftWall;
    [SerializeField] Transform rightWall;
    [SerializeField] Transform topWall;

    float leftEdge, rightEdge, topEdge;
    float halfBall;

    void Start()
    {
        // Ball size
        var sr = GetComponent<SpriteRenderer>();
        halfBall = sr ? sr.bounds.extents.x : transform.localScale.x * 0.5f;

        // Wall edges
        leftEdge  = leftWall.GetComponent<SpriteRenderer>().bounds.max.x; // inside edge
        rightEdge = rightWall.GetComponent<SpriteRenderer>().bounds.min.x;
        topEdge   = topWall.GetComponent<SpriteRenderer>().bounds.min.y;

        // Random start dir
        int randomX = Random.value < 0.5f ? -1 : 1;
        direction = new Vector2(randomX, 1).normalized;
    }

    void Update()
    {
        // Move
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Bounce left/right
        if (transform.position.x - halfBall <= leftEdge && direction.x < 0)
            direction.x *= -1;
        if (transform.position.x + halfBall >= rightEdge && direction.x > 0)
            direction.x *= -1;

        // Bounce top
        if (transform.position.y + halfBall >= topEdge && direction.y > 0)
            direction.y *= -1;
    }
}
