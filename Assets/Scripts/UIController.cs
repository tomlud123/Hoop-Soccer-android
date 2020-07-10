using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static int goals1;
    public static int goals2;
    public static Text goalsText;
    public Text timeText;
    public Text levelText;
    float startGameTime;
    private int timeUntilEnd;
    private int lastTick;
    private int MATCH_LENGTH=120;//TODO 90~~nwm
    // Start is called before the first frame update
    void Start()
    {
        goals1 = 0;
        goals2 = 0;
        startGameTime = Time.time;
        lastTick = 5;
        if (Player2Controller.npcMode == 0)
            levelText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilEnd = (int) (startGameTime + MATCH_LENGTH - Time.time);
        timeText.text = "Time: " + timeUntilEnd.ToString() + " s";
    }
    private void FixedUpdate()
    {
        if (Time.time - startGameTime > 1.5f)
        {
            levelText.enabled = false;
        }
        if (levelText.isActiveAndEnabled)
        {
            levelText.text = "Level " + Player2Controller.npcMode;
            levelText.fontSize = 40 + (int)((Time.time - startGameTime) * 150);
        }
    }

    public static void UpdateGoals(bool leftSide)
    {
        if (leftSide)
        {
            goals2++;
        }
        else
        {
            goals1++;
        }
        goalsText.text = goals1 + "  :  " + goals2;
    }
    private void LateUpdate()
    {
        if (timeUntilEnd==lastTick - 1 && timeUntilEnd!=0)
        {
            lastTick--;
            GetComponent<AudioSource>().Play();
        }
        if (timeUntilEnd < 1)
        {
            if (Player2Controller.npcMode == 0 || goals1 <= goals2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                if (Player2Controller.npcMode != 8)
                {
                    Player2Controller.npcMode++;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                }
            }

        }
    }
    public void goToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
