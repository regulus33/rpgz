﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Tilemap theMap;

    private Vector3 bottomLeftLimit;

    private Vector3 topRightLimit;

    private float halfHeight;

    private float halfWidth; 

    public float transitionSpeed;
    Vector3 newRot;
    Vector3 relPos;

    private GameObject transitionChangeTarget; 

    public static CameraController instance;

    public float distanceToMove = 0.2f;

    private Vector3 flatPosition;
    
    public int musicToPlay;

    private bool musicStarted;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
          //get around script execution order
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize; //current height of camera, measures the height of the screen and chops in hald to find camera
        halfWidth = halfHeight * Camera.main.aspect; //aspect ratio of screen

        //smallest axis y and axis x of object, so bottom left
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        //highest x and y numbers of obj will be top right 
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
        //the camera is responsible for telling the player where his boundaries are, calling and instance method we set on player
        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // Update is called once per frame after Update
    void LateUpdate()
    {
        //this lateupdate must run after camera is destroyed and throws a null reference error, transform is this's attribute for pos
            if(transitionChangeTarget != null)
            {
                relPos = transitionChangeTarget.transform.position - transform.position;
                newRot = new Vector3(relPos.x, relPos.y, -10f);

                flatPosition = new Vector3(transitionChangeTarget.transform.position.x, transitionChangeTarget.transform.position.y, -10f);
                transform.position = Vector3.MoveTowards(transform.position, flatPosition, distanceToMove);

            } else {
                transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
                //keep the camera inside the bounds  mathfunctions.clamp keeps a value between a min and max number so cam position never exceeds boundary
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
            }
                                    
       
       
    }

    public void ChangeTarget(GameObject gameOb)
    {
        transitionChangeTarget = gameOb;
    }
}
