using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Limits (required)")]
    [SerializeField] Transform leftWall;    
    [SerializeField] Transform rightWall;   
    [SerializeField] float padding = 0.1f;  

    [Header("Mouse follow")]
    [SerializeField] bool smoothFollow = true;
    [SerializeField] float followSpeed = 12f;  

    float minX, maxX;     
    float halfPaddle;     
    Camera cam;

    void Start()
    {
        cam = Camera.main;

        var sr = GetComponent<SpriteRenderer>();
        halfPaddle = sr ? sr.bounds.extents.x : transform.localScale.x * 0.5f;

       // i really can't figure this out so i chatgpt it...
        var leftSR  = leftWall.GetComponent<SpriteRenderer>();
        var rightSR = rightWall.GetComponent<SpriteRenderer>();

        
        float leftInner  = leftSR.bounds.max.x; 
        float rightInner = rightSR.bounds.min.x;


        minX = InnerRightEdge(leftWall)  + halfPaddle + padding;   
        maxX = InnerLeftEdge(rightWall) - halfPaddle - padding;   
    }

    void Update()
    {
        // mouseX 
      //i think might be the clamping that is wrong
    float t = Mathf.Clamp01(cam.ScreenToViewportPoint(Input.mousePosition).x); 
    float targetX = Mathf.Lerp(minX, maxX, t);                                  
;

        // move
        float newX = smoothFollow
            ? Mathf.Lerp(transform.position.x, targetX, followSpeed * Time.deltaTime)
            : targetX;

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }


    // Right left 
    float InnerRightEdge(Transform wall)
    {
        float half = HalfWidthOf(wall);
        return wall.position.x + half;
    }

    // Left right
    float InnerLeftEdge(Transform wall)
    {
        float half = HalfWidthOf(wall);
        return wall.position.x - half;
    }

    float HalfWidthOf(Transform t)
    {
        var sr = t.GetComponent<SpriteRenderer>();
        return sr ? sr.bounds.extents.x : t.localScale.x * 0.5f;
    }
}
