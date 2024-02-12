
using UnityEngine;
using UnityEngine.UI;

namespace Crazy { 

    [RequireComponent(typeof(Image))]
    public class RotateImg : MonoBehaviour
    {
        [SerializeField] Vector3 axis=Vector3.forward;
        [SerializeField] float speed=1;

        Image img;

        void Start()
        {
            img=GetComponent<Image>();
        }

        void Update()
        {
            img.rectTransform.Rotate( axis * speed * Time.deltaTime );
        }
    }

}
