using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EndGameController : MonoBehaviour
{
    public Sprite p1Pic;
    public Sprite p2Pic;
    public Image image;
    private float startTime;

    //ads variables
    string gameId = "3704809";
    bool testMode = false;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        startTime = Time.time;
        if (Player2Controller.npcMode == 0)
        {
            if (UIController.goals1 == UIController.goals2)
            {
                GetComponent<Text>().text = "Draw!";
            }
            else if (UIController.goals1 > UIController.goals2)
            {
                GetComponent<Text>().text = "Player1 has won!";
                image.sprite = p1Pic;
            }
            else if (UIController.goals1 < UIController.goals2)
            {
                GetComponent<Text>().text = "Player2 has won!";
                image.sprite = p2Pic;
            }
        }
        else
        {
            if (UIController.goals1 > UIController.goals2)//won the whole game
            {
                GetComponent<Text>().text = "Congratulations!!! You have won all 8 rounds!!";
                image.sprite = p1Pic;
            }
            else
            {
                if (Player2Controller.npcMode == 1)
                {
                    GetComponent<Text>().text = "You haven't won the match. Try again!";
                    image.sprite = p2Pic;
                }
                else if (Player2Controller.npcMode == 2)
                {
                    GetComponent<Text>().text = "You passed 1 round! Not bad!";
                    image.sprite = p1Pic;
                }
                else
                {
                    GetComponent<Text>().text = "You have won " + (Player2Controller.npcMode - 1) + " of 8 rounds! Really good!";
                    image.sprite = p1Pic;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.position = image.transform.position + new Vector3 (0, 1* Mathf.Sin(Mathf.PI * ((Time.time - startTime) / 1f)), 0);
    }
    public void goToMenu()
    {
        Advertisement.Show();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }
}
