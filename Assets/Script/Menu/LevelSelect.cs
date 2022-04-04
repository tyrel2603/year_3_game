using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("3DGrid");
    }

    public void Level2()
    {
      SceneManager.LoadScene("3DGrid 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("3DGrid 3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("GridLevel 4");
    }
    public void Level5()
    {
        SceneManager.LoadScene("GridLevel 5");
   }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
