using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    public float Speed = 5.0f;

    [SerializeField] private KeyCode _upDirection = KeyCode.W;   // Default W
    [SerializeField] private KeyCode _downDirection = KeyCode.S; // Default S

    private float _direction;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Move paddle vertically
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _direction * Speed);
    }

    private void Update()
    {
        _direction = 0.0f;

        if (Input.GetKey(_upDirection))
            _direction += 1.0f;

        if (Input.GetKey(_downDirection))
            _direction -= 1.0f;
    }
}
