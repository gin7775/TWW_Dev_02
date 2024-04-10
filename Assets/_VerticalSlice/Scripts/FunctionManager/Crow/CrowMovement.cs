using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public Transform currentTarget;

    public Transform[] targets; 
    public float[] targetPositions = { 0,0,0,2,3};
    public float[] newPositions = { 0, 0, 0 ,2,3};
    public float[] positions = { 0, 0, 0, 2, 3 };
    public float speed;
    public float posX,posY,posZ;
    public int index;
    public GameObject crowSkull,player,crowHolder;
    public Animator animationCrow, IAanimator;
    public CrowDirector directorIA;

    //public Vector3 nextPosVector;
    void Start()
    {
        index = 0;
        currentTarget = targets[index];
        IAanimator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        

    }
    // Update is called once per frame
    void Update()
    {
        
       // MoveTowards(target[]);

        
    }
    public void MoveTowards()
    {

        positions[0] = transform.position.x;
        positions[1] = transform.position.y;
        positions[2] = transform.position.z;
        targetPositions[0] = currentTarget.position.x;
        targetPositions[1] = currentTarget.position.y;
        targetPositions[2] = currentTarget.position.z;

        for (int i = 0; i <= 3; i++)
        {
            newPositions[i] = Mathf.Lerp(positions[i], targetPositions[i], speed * Time.deltaTime);
            //Debug.Log(i);  
        }

        posX = newPositions[0];
        posY = newPositions[1]; 
        posZ = newPositions[2];
        transform.position = new Vector3(posX, posY, posZ);
        if (Vector3.Distance(transform.position, currentTarget.position) <= 1)
        {
            IAShoot();
            index++;

        }
        
            if (index >= targets.Length)
            {
             index = 0;
             currentTarget = targets[index];
            }
        currentTarget = targets[index];

    }
      public void IAMoveCall()
      {

        IAanimator.SetTrigger("MoveToPosition");

      }
      public void IAShoot()
      {

        IAanimator.SetTrigger("Shoot");

      }
      public void CrowShoot()
      {
        GameObject toInstanciate = Instantiate(crowHolder, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        toInstanciate.GetComponent<Holder>().SetGun(1, 0.01f, 5, true);



      }

}
