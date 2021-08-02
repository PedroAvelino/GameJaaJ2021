using System;
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

    private void Awake()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += ManageByIndex;
        ManageSound(sceneIndex);
    }

    private void ManageByIndex(Scene arg0, LoadSceneMode arg1)
    {
        int sceneIndex = arg0.buildIndex;
        ManageSound(sceneIndex);
    }

    private void OnEnable()
    {
        Dot.OnDeath += SetTryAgain;
        RuleTypeBase.OnRuleFailed += SetTryAgain;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isPlayerDead)
        {
            Time.timeScale = 1f;
            StartCoroutine(LoadScene(1));
        }
        else if(Input.GetKeyDown(KeyCode.R) && isPlayerDead)
        {
            Time.timeScale = 1f;
            StartCoroutine(LoadScene(0));
        }
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        backgroundAnimator.SetTrigger("Fade Out");
        yield return new WaitForSeconds(sceneLoadDelay);
        ManageSound(sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    private void ManageSound(int index)
    {
        switch (index)
        {
            case 0:
            case 1:
                AudioManager.instance.Stop("gameover");
                AudioManager.instance.Stop("credits");
                AudioManager.instance.Play("theme");
                break;

            case 2:
                AudioManager.instance.Stop("theme");
                AudioManager.instance.Play("credits");
                break;
        }
    }

    [ContextMenu("Try")]
    public void SetTryAgain()
    {
        if( tryAgainAnimator != null )
        {
            tryAgainAnimator.SetTrigger("Fade In");
        }
        isPlayerDead = true;
        Time.timeScale = 0.25f;
    }

    private void OnDisable()
    {
        Dot.OnDeath -= SetTryAgain;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }
}
