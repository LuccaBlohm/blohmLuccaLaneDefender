using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// variables used throughout the script
    /// </summary>
    [SerializeField] private ScoreKeeper scoreKeeper;
    Animator animator; 

    private int speed;
    private int savedSpeed;
    [SerializeField] private int health;
    private bool hit;

    [SerializeField] private AudioClip shotSFX;
    [SerializeField] private AudioClip dyingSFX;
    [SerializeField] private AudioClip hitSFX;

    private Vector3 audioReceptor = new Vector3(0, 0, -10);

    /// <summary>
    /// on start set each enemies stats and get their animator
    /// </summary>
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

        animator = gameObject.GetComponent<Animator>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    /// <summary>
    /// when  the enemy collides with anything a different interation happens
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(shotSFX, audioReceptor);
            gameObject.GetComponent<EnemyController>().health = health - 1;
            StartCoroutine(HitAnimation());
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

    /// <summary>
    /// when an enemy is hit play the hit animation for a second
    /// </summary>
    /// <returns></returns>
    IEnumerator HitAnimation()
    {
        animator.SetBool("dying", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("dying", false);
    }

    /// <summary>
    /// when an enemy is hit they stop for half a second
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// every frame the enemy is moving except for when the game is over
    /// </summary>
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        if (scoreKeeper.GameOver)
        {
            speed = 0;
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}
