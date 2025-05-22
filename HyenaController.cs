using UnityEngine;
using System.Collections.Generic;

public class HyenaController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float moveInterval = 0.2f; // Timp între pași

    private List<Vector3> path = new List<Vector3>();
    private int pathIndex = 0;
    private float moveTimer = 0f;

    private Transform bunny;

    void Start()
    {
        bunny = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(UpdatePath), 0f, 1f); // Recalculează drumul la fiecare 1s
    }

    void Update()
    {
        moveTimer += Time.deltaTime;

        if (path != null && pathIndex < path.Count)
        {
            Vector3 targetPos = path[pathIndex];

            // ✅ Păstrăm înălțimea curentă (Y) ca să nu fie trasă în podea
            targetPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                pathIndex++;
                moveTimer = 0f;
            }
        }
    }

    void UpdatePath()
    {
        Vector2Int start = MazeGenerator.Instance.WorldToGrid(transform.position);
        Vector2Int end = MazeGenerator.Instance.WorldToGrid(bunny.position);

        Queue<Vector3> newPath = AStar.FindPath(start, end);
        path = new List<Vector3>(newPath);
        pathIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage();
            }
        }
    }
}
