using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    [Header("Generation Parameters")]
    [SerializeField] float RightProbability;
    [SerializeField] float LeftProbability;

    [Space]

    [SerializeField] float BranchPossibility;

    [Header("Sizes")]
    [SerializeField] Vector2 TileSize;
    [SerializeField] int MinMapSize;
    [SerializeField] int MaxMapSize;

    [Header("Transforms")]
    [SerializeField] Transform EndTransform;
    [SerializeField] Vector3 CurrentRoadPosition;

    [Header("Prefabs")]
    [SerializeField] GameObject StraightRoadPrefab;
    [SerializeField] GameObject LeftRoadPrefab;
    [SerializeField] GameObject RightRoadPrefab;

    Vector2 StartPosition; Vector2 EndPosition;

    List<Vector3> RoadPositions = new List<Vector3>();

    void Start()
    {
        InitialiseRoad();
    }

    void Update()
    {
        GenerateRoad();
    }

    void InitialiseRoad()
    {
        EndPosition.x = Random.Range(MinMapSize, MaxMapSize);
        EndPosition.y = Random.Range(MinMapSize, MaxMapSize);

        EndPosition *= TileSize;
        EndTransform.position = EndPosition;

        CurrentRoadPosition = Vector3.zero; // Reset current position
        RoadPositions.Clear(); // Clear existing road positions
        RoadPositions.Add(CurrentRoadPosition); // Add the starting position
    }

    void GenerateRoad()
    {
        if (CurrentRoadPosition.y + TileSize.y <= EndPosition.y)
        {
            CurrentRoadPosition += new Vector3(CurrentRoadPosition.x, TileSize.y, 0);

            float RandomValue = Random.value;

            if (RandomValue < RightProbability)
            {
                GenerateBranch(1, RightRoadPrefab);
            }
            else if (RandomValue < LeftProbability + RightProbability)
            {
                GenerateBranch(-1, LeftRoadPrefab);
            }
            else
            {
                Instantiate(StraightRoadPrefab, CurrentRoadPosition, Quaternion.identity);
            }
        }
    }

    void GenerateBranch(int Direction, GameObject BranchPrefab)
    {
        Vector3 BranchPosition = CurrentRoadPosition + new Vector3(TileSize.x * Direction, TileSize.y, 0);

        // Check for collisions with existing roads
        bool CanPlaceBranch = true;
        foreach (Vector3 Position in RoadPositions)
        {
            if (Vector3.Distance(BranchPosition, Position) < TileSize.x)
            {
                CanPlaceBranch = false;
                break;
            }
        }

        if (CanPlaceBranch)
        {
            Instantiate(BranchPrefab, BranchPosition, Quaternion.identity);
            RoadPositions.Add(BranchPosition);
            GenerateRoadFromBranch(BranchPosition);
        }
    }

    void GenerateRoadFromBranch(Vector3 BranchPosition)
    {
        Vector3 DirectionToEndpoint = ((Vector3)EndPosition - BranchPosition).normalized;
        Vector3 CurrentPos = BranchPosition;

        while (Vector3.Distance(CurrentPos, EndPosition) > TileSize.y)
        {
            CurrentPos += DirectionToEndpoint * TileSize.y;
            Instantiate(StraightRoadPrefab, CurrentPos, Quaternion.identity);
            RoadPositions.Add(CurrentPos);
        }
    }
}
