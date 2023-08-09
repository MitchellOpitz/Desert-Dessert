using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    [Header("Generation Parameters")]
    [SerializeField] float TurnProbability;
    [SerializeField] Vector2 TileSize;
    [SerializeField] int MinMapSize;
    [SerializeField] int MaxMapSize;
    [SerializeField] Transform EndTransform;
    [SerializeField] GameObject StraightRoadPrefab;
    [SerializeField] GameObject RightTurnPrefab;
    [SerializeField] GameObject LeftTurnPrefab;

    Vector2 CurrentGridPosition;
    Vector2 EndGridPosition;

    float CurrentGridRotation;

    Dictionary<float, Vector2> DirectionChanges = new Dictionary<float, Vector2>()
    {
        { 0f, Vector2.up },
        { 90f, Vector2.right },
        { 180f, Vector2.down },
        { 270f, Vector2.left }
    };

    HashSet<Vector2> OccupiedPositions = new HashSet<Vector2>();
    Stack<Vector2> BacktrackStack = new Stack<Vector2>();

    void Start()
    {
        InitializeRoad();
        GenerateRoad(CurrentGridPosition);
    }

    void InitializeRoad()
    {
        EndGridPosition.x = Random.Range(MinMapSize, MaxMapSize);
        EndGridPosition.y = Random.Range(MinMapSize, MaxMapSize);
        EndGridPosition *= TileSize;
        EndTransform.position = EndGridPosition;

        CurrentGridPosition = transform.position;
        CurrentGridRotation = 0f;

        OccupiedPositions.Clear();
        BacktrackStack.Clear();

        // Add starting point to occupied positions
        OccupiedPositions.Add(new Vector2(0, -26.5f));

        Instantiate(StraightRoadPrefab, EndGridPosition, Quaternion.identity);
        OccupiedPositions.Add(EndGridPosition);
    }

    void GenerateRoad(Vector2 position)
    {
        if (position.y > EndGridPosition.y)
        {
            return;
        }

        float randomValue = Random.value;
        bool rightTurn = Random.value > 0.5f;

        if (randomValue < TurnProbability)
        {
            Instantiate(StraightRoadPrefab, position, Quaternion.Euler(0, 0, -CurrentGridRotation));
            CurrentGridPosition += DirectionChanges[CurrentGridRotation] * TileSize;
        }
        else if (rightTurn)
        {
            Instantiate(RightTurnPrefab, position, Quaternion.Euler(0, 0, -CurrentGridRotation));
            CurrentGridRotation = (CurrentGridRotation + 90f) % 360f;
            CurrentGridPosition += DirectionChanges[CurrentGridRotation] * TileSize;
        }
        else
        {
            Instantiate(LeftTurnPrefab, position, Quaternion.Euler(0, 0, -CurrentGridRotation));
            CurrentGridRotation = (CurrentGridRotation + 270f) % 360f;
            CurrentGridPosition += DirectionChanges[CurrentGridRotation] * TileSize;
        }

        if (!OccupiedPositions.Contains(CurrentGridPosition))
        {
            OccupiedPositions.Add(CurrentGridPosition);
            GenerateRoad(CurrentGridPosition);
        }
        else
        {
            Backtrack(position);
        }
    }

    void Backtrack(Vector2 position)
    {
        if (BacktrackStack.Count > 0)
        {
            Vector2 lastPosition = BacktrackStack.Pop();

            if (!OccupiedPositions.Contains(lastPosition))
            {
                CurrentGridPosition = lastPosition;
                GenerateRoad(lastPosition);
            }
            else
            {
                Backtrack(position);
            }
        }
    }

}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}