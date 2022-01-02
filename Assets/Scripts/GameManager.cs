using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Level
{
    public int NoOfSquareEnemies;
    public int NoOfTriangleEnemies;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Level [] levels;
    [SerializeField] float spawnRadius;
    [SerializeField] Enemy squareEnemyPrefab;
    [SerializeField] Enemy triangleEnemyPrefab;
    int currentLevelNo;
    int noOfSquareEnemiesLeft;
    int noOfTriangleEnemiesLeft;
    float spawnRate;
    float timeLeftToSpawn = 0;
    void Awake() => Instance = this;
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        ChangeLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        timeLeftToSpawn -= Time.deltaTime;
        if(timeLeftToSpawn <= 0)
        {
            int randSpawnNo = Random.Range(1, noOfSquareEnemiesLeft + noOfTriangleEnemiesLeft + 1);
            
            Vector3 spawnPoint = Vector3.zero;
            while(spawnPoint == Vector3.zero)
            {
                Vector2 point = Random.insideUnitCircle.normalized * spawnRadius;
                spawnPoint = new Vector3(point.x, 0, point.y);
            }
            
            if(randSpawnNo <= noOfSquareEnemiesLeft)
            {
                //Spawn Square.
                Instantiate(squareEnemyPrefab, spawnPoint, Quaternion.identity);
                noOfSquareEnemiesLeft --;
            }
            else
            {
                //Not Square.., Duh.
                Instantiate(triangleEnemyPrefab, spawnPoint, Quaternion.identity);
                noOfTriangleEnemiesLeft --;
            }
        }
    }

    void ChangeLevel(int NextLevel)
    {
        currentLevelNo = NextLevel;
        UIManager.Instance.UpdateLevel(currentLevelNo);
        spawnRate = 1 / (float)(levels[NextLevel -1].NoOfSquareEnemies + levels[NextLevel -1].NoOfTriangleEnemies);
        noOfSquareEnemiesLeft = levels[NextLevel -1].NoOfSquareEnemies;
        noOfTriangleEnemiesLeft = levels[NextLevel -1].NoOfTriangleEnemies;
    }

    public void Lose()
    {

    }
}
