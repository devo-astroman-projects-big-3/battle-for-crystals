using System.Collections.Generic;
using UnityEngine;

public class SimpleMazeGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private int gridSizeW = 15;
    [SerializeField] private int gridSizeH = 17;
    [SerializeField] private float cellSize = 1f;

    [Header("Prefabs")]
    [SerializeField] private GameObject brickPrefab;

    [Header("Start / End")]
    [SerializeField] private Vector2Int startCoords = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int endCoords = new Vector2Int(19, 19);

    [Header("Maze Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float blockChance = 0.35f;

    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject ceilingPrefab;

    private readonly List<GameObject> spawnedFloors = new();
    private readonly List<GameObject> spawnedCeilings = new();

    private bool[,] blockedGrid;
    private readonly List<GameObject> spawnedBricks = new();

    [SerializeField] private bool generate = false;
    [SerializeField] private bool generateFloor = false;
    

    public void Update()
    {
        if (generate)
        {
            GenerateMaze();
            generate = false;
        }

        if (generateFloor)
        {
            GenerateFloor(floorPrefab, -0.05f, spawnedFloors);
            generateFloor = false;
        }
    }

    public void GenerateFloor(GameObject prefab, float yPosition, List<GameObject> targetList)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Floor/Ceiling prefab is missing.");
            return;
        }

        for (int x = 0; x < gridSizeW; x++)
        {
            for (int y = 0; y < gridSizeH; y++)
            {
                Vector3 position = new Vector3(
                    x * cellSize,
                    yPosition,
                    y * cellSize
                );

                GameObject tile = Instantiate(
                    prefab,
                    position,
                    Quaternion.identity,
                    transform
                );

                targetList.Add(tile);
            }
        }
    }

    [ContextMenu("Generate Maze")]
    public void GenerateMaze()
    {
        ClearMaze();

        startCoords = ClampCoords(startCoords);
        endCoords = ClampCoords(endCoords);

        blockedGrid = new bool[gridSizeW, gridSizeH];

        // 1. Randomly block cells
        for (int x = 0; x < gridSizeW; x++)
        {
            for (int y = 0; y < gridSizeH; y++)
            {
                blockedGrid[x, y] = Random.value < blockChance;
            }
        }

        // 2. Force start and end to be free
        blockedGrid[startCoords.x, startCoords.y] = false;
        blockedGrid[endCoords.x, endCoords.y] = false;

        // 3. Guarantee a path from start to end
        CreateGuaranteedPath();

        // 4. Spawn bricks for blocked cells
        SpawnBricks();
    }

    private void CreateGuaranteedPath()
    {
        Vector2Int current = startCoords;
        blockedGrid[current.x, current.y] = false;

        while (current != endCoords)
        {
            bool moveHorizontal = Random.value > 0.5f;

            if (moveHorizontal && current.x != endCoords.x)
            {
                current.x += current.x < endCoords.x ? 1 : -1;
            }
            else if (current.y != endCoords.y)
            {
                current.y += current.y < endCoords.y ? 1 : -1;
            }
            else if (current.x != endCoords.x)
            {
                current.x += current.x < endCoords.x ? 1 : -1;
            }

            blockedGrid[current.x, current.y] = false;
        }
    }

    private void SpawnBricks()
    {
        if (brickPrefab == null)
        {
            Debug.LogError("Brick prefab is missing.");
            return;
        }

        for (int x = 0; x < gridSizeW; x++)
        {
            for (int y = 0; y < gridSizeH; y++)
            {
                if (!blockedGrid[x, y])
                    continue;

                Vector3 position = new Vector3(
                    x * cellSize,
                    0f,
                    y * cellSize
                );

                GameObject brick = Instantiate(
                    brickPrefab,
                    position,
                    Quaternion.identity,
                    transform
                );

                spawnedBricks.Add(brick);
            }
        }
    }

    private Vector2Int ClampCoords(Vector2Int coords)
    {
        return new Vector2Int(
            Mathf.Clamp(coords.x, 0, gridSizeW - 1),
            Mathf.Clamp(coords.y, 0, gridSizeH - 1)
        );
    }

    private void ClearMaze()
    {
        foreach (GameObject brick in spawnedBricks)
            if (brick != null)
                DestroyImmediate(brick);

        foreach (GameObject floor in spawnedFloors)
            if (floor != null)
                DestroyImmediate(floor);

        foreach (GameObject ceiling in spawnedCeilings)
            if (ceiling != null)
                DestroyImmediate(ceiling);

        spawnedBricks.Clear();
        spawnedFloors.Clear();
        spawnedCeilings.Clear();
    }

    
}