using System;
using System.Collections;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public static int npcMode; //0 = multiplayer
    public float speed;                //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Sprite spriteNormal;
    public Sprite spriteSpeeded;
    public GameObject controllerText;
    public Rigidbody2D ball;
    public Rigidbody2D player1;
    private bool inSpeedMode;
    private float speedModeTimeEnd;
    private Vector2 lastMoveNpc = new Vector2(0, 0);
    private int moveDuplicationsNpc;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        speedModeTimeEnd = Time.time-8;
        controllerText.SetActive(false);
        StartCoroutine(startingWait());
    }

    private void FixedUpdate()
    {
        if (inSpeedMode && speedModeTimeEnd < Time.time)
        {
            inSpeedMode = false;
            speed = speed / 2;
        }

        if (npcMode==0)
        {
            if (!inSpeedMode && speedModeTimeEnd + 16 < Time.time)
            {
                controllerText.SetActive(true);
                if (Input.GetAxisRaw("F") == 1)
                {
                    controllerText.SetActive(false);
                    GetComponent<AudioSource>().Play();
                    inSpeedMode = true;
                    speedModeTimeEnd = Time.time + 3;
                    speed = speed * 2;
                }
            }

            float moveHorizontal = Input.GetAxisRaw("HorizontalWASD");
            float moveVertical = Input.GetAxisRaw("VerticalWASD");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }
        else//npc
        {
            int npcSpeedInterval = 30 - 3 * npcMode;
            if (npcMode == 1) npcSpeedInterval = 84;
            if (npcMode == 2) npcSpeedInterval = 49;
            if (npcSpeedInterval < 17) npcSpeedInterval = 33;
            if (!inSpeedMode && speedModeTimeEnd + npcSpeedInterval < Time.time && rb2d.constraints!= RigidbodyConstraints2D.FreezeAll)
            {
                GetComponent<AudioSource>().Play();
                inSpeedMode = true;
                speedModeTimeEnd = Time.time + 3;
                speed = speed * 2;
            }
            lastMoveNpc = AIMovement();
            rb2d.AddForce(lastMoveNpc * speed);
        }

    }
    private void LateUpdate()
    {
        if (inSpeedMode && GetComponent<SpriteRenderer>().sprite == spriteNormal)
        {
            GetComponent<SpriteRenderer>().sprite = spriteSpeeded;
        }
        if (!inSpeedMode && GetComponent<SpriteRenderer>().sprite == spriteSpeeded)
        {
            GetComponent<SpriteRenderer>().sprite = spriteNormal;
        }
    }

    private Vector2 AIMovement()//returns 2D Vector created from only numbers -1, 0, 1.
    {
        if (moveDuplicationsNpc < 24)
        {
            moveDuplicationsNpc = 0;
            System.Random random = new System.Random();
            Vector2 p2position = rb2d.position;
            Vector2 p1position = player1.position;
            Vector2 ballPosition = ball.position;
            Vector2 npcToBall = new Vector2(ballPosition.x - p2position.x, ballPosition.y - p2position.y);
            int randomRange = 26 + npcMode * 3;
            int rand = random.Next(randomRange);
            if (npcMode < 7)
            {
                if (rand == 0)
                    return new Vector2(0, 1);
                if (rand == 1)
                    return new Vector2(0, -1);
            }
            if ((rand < 8 && npcMode == 1) || ((rand == 2 || rand == 4) && npcMode != 8))
                return new Vector2(0, 0);
            if (rand > 16 && rand < (24 + npcMode) && ((p2position.y > 2 && p2position.x > 6.5) || (p2position.y > 2 && p2position.x < -6.5) ||
                (p2position.y < -2 && p2position.x > 6.5) || (p2position.y < -2 && p2position.x < -6.5)))//jak npc w rogu i los
            {
                if (p2position.y > 0)
                    return new Vector2(0, 1);
                else
                    return new Vector2(0, -1);
            }
            if (rand % 20 == 5 && player1.Distance(GetComponent<CircleCollider2D>()).distance < 1) // rob czasami nic jak blisko playera
                return new Vector2(0, 0);
            if (npcToBall.y > 1.5 && p2position.y > 0)//npc na gornej połowie, piłka conajmniej kawałek nad nim
            {
                if (npcToBall.x < 0.2)//npc na prawo od piłki
                {
                    return new Vector2(0, 1);
                }
                else
                {
                    return new Vector2(1, 0);
                }
            }
            if (npcToBall.y < -1.5 && p2position.y < 0)//npc na dolnej połowie, piłka conajmniej kawałek pod nim
            {
                if (npcToBall.x < 0.2)//npc na prawo od piłki
                {
                    return new Vector2(0, -1);
                }
                else
                {
                    return new Vector2(1, 0);
                }
            }
            if (npcMode > 5 && npcToBall.x < -3 && p2position.x < p1position.x && ball.velocity.x > 12) //blokowanie piłki
            {
                if (p2position.y < p1position.y)
                {
                    return new Vector2(-1, 1);
                }
                else
                {
                    return new Vector2(-1, -1);
                }
            }
            if ((npcToBall.x < -0.25 && npcMode < 3) || (npcToBall.x < -0.1 && npcMode > 2))//npc na prawo od pilki
            {
                float maxSide = Mathf.Max(Mathf.Abs(npcToBall.x), Mathf.Abs(npcToBall.y));
                Vector2 move = new Vector2(Convert.ToInt32((npcToBall.x / maxSide)), Convert.ToInt32((npcToBall.y / maxSide)));
                return move;
            }
            else //npc na lewo od piłki
            {
                if (npcMode > 1 && Mathf.Abs(npcToBall.y) < 1.3f)
                {
                    if (npcToBall.y > 0)
                    {
                        return new Vector2(1, -1);
                    }
                    else
                    {
                        return new Vector2(1, 1);
                    }
                }
                if (npcMode > 2 && Mathf.Abs(npcToBall.y) > 2 && Mathf.Abs(npcToBall.x) < 1.5)
                {
                    if (npcToBall.y > 0)
                    {
                        return new Vector2(1, 1);
                    }
                    else
                    {
                        return new Vector2(1, -1);
                    }
                }
                else
                {
                    if (rand == 26)
                        return new Vector2(1, -1);
                    else
                        return new Vector2(1, 0);
                }
            }
        }
        else
        {
            moveDuplicationsNpc++;
            return lastMoveNpc;
        }
    }
    IEnumerator startingWait()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}