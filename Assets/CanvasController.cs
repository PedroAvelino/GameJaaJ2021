using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public Animator backgroundAnimator;
    public Animator tryAgainAnimator;
    [SerializeField] float sceneLoadDelay;
    private bool isPlayerDead;


    private void OnEnable()
    {
        Dot.OnDeath += SetTryAgain;        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isPlayerDead)
        {
            Time.timeScale = 1f;
            LoadScene(0);
        }
        else if(Input.GetKeyDown(KeyCode.R) && isPlayerDead)
        {
            Time.timeScale = 1f;
            LoadScene(1);
        }
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        backgroundAnimator.SetTrigger("Fade Out");
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneIndex);
    }

    [ContextMenu("Try")]
    public void SetTryAgain()
    {
        tryAgainAnimator.SetTrigger("Fade In");
        isPlayerDead = true;
        Time.timeScale = 0.25f;
    }

    private void OnDisable()
    {
        Dot.OnDeath -= SetTryAgain;
    }
}
