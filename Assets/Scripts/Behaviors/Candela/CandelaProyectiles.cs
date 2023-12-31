using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelaProyectiles : MonoBehaviour
{
    public int shootNumber = 1;
    public float fireSpeed = 0.1f;
    public GameObject player, proyectile, target;

    public GameObject[] targets;

    // Start is called before the first frame update
    void Start()
    {
        CircleGun();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CircleGun()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject x = Instantiate(target, this.transform.position, Quaternion.identity);
            targets[i] = x;
        }

        for (int i = 0; i < targets.Length; i++)
        {

            targets[i].transform.position = new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z);
            targets[i].transform.RotateAround(this.transform.position, new Vector3(0, 1, 0), i * 360 / targets.Length);
            targets[i].GetComponent<Gun_Test>().fireRate = fireSpeed;
            targets[i].GetComponent<Gun_Test>().shoots = shootNumber;


        }
        Destroy(this.gameObject, 1);
    }
}
