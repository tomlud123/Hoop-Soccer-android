using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{

    public float speed;                //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.asd
    public Sprite spriteNormal;
    public Sprite spriteSpeeded;
    public Button turboButton;
    public GameObject player2;
    public AudioSource tickSource;
    public AudioSource audioSpeedMode;
    public VariableJoystick variableJoystick;
    public Image turboImage;
    private bool inSpeedMode;
    private float speedModeTimeEnd;
    private bool turboPressed;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        speedModeTimeEnd = Time.time-8;
        turboPressed = false;
        setDisabledTurbo();
        StartCoroutine(startingWait());
    }

    private void FixedUpdate()
    {
        if (!inSpeedMode && speedModeTimeEnd + 16 < Time.time)
        {
            setEnabledTurbo();
            if (turboPressed)
            {
                setDisabledTurbo();
                audioSpeedMode.Play();
                inSpeedMode = true;
                speedModeTimeEnd = Time.time + 3;
                speed = speed * 2;
            }
        }
        if (inSpeedMode && speedModeTimeEnd < Time.time)
        {
            inSpeedMode = false;
            speed = speed / 2;
        }

        //Store the current horizontal input in the float moveHorizontal.
        //float moveHorizontal = Input.GetAxisRaw("Horizontal");

        //Store the current vertical input in the float moveVertical.
        //float moveVertical = Input.GetAxisRaw("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        Vector2 movement = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical) * 3f;
        if (movement.x > 1) movement.x = 1;
        if (movement.x < -1) movement.x = -1;
        if (movement.y > 1) movement.y = 1;
        if (movement.y < -1) movement.y = -1;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb2DPlayer2 = player2.GetComponent<Rigidbody2D>();
        if (collision.rigidbody == rb2DPlayer2 || collision.otherRigidbody == rb2DPlayer2)
        {
            Vector2 p1ToP2 = new Vector2(rb2DPlayer2.position.x - rb2d.position.x, rb2DPlayer2.position.y - rb2d.position.y);
            rb2d.AddForce(new Vector2 (-p1ToP2.x, -p1ToP2.y)*70, ForceMode2D.Impulse);
            rb2DPlayer2.AddForce(p1ToP2*70, ForceMode2D.Impulse);
        }
    }
    IEnumerator startingWait()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        tickSource.Play();
    }
    private void setEnabledTurbo()
    {
        turboButton.enabled = true;
        turboButton.image.color = new Color(1, 0.9f, 0.5f, 0.3f);
        turboImage.color = new Color(1, 1,1, 0.6f);
    }
    private void setDisabledTurbo()
    {
        turboButton.enabled = false;
        turboPressed = false;
        turboButton.image.color = new Color(1, 0.9f, 0.5f, 0.05f);
        turboImage.color = new Color(1, 1,1, 0.05f);
    }
    public void onClickTurboButton()
    {
        turboPressed = true;
    }


}