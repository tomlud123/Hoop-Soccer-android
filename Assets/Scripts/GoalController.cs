using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    public bool leftSide;
    public Text gText;
    public GameObject ball;
    public GameObject player1;
    public GameObject player2;
    public Sprite p1Normal;
    public Sprite p1Happy;
    public Sprite p2Normal;
    public Sprite p2Happy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == ball)
        {
            UIController.goalsText = gText;
            UIController.UpdateGoals(leftSide);
            GetComponent<AudioSource>().Play();
            StartCoroutine(prepareAfterGoal());
        }
    }
    IEnumerator prepareAfterGoal()
    {
        if (leftSide)
        {
            player2.GetComponent<SpriteRenderer>().sprite = p2Happy;
        } else
        {
            player1.GetComponent<SpriteRenderer>().sprite = p1Happy;
        }
        // suspend execution for 5 seconds
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1);
        player1.transform.position = new Vector3(-7, 0, 0);
        player2.transform.position = new Vector3(6.8f, 0, 0);
        ball.transform.position = new Vector3(-0.1f, 0, 0);
        yield return new WaitForSeconds(1);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        if (leftSide)
        {
            player2.GetComponent<SpriteRenderer>().sprite = p2Normal;
        }
        else
        {
            player1.GetComponent<SpriteRenderer>().sprite = p1Normal;
        }
    }
}