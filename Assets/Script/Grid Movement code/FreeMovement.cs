using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovement : MonoBehaviour
{
    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
      
    }

    private void MovePlayer(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
