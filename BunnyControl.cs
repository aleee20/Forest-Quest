using UnityEngine;

public class BunnyController : MonoBehaviour
{
    public float moveSpeed = 5f;      // viteza de mișcare
    public float tileSize = 1f;

    private Vector3 direction = Vector3.zero;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                direction = new Vector3(-tileSize, 0, 0);  // Sus
            else if (Input.GetKey(KeyCode.DownArrow))
                direction = new Vector3(tileSize, 0, 0);   // Jos
            else if (Input.GetKey(KeyCode.LeftArrow))
                direction = new Vector3(0, 0, -tileSize);  // Stânga
            else if (Input.GetKey(KeyCode.RightArrow))
                direction = new Vector3(0, 0, tileSize);   // Dreapta
            else
                direction = Vector3.zero;

            if (direction != Vector3.zero)
            {
                bool hitWall = Physics.Raycast(transform.position, direction.normalized, tileSize * 0.6f);

                if (!hitWall)
                {
                    targetPosition = transform.position + direction;
                    isMoving = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}
