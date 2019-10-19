using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public void OnBtnStart()
    {
        SceneManager.LoadScene("Stage_Forest");
    }

    public void OnBtnExit()
    {
        Application.Quit();
    }
}
