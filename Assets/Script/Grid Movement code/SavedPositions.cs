using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPositions : MonoBehaviour
{
   public List<List<ObjectInGame>> positions = new List<List<ObjectInGame>>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (positions.Count >= 1)
            {
                Undo();
            }
        }
    }

    void Undo()
    {
        var index = positions.Count - 1;
        foreach (var pos in positions[index])
        {
            pos.item.transform.position = pos.position;
            pos.item.SetActive(pos.visibility);
        }
        positions.RemoveAt(positions.Count - 1);
    }
}
