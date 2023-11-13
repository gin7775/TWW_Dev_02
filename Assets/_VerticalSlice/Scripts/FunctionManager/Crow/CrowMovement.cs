using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public Transform target;
    public float[] targetPositions = { 0,0,0,2,3};
    public float[] newPositions = { 0, 0, 0 ,2,3};
    public float[] positions = { 0, 0, 0  ,2,3};
    public float speed;
    public float posX,posY,posZ;
    public GameObject crowSkull,player;
    public Animator animation;

    //public Vector3 nextPosVector;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");



    }
    // Update is called once per frame
    void Update()
    {
        
       // MoveTowards(target);

        
    }
    public void MoveTowards(Transform target)
    {

        positions[0] = transform.position.x;
        positions[1] = transform.position.y;
        positions[2] = transform.position.z;
        targetPositions[0] = target.position.x;
        targetPositions[1] = target.position.y;
        targetPositions[2] = target.position.z;

        for (int i = 0; i <= 3; i++)
        {
            newPositions[i] = Mathf.Lerp(positions[i], targetPositions[i], speed * Time.deltaTime);
            //Debug.Log(i);  
        }

        posX = newPositions[0];
        posY = newPositions[1]; 
        posZ = newPositions[2];
        transform.position = new Vector3(posX, posY, posZ);



    }
}
