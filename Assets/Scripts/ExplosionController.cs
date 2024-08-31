using System.Collections;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    /// <summary>
    /// the explosion begins to dissapear as soon as it spawns
    /// </summary>
    void Start()
    {
        StartCoroutine(Dissapear());
    }

    /// <summary>
    /// in half a second the game object will dissapear
    /// </summary>
    /// <returns></returns>
    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
