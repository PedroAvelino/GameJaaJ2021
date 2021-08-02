using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
     void PlaySund()
    {
        AudioManager.instance.Play("button");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
