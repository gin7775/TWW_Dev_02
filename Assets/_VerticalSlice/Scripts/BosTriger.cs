using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosTriger : MonoBehaviour
{
    public Animator bosStarterAnim;
    public GameObject otherTriger;
    // Start is called before the first frame update
    void Start()
    {
        bosStarterAnim = GameObject.FindGameObjectWithTag("Armore").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bosStarterAnim.SetTrigger("Action");
            Destroy(otherTriger);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
