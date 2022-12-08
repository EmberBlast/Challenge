using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class EnemySpawnerProfile
    {
        [SerializeField] private string profileName;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int totalEnemies;

        private List<GameObject> enemies = new List<GameObject>();

        public string ProfileName { get => profileName; private set => profileName = value; }
        public GameObject EnemyPrefab { get => enemyPrefab; private set => enemyPrefab = value; }
        public int TotalEnemies { get => totalEnemies; private set => totalEnemies = value; }
        public List<GameObject> Enemies { get => enemies; private set => enemies = value; }

        public void CreateEnemies()
        {
            GameObject container = new GameObject();
            container.name = profileName + " container";

            for (int i = 0; i < totalEnemies; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, container.transform);
                enemies.Add(enemy);
                enemy.gameObject.SetActive(false);
            }      
        }
    }

    [SerializeField] private List<EnemySpawnerProfile> spawnProfiles;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private float spwanRate;
    [SerializeField] private float spawnRateTime;

    private float spawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (Time.time - spawnTimer > spawnRateTime / spwanRate)
        {
            spawnTimer = Time.time;
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        int rand = UnityEngine.Random.Range(0,spawnProfiles.Count);
       
        for (int i = 0; i < spawnProfiles[rand].Enemies.Count; i++)
        {
            if (!spawnProfiles[rand].Enemies[i].activeInHierarchy)
            {
                spawnProfiles[rand].Enemies[i].SetActive(true);
                spawnProfiles[rand].Enemies[i].transform.position = GetRandomSpawnPoint();
                return;
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randPoint = UnityEngine.Random.Range(0, spawnPoints.childCount);
        return spawnPoints.GetChild(randPoint).transform.position;
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < spawnProfiles.Count; i++)
        {
            spawnProfiles[i].CreateEnemies();
        }
    }
}
