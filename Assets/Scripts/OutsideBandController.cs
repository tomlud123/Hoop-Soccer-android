using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBandController : MonoBehaviour
{
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ball.transform.position = new Vector3(-0.1f, 0, 0);
    }
}
