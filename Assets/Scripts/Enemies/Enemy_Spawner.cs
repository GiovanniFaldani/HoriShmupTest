using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] List<Enemy_Data> enemyDatas;
    [SerializeField] int enemyNumber = 5;
    [SerializeField] float spawnInterval = 1f;

    [SerializeField] List<SplineContainer> splines;
    Enemy_Factory enemyFactory;

    float spawnTimer = 0f;
    int enemiesSpawned = 0;
    int current = 0;

    private void OnValidate()
    {
        splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
    }

    private void Start()
    {
        enemyFactory = new Enemy_Factory();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (enemiesSpawned < enemyNumber && spawnTimer >= spawnInterval)
        {
            SpawnEnemy(current);
            spawnTimer = 0f;

            // move on to next enemy type if current one has been exhaused
            if (enemiesSpawned == enemyNumber && current < enemyDatas.Count)
            {
                current++;
                enemiesSpawned = 0;
            }
            if (current >= enemyDatas.Count)
            {
                current = 0;
                enemiesSpawned = enemyNumber + 1;
            }
        }
    }

    private void SpawnEnemy(int typeIndex)
    {
        SplineContainer currentSpline = splines[typeIndex];
        if (currentSpline != null)
        {
            GameObject enemy = enemyFactory.CreateEnemy(enemyDatas[typeIndex], currentSpline);
        }
        else
        {
            // pick random spline from a list
            GameObject enemy = enemyFactory.CreateEnemy(enemyDatas[typeIndex], GameLibrary.Instance.randomSplines[Random.Range(0, GameLibrary.Instance.randomSplines.Count)].GetComponent<SplineContainer>());
        }
        enemiesSpawned++;
    }
}
