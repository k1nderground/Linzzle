using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect_Script : MonoBehaviour
{
    public void GoToLevelOne()
    {
        SceneManager.LoadScene("Level_1");
    }
}
