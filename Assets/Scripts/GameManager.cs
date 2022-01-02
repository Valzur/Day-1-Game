using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] float gameSpeedModifier;
    [SerializeField] Enemy squareEnemyPrefab;
    [SerializeField] Enemy triangleEnemyPrefab;
    int currentLevelNo;
    int noOfSquareEnemiesLeft;
    int noOfTriangleEnemiesLeft;
    float spawnRate;
    float timeLeftToSpawn = 0;
    bool isSimulating = true;
    void Awake() => Instance = this;
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        ChangeLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSimulating)
            SpawnEnemies();
    }

    void SpawnEnemies()
    {
        timeLeftToSpawn -= Time.deltaTime;
        if(timeLeftToSpawn <= 0)
        {
            timeLeftToSpawn = 1 / spawnRate;
            int randSpawnNo = Random.Range(1, noOfSquareEnemiesLeft + noOfTriangleEnemiesLeft + 1);
            
            Vector3 spawnPoint = Vector3.zero;
            while(spawnPoint == Vector3.zero)
            {
                Vector2 point = Random.insideUnitCircle.normalized * spawnRadius;
                spawnPoint = new Vector3(point.x, 0, point.y);
            }
            
            if(randSpawnNo <= noOfSquareEnemiesLeft)
            {
                //Shift as we move
                spawnPoint += Player.Instance.transform.position;
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
            if(noOfTriangleEnemiesLeft == 0 && noOfSquareEnemiesLeft == 0)
            {
                if(currentLevelNo == levels.Length - 1)
                {
                    isSimulating = false;
                    UIManager.Instance.Win();
                    return;
                }
                ChangeLevel(currentLevelNo + 1);
            }
        }
    }

    void ChangeLevel(int NextLevel)
    {
        currentLevelNo = NextLevel;
        UIManager.Instance.UpdateLevel(currentLevelNo);
        spawnRate = gameSpeedModifier /((float)(levels[NextLevel].NoOfSquareEnemies + levels[NextLevel].NoOfTriangleEnemies));
        noOfSquareEnemiesLeft = levels[NextLevel].NoOfSquareEnemies;
        noOfTriangleEnemiesLeft = levels[NextLevel].NoOfTriangleEnemies;
    }

    public void Lose()
    {
        isSimulating = false;
        UIManager.Instance.Lose(currentLevelNo);
    }
    public void Restart() => SceneManager.LoadScene(1);
    public void Quit() => Application.Quit();
}
