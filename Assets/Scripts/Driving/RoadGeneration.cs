using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    [Header("Generation Parameters")]
    [SerializeField]
    float TurnProbability;

    [SerializeField]
    float TileSize;

    [SerializeField]
    int MinMapSize;

    [SerializeField]
    int MaxMapSize;

    [SerializeField]
    Transform EndTransform;

    [SerializeField]
    GameObject StraightRoadPrefab;

    [SerializeField]
    GameObject RightTurnPrefab;

    [SerializeField]
    GameObject LeftTurnPrefab;

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

    [SerializeField]
    GameObject BackgroundGraphics;

    [SerializeField]
    float BGSize;

    void Start()
    {
        GenerateBackground();
        InitializeRoad();
        OccupiedPositions.Add(new Vector2(0, -26.5f));
        GenerateRoad();
    }

    void InitializeRoad()
    {
        EndGridPosition.x = Random.Range(MinMapSize, MaxMapSize);
        EndGridPosition.y = Random.Range(MinMapSize, MaxMapSize);
        EndGridPosition *= TileSize;

        EndTransform.position = EndGridPosition;

        CurrentGridPosition = Vector2.zero;
        CurrentGridRotation = 0f;
    }

    void GenerateRoad()
    {
        if (CurrentGridPosition.y >= EndGridPosition.y)
        {
            Quaternion endRotation = Quaternion.Euler(new Vector3(0, 0, CurrentGridRotation));
            Instantiate(StraightRoadPrefab, CurrentGridPosition, endRotation);
            OccupiedPositions.Add(CurrentGridPosition);
            return;
        }

        float RandomValue = Random.value;

        Vector2 targetDirection = (EndGridPosition - CurrentGridPosition).normalized;
        float angleToTarget = Vector2.SignedAngle(
            DirectionChanges[CurrentGridRotation],
            targetDirection
        );

        if (RandomValue < TurnProbability || Mathf.Abs(angleToTarget) > 45f)
        {
            GameObject prefab;
            float rotation;

            if (Mathf.Abs(angleToTarget) > 45f)
            {
                if (angleToTarget > 0f)
                {
                    prefab = RightTurnPrefab;
                    rotation = CurrentGridRotation + 90f;
                    CurrentGridRotation = (CurrentGridRotation + 270f) % 360f;
                }
                else
                {
                    prefab = LeftTurnPrefab;
                    rotation = CurrentGridRotation + 90f;
                    CurrentGridRotation = (CurrentGridRotation + 90f) % 360f;
                }
            }
            else
            {
                prefab = StraightRoadPrefab;
                rotation = CurrentGridRotation;
            }

            Quaternion prefabRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            Vector2 newPosition =
                CurrentGridPosition + DirectionChanges[CurrentGridRotation] * TileSize;

            if (!OccupiedPositions.Contains(newPosition) && IsWithinBounds(newPosition))
            {
                Instantiate(prefab, CurrentGridPosition, prefabRotation);
                OccupiedPositions.Add(CurrentGridPosition);
                CurrentGridPosition = newPosition;
            }
            else
            {
                GenerateRoad();
            }
        }
        else
        {
            Vector2 newPosition =
                CurrentGridPosition + DirectionChanges[CurrentGridRotation] * TileSize;

            if (!OccupiedPositions.Contains(newPosition) && IsWithinBounds(newPosition))
            {
                Instantiate(
                    StraightRoadPrefab,
                    CurrentGridPosition,
                    Quaternion.Euler(new Vector3(0, 0, CurrentGridRotation))
                );
                OccupiedPositions.Add(CurrentGridPosition);
                CurrentGridPosition = newPosition;
            }
            else
            {
                GenerateRoad();
            }
        }

        GenerateRoad();
    }

    bool IsWithinBounds(Vector2 position)
    {
        return position.x >= 0
            && position.x <= MaxMapSize * TileSize
            && position.y >= 0
            && position.y <= MaxMapSize * TileSize;
    }

    void GenerateBackground()
    {
        for (float y = -MaxMapSize; y < (MaxMapSize / 0.5f * BGSize); y += BGSize)
        {
            for (float x = -MaxMapSize; x < (MaxMapSize / 0.5f * BGSize); x += BGSize)
            {
                Instantiate(
                    BackgroundGraphics,
                    new Vector3(x, y, 0),
                    Quaternion.identity,
                    transform.Find("BG")
                );
            }
        }
    }
}
