using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    private Vector2 startTouchPosition; // Starting position of the touch
    private Vector2 endTouchPosition;   // Ending position of the touch
    public float swipeThreshold = 50f; // Minimum swipe distance to register a swipe

    public float moveSpeed = 5f; // Speed of ball movement
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleSwipeInput();
    }

    void HandleSwipeInput()
    {
        if (Input.touchCount > 0) // Check if the screen is touched
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        // Ensure the swipe distance is above the threshold
        if (swipeDelta.magnitude > swipeThreshold)
        {
            // Horizontal swipe detection
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
        }
    }

    void MoveRight()
    {
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
        Debug.Log("Swiped Right");
    }

    void MoveLeft()
    {
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);
        Debug.Log("Swiped Left");
    }
}
