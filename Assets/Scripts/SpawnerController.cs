using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    /// <summary>
    /// variable thats will be used throughout the script
    /// </summary>
    [SerializeField] private ScoreKeeper scoreKeeper;

    private float spawnTime = 5;
    private int lane;
    private Transform selectedLane;
    private int enemyType;
    private GameObject enemy;

    [SerializeField] private GameObject pinkSlime;
    [SerializeField] private GameObject blueSlime;
    [SerializeField] private GameObject greenSlime;
    [SerializeField] private GameObject snail;
    [SerializeField] private GameObject snake;

    [SerializeField] private Transform lane1;
    [SerializeField] private Transform lane2;
    [SerializeField] private Transform lane3;
    [SerializeField] private Transform lane4;
    [SerializeField] private Transform lane5;

    /// <summary>
    /// on start set up both timers to loop
    /// </summary>
    void Start()
    {
        StartCoroutine(SpawnTimer());
        StartCoroutine(TimerUpdate());

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    /// <summary>
    /// after a certain amount of time an enemy spawn, repeat ad infinitum till end of game
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTimer()
    {
        Selection();
        yield return new WaitForSeconds(spawnTime);
        Instantiate(enemy, selectedLane.transform.position, Quaternion.identity);
        StartCoroutine(SpawnTimer());
    }

    /// <summary>
    /// every few seconds the time between enemy spawns gets shorter and shorter till it caps at 1 second
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerUpdate()
    {
        yield return new WaitForSeconds(2);
        spawnTime = (spawnTime - 0.2f);
        if (spawnTime >= 1f)
        {
            StartCoroutine(TimerUpdate());
        }
    }

    /// <summary>
    /// randomly select an enemy and a lane to spawn that enemy
    /// </summary>
    public void Selection()
    {
        enemyType = Random.Range(1, 6);
        switch (enemyType) 
        {
            case 1:
                enemy = greenSlime; 
                break;
            case 2:
                enemy = snail;
                break;
            case 3:
                enemy = blueSlime;
                break;
            case 4:
                enemy = snake;
                break;
            case 5:
                enemy = pinkSlime;
                break;
        }

        lane = Random.Range(1, 6);
        switch (lane)
        {
            case 1:
                selectedLane = lane1;
                break;
            case 2:
                selectedLane = lane2;
                break;
            case 3:
                selectedLane = lane3;
                break;
            case 4:
                selectedLane = lane4;
                break;
            case 5:
                selectedLane = lane5;
                break;
        }

    }


    /// <summary>
    /// when the game is over stop all coroutines from looping
    /// </summary>
    void Update()
    {
        if (scoreKeeper.GameOver)
        {
            StopAllCoroutines();
        }
    }
}
