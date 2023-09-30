using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToTheNextSceneOnJump : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
        }
    }
}
