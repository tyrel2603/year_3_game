using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{

    private bool isMoving;
    private bool touchingKey;

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
    public GameObject[] backgrounds;
    private int Index = 0;
    private Footsteps footsteps;
    public Animator[] anim;

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
                backgrounds[0].SetActive(false);
                backgrounds[1].SetActive(false);
                backgrounds[Index].SetActive(true);
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
           // currentPlayer.transform.parent.eulerAngles = new Vector3(0, 0, -90);
            CanMove(Vector3.up*2);
        }
        else if (Input.GetKey(KeyCode.A) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.left*2);
        }
        else if(Input.GetKey(KeyCode.S) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.down*2);
        }
        else if(Input.GetKey(KeyCode.D) && !isMoving && Rotating == false)
        {
            CanMove(Vector3.right*2);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isMoving == false)
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
        if (isMoving == true && touchingKey == true)
        {
            anim[Index].SetTrigger("Push");
        }
        else if (isMoving == true)
        {
            anim[Index].SetTrigger("Walk");
        }
        else
        {
            anim[Index].SetTrigger("Idle");
        }
    }

    void CanMove(Vector3 direction)
    {
        print("CanMove");
        origPos = currentPlayer.transform.position;
        Debug.Log($"Direction={direction}");
        currentPlayer.transform.rotation = Quaternion.LookRotation(direction,Vector3.back);

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
                    touchingKey = true;     
                    footsteps.Walk();
                    SaveObjectInGame();
                    StartCoroutine(MovePlayer(direction));
                }
            }
            else if (hit.collider.tag != "Wall" && hit.collider.tag.Substring(0, 4) != "Door")
            {
                touchingKey = false;             
                footsteps.Walk();
                SaveObjectInGame();
                StartCoroutine(MovePlayer(direction));
            }
        }
        else
        {
            touchingKey=(false);         
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

        yield return new WaitForSeconds(0.25f);

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
