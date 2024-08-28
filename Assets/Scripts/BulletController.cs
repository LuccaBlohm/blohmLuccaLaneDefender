using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject explosion;

    private Vector2 offset;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        offset = new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y);
        Instantiate(explosion, offset, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
    }
}
