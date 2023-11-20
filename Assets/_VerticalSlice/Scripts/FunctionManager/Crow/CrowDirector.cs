using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDirector : MonoBehaviour
{
    public int crowIndex;
    public GameObject crowSkull;
    public GameObject[] crowsPack;
    public float maskTimer;
    // Start is called before the first frame update
    void Start()
    {
        maskTimer = Random.Range(5f,9f);
    }

    // Update is called once per frame
    void Update()
    {
        maskTimer -= Time.deltaTime;
        if (maskTimer <= 0)
        {
            if (Random.Range(0, 2) >= 1)
            {
                maskChange();
            }
            maskTimer = Random.Range(5f, 9f);
        }
    }
    public void MoveCall()
    {
        for (int i = 0; i <= crowsPack.Length; i++)
        {
            //crowsPack[i].GetComponent<CrowMovement>().MoveTowards(crowsPack[i].GetComponent<CrowMovement>().currentTarget);
            crowsPack[i].GetComponent<CrowMovement>().IAMoveCall();
        }
    }
    public void maskChange()
    {
        crowIndex = Random.Range(0, crowsPack.Length);//Revisar el valor excluido
        if (crowsPack[crowIndex] != null)
        {

          crowSkull.transform.position = crowsPack[crowIndex].transform.position;

        }

    }
}
