using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIncreaser : MonoBehaviour
{
    private float oldDrag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        oldDrag = collision.attachedRigidbody.drag;
        collision.attachedRigidbody.drag *= 3f;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.attachedRigidbody.drag = oldDrag;
    }
}
