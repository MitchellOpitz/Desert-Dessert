using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    [Header("Generation Parameters")]
    [SerializeField] float TurnProbability;

    [Header("Sizes")]
    [SerializeField] Vector2 TileSize;
    [SerializeField] int MinMapSize;
    [SerializeField] int MaxMapSize;

    [Header("Transforms")]
    [SerializeField] Transform EndTransform;

    [Header("Prefabs")]
    [SerializeField] GameObject StraightRoadPrefab;
    [SerializeField] GameObject RightTurnPrefab;
    [SerializeField] GameObject LeftTurnPrefab;

    Vector3 CurrentRoadPosition;
    Vector2 EndPosition;

    float CurrentRoadRotation; // Change type from Vector3 to float

    // Direction changes based on rotation angles
    Dictionary<float, Vector3> DirectionChanges = new Dictionary<float, Vector3>()
    {
        { 0f, Vector3.up },    // Facing up
        { 90f, Vector3.right }, // Facing right
        { 180f, Vector3.down }, // Facing down
        { 270f, Vector3.left }  // Facing left
    };

    void Start()
    {
        InitialiseRoad();
    }

    void Update() {
        GenerateRoad();
    }

    void InitialiseRoad()
    {
        // Calculate the end position based on map size and tile size
        EndPosition.x = Random.Range(MinMapSize, MaxMapSize);
        EndPosition.y = Random.Range(MinMapSize, MaxMapSize);
        EndPosition *= TileSize;
        EndTransform.position = EndPosition;

        // Reset the road generation position to the start
        CurrentRoadPosition = transform.position;
        CurrentRoadRotation = 0f; // Change type from Vector3 to float
    }

    void GenerateRoad()
    {
        if (CurrentRoadPosition.y > EndPosition.y)
        {
            return;
        }

        Instantiate(StraightRoadPrefab, CurrentRoadPosition, Quaternion.Euler(0, 0, -CurrentRoadRotation));
        CurrentRoadPosition += DirectionChanges[CurrentRoadRotation] * TileSize.y;
    }
}
