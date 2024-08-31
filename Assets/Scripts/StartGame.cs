using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    /// <summary>
    /// when the start button is clicked begin the game proper
    /// </summary>
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
