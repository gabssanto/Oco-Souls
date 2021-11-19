using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    private void Start()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 1, 0);
        LeanTween.alpha(fader, 0, 2f).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }

    public void OpenMenuScene()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 2f);
    }

    public void OpenGameScene()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 1, 0);
        LeanTween.alpha(fader, 0, 2f);
    }
}
