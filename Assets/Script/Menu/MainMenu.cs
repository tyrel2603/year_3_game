using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
  public void PlayGame ()
    {
        SceneManager.LoadScene("3DGrid_Easy1");
    }

   public void LevelSelect()
    {
       SceneManager.LoadScene("LevelSelect");
        
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

}
