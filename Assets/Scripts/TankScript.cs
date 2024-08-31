using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TankScript : MonoBehaviour
{
    /// <summary>
    /// variables used throughout the script
    /// </summary>
    [SerializeField] private ScoreKeeper scoreKeeper;

    [SerializeField] private float speed = 7;
    [SerializeField] private float moveDirection;
    [SerializeField] private bool tankMoving;

    private Vector2 enemy;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject contactExplosion;
    [SerializeField] private GameObject bulletSpawner;

    public PlayerInput playerControls;
    private InputAction move;
    private InputAction shoot;
    private InputAction restart;

    [SerializeField] private AudioClip shootingSFX;

    private Vector3 audioReceptor = new Vector3(0, 0, -10);

    /// <summary>
    /// on start set the controls
    /// </summary>
    void Start()
    {
        playerControls.currentActionMap.Enable();
        move = playerControls.currentActionMap.FindAction("Move");
        move.started += Move_started;
        move.canceled += Move_canceled;
        shoot = playerControls.currentActionMap.FindAction("Shoot");
        shoot.started += Shoot_started;
        shoot.canceled += Shoot_canceled;
        restart = playerControls.currentActionMap.FindAction("Restart");
        restart.started += Restart_started;
        restart.canceled += Restart_canceled;

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Restart_canceled(InputAction.CallbackContext context)
    {
        //nothing happens when you lift up on restart
    }

    /// <summary>
    /// when restart button is clicked reload the scene
    /// </summary>
    /// <param name="context"></param>
    private void Restart_started(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// stop shooting when button isnt clicked/held
    /// </summary>
    /// <param name="context"></param>
    private void Shoot_canceled(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        explosion.SetActive(false);
    }

    /// <summary>
    /// shoot when button is clicked/held
    /// </summary>
    /// <param name="context"></param>
    private void Shoot_started(InputAction.CallbackContext context)
    {
        StartCoroutine(SpawnBullets());
    }
    
    /// <summary>
    /// stop moving when button isnt clicked/held
    /// </summary>
    /// <param name="context"></param>
    private void Move_canceled(InputAction.CallbackContext context)
    {
        tankMoving = false;
    }

    /// <summary>
    /// move when buttons are clicked/held
    /// </summary>
    /// <param name="context"></param>
    private void Move_started(InputAction.CallbackContext context)
    {
        tankMoving = true;
    }

    /// <summary>
    /// spawn a bullet in front of the tank when shoot is clicked
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnBullets()
    {
        Instantiate(bullet, bulletSpawner.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(shootingSFX, audioReceptor);
        StartCoroutine(BulletExplosion());
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SpawnBullets());
    }

    /// <summary>
    /// spawn a explosion to hide the spawning bullet
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletExplosion()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        explosion.SetActive(false);
    }

    /// <summary>
    /// when an enemy hits the tank spawn an explosion on the enemy, destroy the enemy, and lose a life
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            enemy = new Vector2(gameObject.transform.position.x + 0.8f, gameObject.transform.position.y);
            Instantiate(contactExplosion, enemy, Quaternion.identity);
            scoreKeeper.Lives--;
        }
        else if (collision.gameObject.tag == "Snail")
        {
            enemy = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            Instantiate(contactExplosion, enemy, Quaternion.identity);
            scoreKeeper.Lives--;
        }
        else if (collision.gameObject.tag == "Snake")
        {
            enemy = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            Instantiate(contactExplosion, enemy, Quaternion.identity);
            scoreKeeper.Lives--;
        }
    }

    /// <summary>
    /// set up the speed whenever movement is clicked
    /// </summary>
    private void FixedUpdate()
    {
        if (tankMoving)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0 ,speed * moveDirection);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }

    /// <summary>
    /// stop all controls on destroy to prevent errors
    /// </summary>
    private void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        shoot.started -= Shoot_started;
        shoot.canceled -= Shoot_canceled;
        restart.started -= Restart_started;
        restart.canceled -= Restart_canceled;
    }

    /// <summary>
    /// every frame check if tank is moving and if games over end all controls
    /// </summary>
    void Update()
    {
        if (tankMoving)
        {
            moveDirection = move.ReadValue<float>();
        }

        if (scoreKeeper.GameOver)
        {
            move.started -= Move_started;
            move.canceled -= Move_canceled;
            shoot.started -= Shoot_started;
            shoot.canceled -= Shoot_canceled;
        }
    }
}
