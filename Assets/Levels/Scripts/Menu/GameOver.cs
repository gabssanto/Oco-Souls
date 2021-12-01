using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    [SerializeField] RectTransform image;

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 2f).setOnComplete(() =>
        {
            image.gameObject.SetActive(true);
            LeanTween.alpha(fader, 1, 0);
            LeanTween.alpha(fader, 0, 3f).setOnComplete(() =>
            {
                LeanTween.alpha(fader, 0, 0);
                LeanTween.alpha(fader, 1, 2f).setOnComplete(() =>
                {
                    Application.Quit();
                });
            });
        });
    }
}
