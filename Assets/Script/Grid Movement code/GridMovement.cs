using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{

    private bool isMoving;

    private Vector3 origPos, targetPos;

    private float timeToMove = 0.2f;

    public LayerMask layer;

    public GameObject[] players;

    private GameObject currentPlayer;

    private int playerIndex = 0;

    private SavedPositions savedPositions;

    private List<ObjectInGame> objectInGames;

    public Transform World;

    public float RotateSpeed;

    private float Angle = 0;
    private bool Rotating = false;

    private float[] TargetAngle = new float[] {180,360};
    private int Index = 0;
    private Footsteps footsteps;

    void Start()
    {
        currentPlayer = players[playerIndex];
        savedPositions = GetComponent<SavedPositions>(); 
        footsteps = GetComponent<Footsteps>();
    }

    void Update()
    {
        if (Rotating == true)
        {
            Angle += RotateSpeed * Time.deltaTime;
            if (Angle > TargetAngle[Index])
            {
                Rotating = false;
                Angle = TargetAngle[Index];
                Index += 1;
                if (Index == 2)
                {
                    Index = 0;
                    Angle = 0;
                }
            }
            World.eulerAngles = new Vector3(0, Angle, 0);
        }
        if (Input.GetKey(KeyCode.W) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.up*2);
        }
        if (Input.GetKey(KeyCode.A) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.left*2);
        }
        if (Input.GetKey(KeyCode.S) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.down*2);
        }
        if (Input.GetKey(KeyCode.D) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.right*2);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isMoving == false)
        {
            footsteps.Rotate();
            Rotating = true;
            playerIndex += 1;
            if (playerIndex == players.Length)
            {
                playerIndex = 0;
            }

            currentPlayer = players[playerIndex];
       
        }
    }

    void CanMove(Vector3 direction)
    {
        print("CanMove");
        origPos = currentPlayer.transform.position;

        targetPos = origPos + direction;

        RaycastHit hit;
        Debug.DrawRay(currentPlayer.transform.position, targetPos - currentPlayer.transform.position, Color.red);
         if(Physics.Raycast(currentPlayer.transform.position, targetPos - currentPlayer.transform.position, out hit, Vector3.Distance(currentPlayer.transform.position, targetPos), layer))
        {
           
            //print(hit.collider.tag);
            if (hit.collider.tag.Substring(0,3) == "Key")
            {
                print("KeyCollide");
                //savedPositions.positions.Add();
                bool canMove = hit.collider.GetComponent<KeyOnGrid>().OnTriggerPlayer(currentPlayer.GetComponent<Collider>());
                if (canMove == true)
                {
                    footsteps.Walk();
                    SaveObjectInGame();
                    StartCoroutine(MovePlayer(direction));
                }
            }
            else if (hit.collider.tag != "Wall" && hit.collider.tag.Substring(0, 4) != "Door")
            {
                footsteps.Walk();
                SaveObjectInGame();
                StartCoroutine(MovePlayer(direction));
            }
        }
        else
        {
            footsteps.Walk();
            SaveObjectInGame();
            StartCoroutine(MovePlayer(direction));
        }
        
        
        
    }



    private IEnumerator MovePlayer(Vector3 direction)
    {
        
        isMoving = true;

        float elapsedTime = 0;

        origPos = currentPlayer.transform.position;

        targetPos = origPos + direction;


            while (elapsedTime < timeToMove)
            {
                currentPlayer.transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        
        

            currentPlayer.transform.position = targetPos;

        yield return new WaitForSeconds(0.1f);

        isMoving = false;
    }

    void SaveObjectInGame()
    {
        var OBS = GameObject.FindObjectsOfType<ObjectStatus>();
        //print("Object found: " + OBS.Length);
        objectInGames = new List<ObjectInGame>();
        foreach (var item in OBS)
        {
            var OB = new ObjectInGame();
            OB.item = item.gameObject;
            OB.position = item.transform.position;
            OB.visibility = item.gameObject.activeSelf;
            objectInGames.Add(OB);
            print(objectInGames[0].item.tag);

        }
        if (savedPositions != null)
        {
            savedPositions.positions.Add(objectInGames);
        }
        else
        {
            print("Saved positions does not exist");
        }
    }

}
