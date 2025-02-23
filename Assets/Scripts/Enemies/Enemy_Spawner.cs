using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] List<Enemy_Data> enemyDatas;
    [SerializeField] List<int> enemyNumbers;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] List<float> waveTimes;
    [SerializeField] List<SplineContainer> splines;
    [SerializeField] Enemy_Data bossData;
    [SerializeField] SplineContainer bossSpline;
    Enemy_Factory enemyFactory;

    float globalTimer = 0f;
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
        GameLibrary.Instance.totalEnemies = enemyNumbers.Sum();
    }

    private void Update()
    {
        globalTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (enemiesSpawned < enemyNumbers[current] && spawnTimer >= spawnInterval)
        {
            SpawnEnemy(current);
            spawnTimer = 0f;
        }

        // move on to next enemy type if current one has been exhaused
        if (enemiesSpawned == enemyNumbers[current] && current < enemyDatas.Count && globalTimer >= waveTimes[current])
        {
            current++;
            enemiesSpawned = 0;
        }
        if (current >= enemyDatas.Count)
        {
            current = 0;
            enemiesSpawned = enemyNumbers[current] + 1;
        }
        if(GameLibrary.Instance.totalEnemies == 0)
        {
            SpawnBoss();
            GameLibrary.Instance.totalEnemies = -1;
        }
        
    }

    public IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void SpawnEnemy(int typeIndex)
    {
        ;
        if (typeIndex < splines.Count)
        {
            SplineContainer currentSpline = splines[typeIndex];
            GameObject enemy = enemyFactory.CreateEnemy(enemyDatas[typeIndex], currentSpline);
        }
        else
        {
            // pick random spline from a list
            GameObject enemy = enemyFactory.CreateEnemy(enemyDatas[typeIndex], GameLibrary.Instance.randomSplines[Random.Range(0, GameLibrary.Instance.randomSplines.Count)].GetComponent<SplineContainer>());
        }
        enemiesSpawned++;
    }

    private void SpawnBoss()
    {
        GameObject boss = enemyFactory.CreateBoss(bossData, bossSpline);
    }
}
