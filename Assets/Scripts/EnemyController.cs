using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private ScoreKeeper scoreKeeper;
    [SerializeField] private Animator animator; 

    private int speed;
    private int savedSpeed;
    [SerializeField] private int health;
    private bool hit;

    [SerializeField] private AudioClip shotSFX;
    [SerializeField] private AudioClip dyingSFX;
    [SerializeField] private AudioClip hitSFX;

    private Vector3 audioReceptor = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Slime"))
        {
            speed = -3;
            savedSpeed = -3;
            health = 3;
        }
        if (gameObject.CompareTag("Snail"))
        {
            speed = -1;
            savedSpeed = -1;
            health = 5;
        }
        if (gameObject.CompareTag("Snake"))
        {
            speed = -5;
            savedSpeed = -5;
            health = 1;
        }

        animator = FindObjectOfType<Animator>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(shotSFX, audioReceptor);
            gameObject.GetComponent<EnemyController>().health = health - 1;
            StartCoroutine(Stop());
            if (health <= 0) 
            {
                AudioSource.PlayClipAtPoint(dyingSFX, audioReceptor);
                scoreKeeper.Score += 100;
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Tank")
        {
            AudioSource.PlayClipAtPoint(hitSFX, audioReceptor);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PastPlayer")
        {
            AudioSource.PlayClipAtPoint(hitSFX, audioReceptor);
            scoreKeeper.Lives--;
            Destroy(gameObject);
        }
    }

    IEnumerator Stop()
    {
        if (hit == false)
        {
            hit = true;
            speed = 0;
            yield return new WaitForSeconds(0.5f);
            speed = savedSpeed;
            hit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }
}
