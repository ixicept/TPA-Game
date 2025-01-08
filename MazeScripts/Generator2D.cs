/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Graphs;

public class Generator2D : MonoBehaviour
{
    enum CellType
    {
        None,
        Heuristic,
        Hallway,
        Room
    }

    class Heuristic
    {
        public RectInt bounds;

        public Heuristic(Vector2Int location, Vector2Int size)
        {
            bounds = new RectInt(location, size);
        }

        public static bool Intersect(Heuristic a, Heuristic b)
        {
            return !((a.bounds.position.x >= (b.bounds.position.x + b.bounds.size.x)) || ((a.bounds.position.x + a.bounds.size.x) <= b.bounds.position.x)
                || (a.bounds.position.y >= (b.bounds.position.y + b.bounds.size.y)) || ((a.bounds.position.y + a.bounds.size.y) <= b.bounds.position.y));
        }
    }

    class Room
    {
        public RectInt bounds;

        public Room(Vector2Int location, Vector2Int size)
        {
            bounds = new RectInt(location, size);
        }

        public static bool Intersect(Room a, Room b)
        {
            return !((a.bounds.position.x >= (b.bounds.position.x + b.bounds.size.x)) || ((a.bounds.position.x + a.bounds.size.x) <= b.bounds.position.x)
                || (a.bounds.position.y >= (b.bounds.position.y + b.bounds.size.y)) || ((a.bounds.position.y + a.bounds.size.y) <= b.bounds.position.y));
        }
    }


    [SerializeField]
    Vector2Int size;
    [SerializeField]
    int roomCount;
    [SerializeField]
    Vector2Int roomMaxSize;
    [SerializeField]
    GameObject cubePrefab;
    [SerializeField]
    Material redMaterial;
    [SerializeField]
    Material blueMaterial;
    [SerializeField]
    Material greenMaterial;


    Random random;
    Grid2D<CellType> grid;
    List<Heuristic> heuristics;
    Delaunay2D delaunay;
    HashSet<Prim.Edge> selectedEdges;
    List<RectInt> rooms;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        random = new Random();
        grid = new Grid2D<CellType>(size, Vector2Int.zero);
        heuristics = new List<Heuristic>();
        rooms = new List<RectInt>();

