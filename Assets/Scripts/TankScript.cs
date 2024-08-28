using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class TankScript : MonoBehaviour
{
    [SerializeField] private ScoreKeeper scoreKeeper;

    [SerializeField] private float speed = 7;
    [SerializeField] private float moveDirection;
    [SerializeField] private bool tankMoving;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject bulletSpawner;

    public PlayerInput playerControls;
    private InputAction move;
    private InputAction shoot;

    [SerializeField] private AudioClip shootingSFX;

    private Vector3 audioReceptor = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        playerControls.currentActionMap.Enable();
        move = playerControls.currentActionMap.FindAction("Move");
        move.started += Move_started;
        move.canceled += Move_canceled;
        shoot = playerControls.currentActionMap.FindAction("Shoot");
        shoot.started += Shoot_started;
        shoot.canceled += Shoot_canceled;

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Shoot_canceled(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        explosion.SetActive(false);
    }

    private void Shoot_started(InputAction.CallbackContext context)
    {
        StartCoroutine(SpawnBullets());
    }

    private void Move_canceled(InputAction.CallbackContext context)
    {
        tankMoving = false;
    }

    private void Move_started(InputAction.CallbackContext context)
    {
        tankMoving = true;
    }

    IEnumerator SpawnBullets()
    {
        Instantiate(bullet, bulletSpawner.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(shootingSFX, audioReceptor);
        StartCoroutine(BulletExplosion());
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SpawnBullets());
    }

    IEnumerator BulletExplosion()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        explosion.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            scoreKeeper.Lives--;
        }
        else if (collision.gameObject.tag == "Snail")
        {
            scoreKeeper.Lives--;
        }
        else if (collision.gameObject.tag == "Snake")
        {
            scoreKeeper.Lives--;
        }
    }

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

    // Update is called once per frame
    void Update()
    {
        if (tankMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }
}
