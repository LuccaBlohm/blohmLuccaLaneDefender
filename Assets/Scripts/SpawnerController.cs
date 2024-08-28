using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimer());
        StartCoroutine(TimerUpdate());
    }

    IEnumerator SpawnTimer()
    {
        Selection();
        yield return new WaitForSeconds(spawnTime);
        Instantiate(enemy, selectedLane.transform.position, Quaternion.identity);
        StartCoroutine(SpawnTimer());
    }

    IEnumerator TimerUpdate()
    {
        yield return new WaitForSeconds(2);
        spawnTime = (spawnTime - 0.2f);
        if (spawnTime >= 1f)
        {
            StartCoroutine(TimerUpdate());
        }
    }

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
