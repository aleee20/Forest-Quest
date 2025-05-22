using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator Instance; // Singleton

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject coinPrefab;
    public GameObject bunnyPrefab;
    public GameObject hyenaPrefab;
    private float cellSize = 1f;
    private float wallThickness = 0.3f;
    private float overlapOffset = 0.05f;

    public float tileSize = 1f;

    public int[,] maze = new int[,] {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,4,2,2,1,2,2,2,1,2,2,2,2,2,1},
        {1,2,1,2,1,2,1,2,1,2,1,1,1,2,1},
        {1,2,1,2,0,0,1,2,2,2,0,0,1,2,1},
        {1,2,1,1,1,1,1,1,1,1,1,2,1,2,1},
        {1,2,2,2,2,2,2,2,2,2,1,2,1,2,1},
        {1,2,1,1,1,1,1,1,1,0,1,2,1,2,1},
        {1,2,1,0,0,0,0,2,1,0,1,2,1,2,1},
        {1,2,1,0,1,1,0,2,1,0,1,2,1,2,1},
        {1,2,1,0,1,3,0,2,0,0,1,2,1,2,1},
        {1,2,1,0,1,1,1,1,1,1,1,2,1,2,1},
        {1,2,2,0,0,0,2,2,2,2,2,2,1,4,1}, // hiena
        {1,1,1,1,1,1,1,1,1,1,1,1,1,2,1},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };

    void Start()
    {
        Instance = this;
        GenerateMaze();
    }

    void GenerateMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, -y * tileSize);
                int cell = maze[y, x];

                Instantiate(floorPrefab, position, Quaternion.identity, transform);

                if (cell == 1)
                {
                    GameObject wall = Instantiate(wallPrefab, position + Vector3.up * 0.5f, Quaternion.identity, transform);
                    wall.transform.localScale = new Vector3(1f, 0.4f, 1f);
                }
                else if (cell == 2)
                    Instantiate(coinPrefab, position + Vector3.up * 0.5f, Quaternion.identity, transform);
                else if (cell == 3)
                    Instantiate(bunnyPrefab, position + Vector3.up * 0.5f, Quaternion.identity);
                else if (cell == 4)
                    Instantiate(hyenaPrefab, position + Vector3.up * 0.5f, Quaternion.identity);

            }
        }
    }

    // ✅ Verifică dacă celula e liberă
    public bool IsWalkable(int x, int y)
    {
        if (y < 0 || y >= maze.GetLength(0) || x < 0 || x >= maze.GetLength(1))
            return false;

        return maze[y, x] != 1;
    }

    // ✅ Conversie din poziție în lume în poziție din matrice
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / tileSize);
        int y = Mathf.RoundToInt(-worldPos.z / tileSize);
        return new Vector2Int(x, y);
    }

    // ✅ Conversie din poziție în matrice în poziție din lume
    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        float y ;
        float x = gridPos.x * tileSize;
        float z = -gridPos.y * tileSize;
        return new Vector3(x, 0, z);
    }
}
