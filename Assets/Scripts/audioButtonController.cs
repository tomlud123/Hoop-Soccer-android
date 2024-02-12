using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioButtonController : MonoBehaviour
{
    public Sprite sOn;
    public Sprite sOff;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundOffOn(GameObject go)
    {
        AudioSource audio = go.GetComponent<AudioSource>();
        if (audio.isPlaying)
        {
            audio.Pause();
            image.sprite = sOff;
        }
        else
        {
            audio.Play();
            image.sprite = sOn;
        }
    }
}
