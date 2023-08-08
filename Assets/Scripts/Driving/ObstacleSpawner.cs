using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ObstacleList;
    [SerializeField] Transform[] SpawnPoints;

    [SerializeField] float SpawnRate;

    void Start() {
        foreach(Transform Point in SpawnPoints) {
            float RandomValue = Random.value;
            if(RandomValue <= SpawnRate) {
                int RandomObject = Random.Range(0, ObstacleList.Length);
                Instantiate(ObstacleList[RandomObject], Point.position, Quaternion.identity);
            }
        }
    }
}
