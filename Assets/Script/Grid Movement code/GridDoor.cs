using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDoor : MonoBehaviour
{
    public Colours colour;

    public ObjectType objectType;

    public void DestroyDoor()
    {
        //Destroy(gameObject,0.25f);
        gameObject.SetActive(false);
    }

    public ObjectType GetType()
    {
        return objectType;
    }

    public Colours GetColour()
    {
        return colour;
    }
}

