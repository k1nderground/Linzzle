using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public string CurrentLevel;
    [SerializeField] public string BeautyLevel;
    [SerializeField] public int curlevel;

    void Start()
    {
        CurrentLevel = "Level_"+curlevel;
        BeautyLevel = "Уровень "+curlevel;
    }

public void nextLevel()
{
    Debug.Log("NEXT LEVEL CALLED");

    curlevel++;
    CurrentLevel = "Level_" + curlevel;

    Debug.Log("Trying to load: " + CurrentLevel);

    if (Application.CanStreamedLevelBeLoaded(CurrentLevel))
    {
        Debug.Log("Scene exists, loading...");
        SceneManager.LoadScene(CurrentLevel);
    }
    else
    {
        Debug.Log("Scene NOT found, loading menu");
        SceneManager.LoadScene("MainMenu");
    }
}

    public void restartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
}
