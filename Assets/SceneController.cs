using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator backgroundAnimator;
    public static SceneController sceneController;
    [SerializeField] float sceneLoadDelay;


    private void Awake()
    {
        if (sceneController == null)
        {
            sceneController = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        backgroundAnimator.SetTrigger("Fade Out");
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneIndex);
    }

    [ContextMenu("Call Scene")]
    void CallScene()
    {
        StartCoroutine(LoadScene(1));
    }
}
