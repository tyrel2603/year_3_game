using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOnGrid : MonoBehaviour
{
    public float buffer = 0.5f;
    private bool isMoving;

    public LayerMask layer;

    private Vector3 origPos, targetPos;

    private float timeToMove = 0.2f;
    public Colours colour;
    public ObjectType objectType;
    private GridMovement Player;
    private bool activeKey = false;

    public Color wallLightColor; 
    public MeshRenderer[] wallLights;

    private void Start()
    {
        Player = GameObject.FindObjectOfType<GridMovement>();
    }

    public void SetActiveKey(bool value)
    {
        activeKey = value;
    }

    //The method below is called by the GridMovement script on the Player
    public bool OnTriggerPlayer(Collider collision)
    {
        //print("Collision");
        if (!isMoving && collision.gameObject.tag == "Player")
        {
            activeKey = true;//Because player is pushing me
            if (collision.transform.position.y > transform.position.y + buffer)
            {
                return CanMove(Vector3.down * 2);
            }
            else if (collision.transform.position.y < transform.position.y - buffer)
            {
                return CanMove(Vector3.up * 2);
            }
            else if (collision.transform.position.x > transform.position.x + buffer)
            {
                return CanMove(Vector3.left * 2);
            }
            else if (collision.transform.position.x < transform.position.x - buffer)
            {
                return CanMove(Vector3.right * 2);
            }

        }
        return false;
    }

    public void Move(Vector3 direction)
    {
        if (activeKey == false)
        {
            StartCoroutine(MovePlayer(direction));
        }
    }

    private void MoveAllKeys(Vector3 direction, Colours keyColour)
    {
        var allKeys = GameObject.FindObjectsOfType<KeyOnGrid>();
        print("found Keys " + allKeys.Length);
        foreach (var key in allKeys)
        {
            if (key.colour == keyColour)
            {
                key.Move(direction);
            }
            else
            {
                print("keys not same colour");
            }
        }
    }

    private void DestroyKey()
    {
        if (activeKey == false)
        {
            //  Destroy(gameObject);
            
            gameObject.SetActive(false); 
        }
    }

    private void DeleteAllKeys(Colours keyColour)
    {
        var allKeys = GameObject.FindObjectsOfType<KeyOnGrid>();
        foreach (var key in allKeys)
        {
            if (key.colour == keyColour)
            {
                key.DestroyKey();
            }
        }
        if (wallLights.Length > 0)
        {
            foreach (var light in wallLights)
            {
                light.material.EnableKeyword("_EMISSION");
                light.material.SetColor("_EmissionColor", wallLightColor);
            }
        }
    }

    bool CanMove(Vector3 direction)
    {

        origPos = transform.position;

        targetPos = origPos + direction;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, targetPos - transform.position, out hit, Vector3.Distance(transform.position, targetPos), layer))
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos - transform.position, Vector3.Distance(transform.position, targetPos), layer);
        //if (hit.collider != null)
        {
            //print(hit.collider.tag + " From the key");
            if (hit.collider.tag.Substring(0, 4) == "Door")
            {
                var theDoor = hit.collider.GetComponent<GridDoor>();
                if (DeleteDoors(theDoor))
                {
                    //StartCoroutine(MovePlayer(direction));
                    DeleteAllKeys(colour);
                    activeKey = false;
                    // Destroy(gameObject);
                    gameObject.SetActive(false);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (hit.collider.tag != "Wall")
            {
                MoveAllKeys(direction, colour);
                StartCoroutine(MovePlayer(direction));
                return true;
            }
            else
            {
                //the wall, so dont move
                return false;
            }
        }
        else
        {
            MoveAllKeys(direction, colour);
            StartCoroutine(MovePlayer(direction));
            return true;
        }
    }


    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
        activeKey = false;
    }

    bool DeleteDoors(GridDoor touchedDoor)
    {
        if (touchedDoor.GetColour() == this.colour)
        {
            var doors = GameObject.FindGameObjectsWithTag("Door" + this.colour.ToString());
            foreach (var door in doors)
            {
                door.GetComponent<GridDoor>().DestroyDoor();
            }

            return true;
        }

        return false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        print ("Collision");
        if (!isMoving && collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.y > transform.position.y + buffer)
            {
                CanMove(Vector3.down * 2);
            }
            else if (collision.transform.position.y < transform.position.y - buffer)
            {
                CanMove(Vector3.up * 2);
            }
            else if (collision.transform.position.x > transform.position.x + buffer)
            {
                CanMove(Vector3.left * 2);
            }
            else if (collision.transform.position.x < transform.position.x - buffer)
            {
                CanMove (Vector3.right * 2);
            }
        }
    }*/
}