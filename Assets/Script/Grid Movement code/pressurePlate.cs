using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressurePlate : MonoBehaviour
{
    public pressurePlate OtherPlate;
    private bool onPlate = false;
    public string next = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlate = true;
            if (OtherPlate == null )
            {
                SceneManager.LoadScene(next);
            }
            else if (OtherPlate.onPlate)
            {

                SceneManager.LoadScene(next);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlate = false;
        }
    }
}
