using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// variables used throughout the script
    /// </summary>
    [SerializeField] private ScoreKeeper scoreKeeper;

    [SerializeField] private GameObject explosion;

    private Vector2 offset;

    private int speed = 10;

    /// <summary>
    /// on start gets the scorekeeper script
    /// </summary>
    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    /// <summary>
    /// when a bullet collides with an object it gets destroyed and spawns an explosion
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        offset = new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y);
        Instantiate(explosion, offset, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// every frame the bulllet is moving except when the game is over
    /// </summary>
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        if (scoreKeeper.GameOver)
        {
            speed = 0;
        }
    }
}
