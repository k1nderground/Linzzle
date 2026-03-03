using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Buttons_Script : MonoBehaviour
{
  public void GoToLevelSelect(){
    SceneManager.LoadScene("LevelSelect");
  }
}
