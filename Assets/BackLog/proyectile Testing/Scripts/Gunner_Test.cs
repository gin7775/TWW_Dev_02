using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner_Test : MonoBehaviour
{
    public int shootNumber = 1;
    public float fireSpeed = 1;
    public GameObject player, proyectile, target ,target2;
    public SlashShooter shooter_;
    public GameObject[] targets;
    public float slashDistance;
    public Vector3 proyectilePos;
    public bool secondFace;
    public int proyectileNumber;
    // Start is called before the first frame update
    void Start()
    {

        //shooter_ = this.gameObject.GetComponent<SlashShooter>();
        
        //CircleGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CircleGun()
    {
        Debug.Log("Hay circulitos");
        if(secondFace == false)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject x;


                x = Instantiate(target, proyectilePos, Quaternion.identity);



                targets[i] = x;
            }

            for (int i = 0; i < targets.Length; i++)
            {

                targets[i].transform.position = new Vector3(proyectilePos.x + 2, proyectilePos.y, proyectilePos.z);
                targets[i].transform.RotateAround(proyectilePos, new Vector3(0, 1, 0), i * 360 / targets.Length);
                /*shooter_ = targets[i].GetComponent<SlashShooter>();
                shooter_.timeToFire = Time.time + 1 / shooter_.fireRate;
                shooter_.destination = new Vector3(targets[i].transform.position.x, targets[i].transform.position.y, targets[i].transform.position.z + slashDistance)  ;
                shooter_.ShootProjectile();*/
                targets[i].GetComponent<Gun_Test>().fireRate = fireSpeed * Time.deltaTime;
                targets[i].GetComponent<Gun_Test>().shoots = shootNumber;


            }
        }
        else
        {
            GameObject x = Instantiate(target2, proyectilePos, Quaternion.identity);
        }
        
        //Destroy(this.gameObject,2f);
    }
}
