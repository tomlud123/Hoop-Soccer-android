using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModeTxtController : MonoBehaviour
{
    public GameObject player;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.GetComponent<Rigidbody2D>().position + 
            new Vector2(0, 1.4f + 0.1f * Mathf.Sin(Mathf.PI * ((Time.time - startTime) / 0.2f)));
    }
}
