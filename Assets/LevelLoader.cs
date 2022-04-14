using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            SceneManager.LoadScene("3DGrid_Easy1");


            SceneManager.LoadScene("LevelSelect");



            SceneManager.LoadScene("HowToPlay");


            SceneManager.LoadScene("Main Menu");

            Application.Quit();
        }
    
    }
}
