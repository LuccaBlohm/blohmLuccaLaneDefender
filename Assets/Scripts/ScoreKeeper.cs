using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private float lives = 3;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    public float Score { get => score; set => score = value; }
    public float Lives { get => lives; set => lives = value; }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + Score;
        livesText.text = "Lives: " + Lives;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Score;
        livesText.text = "Lives: " + Lives;
    }
}