        PlaceHeuristics();
        Triangulate();
        CreateHallways();
        PathfindHallways();
        PlaceRooms();
    }


    void PlaceRooms()
    {
        // Initialize room counter
        HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();

        List<Vector2Int> hallwayCells = FindHallwayCells();
        int roomCounter = 0;
        foreach (var heuristic in heuristics)
        {
            Debug.Log("Placing rooms for heuristic at: " + heuristic.bounds.position);


            Shuffle(hallwayCells);

            int maxAttempts = 5;
            int attempts = 0;

            while (roomCounter < 10 && attempts < maxAttempts)
            {

                Vector2Int hallwayPos = hallwayCells[attempts];


                Vector2Int roomPos = GetRandomPositionAdjacentToHallway(hallwayPos);

                Vector2Int roomSize = new Vector2Int(2, 2);


                PlaceRoom(roomPos, roomSize);
                roomCounter++;
                occupiedPositions.Add(roomPos);

                Debug.Log(roomPos);
                attempts++;
            }
        }
    }

    List<Vector2Int> FindHallwayCells()
    {
        List<Vector2Int> hallwayCells = new List<Vector2Int>();

        for (int x = 0; x < grid.Size.x; x++)
        {
            for (int y = 0; y < grid.Size.y; y++)
            {
                if (grid[x, y] == CellType.Hallway)
                {
                    hallwayCells.Add(new Vector2Int(x, y));
                }
            }
        }

        return hallwayCells;
    }
    bool IsRoomPositionValid(Vector2Int position, Vector2Int size)
    {

        RectInt newRoom = new RectInt(position, size);

        if (newRoom.xMin < 0 || newRoom.yMin < 0 || newRoom.xMax >= size.x || newRoom.yMax >= size.y)
        {
            return false;
        }


        foreach (var room in rooms)
        {
            if (room.Overlaps(newRoom))
            {
                return false;
            }
        }
        return true;
    }
    Vector2Int GetRandomPositionAdjacentToHallway(Vector2Int hallwayPos)
    {
        int x = hallwayPos.x + random.Next(-1, 2);
        int y = hallwayPos.y + random.Next(-1, 2);
        return new Vector2Int(x, y);
    }
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void PlaceHeuristics()
    {
        for (int i = 0; i < roomCount; i++)
        {
            Vector2Int location = new Vector2Int(
                random.Next(roomMaxSize.x, size.x - roomMaxSize.x),
                random.Next(roomMaxSize.y, size.y - roomMaxSize.y)
            );

            Vector2Int roomSize = new Vector2Int(
                random.Next(1, roomMaxSize.x + 1),
                random.Next(1, roomMaxSize.y + 1)
            );


            Heuristic newHeuristic = new Heuristic(location, roomSize);
            heuristics.Add(newHeuristic);

            PlaceHeuristic(newHeuristic.bounds.position, newHeuristic.bounds.size);

            foreach (var pos in newHeuristic.bounds.allPositionsWithin)
            {
                grid[pos] = CellType.Heuristic;
            }
        }
    }

    void Triangulate()
    {
        List<Vertex> vertices = new List<Vertex>();

        foreach (var heuristic in heuristics)
        {
            vertices.Add(new Vertex<Heuristic>((Vector2)heuristic.bounds.position + ((Vector2)heuristic.bounds.size) / 2, heuristic));
        }

        delaunay = Delaunay2D.Triangulate(vertices);
    }

    void CreateHallways()
    {
        List<Prim.Edge> edges = new List<Prim.Edge>();

        foreach (var heuristic1 in heuristics)
        {
            foreach (var heuristic2 in heuristics)
            {
                if (heuristic1 != heuristic2)
                {
                    edges.Add(new Prim.Edge(new Vertex<Heuristic>(heuristic1.bounds.center, heuristic1),
                                             new Vertex<Heuristic>(heuristic2.bounds.center, heuristic2)));
                }
            }
        }

        List<Prim.Edge> mst = Prim.MinimumSpanningTree(edges, edges[0].U);

        selectedEdges = new HashSet<Prim.Edge>(mst);
        var remainingEdges = new HashSet<Prim.Edge>(edges);
        remainingEdges.ExceptWith(selectedEdges);

        foreach (var edge in remainingEdges)
        {
            if (random.NextDouble() < 0.125)
            {
                selectedEdges.Add(edge);
            }
        }
    }

    void PathfindHallways()
    {
        DungeonPathfinder2D aStar = new DungeonPathfinder2D(size);

        foreach (var edge in selectedEdges)
        {
            var startHeuristic = (edge.U as Vertex<Heuristic>).Item;
            var endHeuristic = (edge.V as Vertex<Heuristic>).Item;

            var startPosf = startHeuristic.bounds.center;
            var endPosf = endHeuristic.bounds.center;
            var startPos = new Vector2Int((int)startPosf.x, (int)startPosf.y);
            var endPos = new Vector2Int((int)endPosf.x, (int)endPosf.y);

            var path = aStar.FindPath(startPos, endPos, (DungeonPathfinder2D.Node a, DungeonPathfinder2D.Node b) =>
            {
                var pathCost = new DungeonPathfinder2D.PathCost();

                pathCost.cost = Vector2Int.Distance(b.Position, endPos);

                if (grid[b.Position] == CellType.Heuristic)
                {
                    pathCost.cost += 2;
                }
                else if (grid[b.Position] == CellType.None)
                {
                    pathCost.cost += 5;
                }
                else if (grid[b.Position] == CellType.Hallway)
                {
                    pathCost.cost += 1;
                }

                pathCost.traversable = true;

                return pathCost;
            });

            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    var current = path[i];

                    if (grid[current] == CellType.None)
                    {
                        grid[current] = CellType.Hallway;
                    }

                    if (i > 0)
                    {
                        var prev = path[i - 1];

                        var delta = current - prev;
                    }
                }

                foreach (var pos in path)
                {
                    if (grid[pos] == CellType.Hallway)
                    {
                        PlaceHallway(pos);
                    }
                }
            }
        }
    }


    void PlaceCube(Vector2Int location, Vector2Int size, Material material)
    {
        GameObject go = Instantiate(cubePrefab, new Vector3(location.x, 0, location.y), Quaternion.identity);
        go.GetComponent<Transform>().localScale = new Vector3(size.x, 1, size.y);
        go.GetComponent<MeshRenderer>().material = material;
    }

    void PlaceRoom(Vector2Int location, Vector2Int size)
    {
        PlaceCube(location, size, greenMaterial);
    }
    void PlaceHeuristic(Vector2Int location, Vector2Int size)
    {
        PlaceCube(location, size, redMaterial);
    }

    void PlaceHallway(Vector2Int location)
    {
        PlaceCube(location, new Vector2Int(1, 1), blueMaterial);
    }
}
*/