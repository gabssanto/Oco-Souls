using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    public void PlayGame ()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 1f).setOnComplete(() =>
        {
            SceneManager.LoadScene("MainLevel");
        });
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
