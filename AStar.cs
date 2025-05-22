using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    public static Queue<Vector3> FindPath(Vector2Int start, Vector2Int goal)
    {
        int[,] maze = MazeGenerator.Instance.maze;

        List<Node> openList = new List<Node>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();

        Node startNode = new Node(start, null, 0, GetHeuristic(start, goal));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.F.CompareTo(b.F));
            Node current = openList[0];

            if (current.Position == goal)
                return BuildPath(current);

            openList.Remove(current);
            closedSet.Add(current.Position);

            foreach (Vector2Int dir in GetDirections())
            {
                Vector2Int neighbor = current.Position + dir;

                if (!IsInBounds(maze, neighbor) || !IsWalkable(maze, neighbor) || closedSet.Contains(neighbor))
                    continue;

                float g = current.G + 1;
                Node existing = openList.Find(n => n.Position == neighbor);

                if (existing == null)
                {
                    Node newNode = new Node(neighbor, current, g, GetHeuristic(neighbor, goal));
                    openList.Add(newNode);
                }
                else if (g < existing.G)
                {
                    existing.G = g;
                    existing.Parent = current;
                }
            }
        }

        return new Queue<Vector3>(); // dacă nu găsește cale
    }

    private static Queue<Vector3> BuildPath(Node node)
    {
        Stack<Vector2Int> path = new Stack<Vector2Int>();

        while (node != null)
        {
            path.Push(node.Position);
            node = node.Parent;
        }

        Queue<Vector3> worldPath = new Queue<Vector3>();
        foreach (Vector2Int step in path)
        {
            worldPath.Enqueue(GridToWorld(step));
        }

        return worldPath;
    }

    private static float GetHeuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // distanță Manhattan
    }

    private static bool IsWalkable(int[,] maze, Vector2Int pos)
    {
        return maze[pos.y, pos.x] != 1;
    }

    private static bool IsInBounds(int[,] maze, Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < maze.GetLength(1) && pos.y >= 0 && pos.y < maze.GetLength(0);
    }

    private static Vector3 GridToWorld(Vector2Int grid)
    {
        return new Vector3(grid.x, 0, -grid.y); // adaptat la coordonatele din Unity
    }

    private static List<Vector2Int> GetDirections()
    {
        return new List<Vector2Int> {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
    }

    private class Node
    {
        public Vector2Int Position;
        public Node Parent;
        public float G; // cost de la start
        public float H; // estimare până la final
        public float F => G + H;

        public Node(Vector2Int pos, Node parent, float g, float h)
        {
            Position = pos;
            Parent = parent;
            G = g;
            H = h;
        }
    }
}
