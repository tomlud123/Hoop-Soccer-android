
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Crazy { 

    public class LoadNextScene : MonoBehaviour
    {
        [SerializeField] float withDelay=2;

        void Start()
        {
            Invoke("loadNextScene", withDelay);
        }

        void loadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

}
