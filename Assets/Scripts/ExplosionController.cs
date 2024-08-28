using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dissapear());
    }

    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
