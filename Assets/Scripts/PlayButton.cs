using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame(bool withNpc)
    {
        if (withNpc)
        {
            Player2Controller.npcMode = 1;
        }
        else
        {
            Player2Controller.npcMode = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
