using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;

public class MazeLevelView : MonoBehaviour
{
    [SerializeField] private Transform worldRoot;

    [SerializeField] private GameObject wallPrefab;

    [SerializeField] private GameObject floorPrefab;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameObject finishPrefab;

    [SerializeField] private GameObject runePrefab;

    public Button startButton;

    private float cellSize = 2f;

    private MazeData maze;

    private MazeGenerator generator = new();

    public event Action FinishReached;

    public PlayerMovement Build(MazeData data)
    {
        maze = data;

        ClearWorld();

        BuildMaze();

        SpawnRunes();

        SpawnFinish();

        startButton.gameObject.SetActive(true);

        return SpawnPlayer();
    }

    void BuildMaze()
    {
        for (int x = 0; x < maze.width; x++)
        {
            for (int y = 0; y < maze.height; y++)
            {
                MazeCell cell = maze.cells[x, y];

                float offsetX = -(maze.width * cellSize) / 2f;
                float offsetY = -(maze.height * cellSize) / 2f;

                Vector3 pos = new Vector3(
                    x * cellSize + offsetX,
                    y * cellSize + offsetY,
                    0);

                Instantiate(
                    floorPrefab,
                    pos,
                    Quaternion.identity,
                    worldRoot);

                if (cell.wallTop)
                {
                    CreateWall(pos + new Vector3(0, cellSize / 2f, 0),
                        new Vector3(cellSize, 0.2f, 1));
                }

                if (cell.wallBottom)
                {
                    CreateWall(pos + new Vector3(0, -cellSize / 2f, 0),
                        new Vector3(cellSize, 0.2f, 1));
                }

                if (cell.wallLeft)
                {
                    CreateWall(pos + new Vector3(-cellSize / 2f, 0, 0),
                        new Vector3(0.2f, cellSize, 1));
                }

                if (cell.wallRight)
                {
                    CreateWall(pos + new Vector3(cellSize / 2f, 0, 0),
                        new Vector3(0.2f, cellSize, 1));
                }
            }
        }
    }

    void CreateWall(Vector3 pos, Vector3 scale)
    {
        GameObject wall = Instantiate(
            wallPrefab,
            pos,
            Quaternion.identity,
            worldRoot);

        wall.transform.localScale = scale;
    }

    PlayerMovement SpawnPlayer()
    {
        GameObject obj = Instantiate(
            playerPrefab,
            new Vector3(-(maze.width * cellSize) / 2f, -(maze.height * cellSize) / 2f, 0),
            Quaternion.identity,
            worldRoot);

        PlayerMovement player = obj.GetComponent<PlayerMovement>();

        player.StopMovement();

        player.EnableTorch();

        Camera.main.GetComponent<CameraFollow>().target = obj.transform;

        return player;
    }

    void SpawnFinish()
    {
        Vector3 finishPos = new Vector3(
            (maze.width - 1) * cellSize - (maze.width * cellSize) / 2f,
            (maze.height - 1) * cellSize - (maze.height * cellSize) / 2f,
            0);

        GameObject finish = Instantiate(
            finishPrefab,
            finishPos,
            Quaternion.identity,
            worldRoot);

        FinishZone finishZone =
    finish.GetComponent<FinishZone>();

        finishZone.OnPlayerEntered += HandleFinishReached;
    }

    void ClearWorld()
    {
        while (worldRoot.childCount > 0)
        {
            DestroyImmediate(worldRoot.GetChild(0).gameObject);
        }

    }

    void SpawnRunes()
    {
        float offsetX =
            -(maze.width * cellSize) / 2f;

        float offsetY =
            -(maze.height * cellSize) / 2f;

        for (int x = 0; x < maze.width; x++)
        {
            for (int y = 0; y < maze.height; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);

                bool isCorrect = maze.path.path.Contains(cell);

                SpawnRuneGroup(
                    x,
                    y,
                    isCorrect,
                    offsetX,
                    offsetY);
            }
        }
    }

    void SpawnRuneGroup(int x, int y, bool isCorrect, float offsetX, float offsetY)
    {
        Vector3 center = new Vector3(
            x * cellSize + offsetX,
            y * cellSize + offsetY,
            -0.5f);

        List<char> runes = new();

        while (runes.Count < 4)
        {
            char randomRune = generator.GetRandomRune();

            if (!runes.Contains(randomRune) && randomRune != maze.correctRune)
            {
                runes.Add(randomRune);
            }
        }

        if (isCorrect && Random.Range(0, 10) < 5)
        {
            runes[Random.Range(0, runes.Count)] = maze.correctRune;
        }

        Vector3[] offsets =
        {
            new Vector3(-0.35f, 0.35f, 0),
            new Vector3(0.35f, 0.35f, 0),
            new Vector3(-0.35f, -0.35f, 0),
            new Vector3(0.35f, -0.35f, 0)
        };

        for (int i = 0; i < 4; i++)
        {
            GameObject runeObj =
                Instantiate(
                    runePrefab,
                    center + offsets[i],
                    Quaternion.identity,
                    worldRoot);

            TMP_Text text = runeObj.GetComponent<TMP_Text>();

            text.text = runes[i].ToString();

            text.color = new Color(0.7f, 0.7f, 0.7f);
        }
    }

    void HandleFinishReached()
    {
        FinishReached?.Invoke();
    }
}
