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
        curlevel++;
        CurrentLevel = "Level_"+curlevel;
        if (SceneUtility.GetBuildIndexByScenePath(CurrentLevel) != -1)
        {
        SceneManager.LoadScene(CurrentLevel);
        }
        else
        {
        SceneManager.LoadScene("MainMenu");
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
}
