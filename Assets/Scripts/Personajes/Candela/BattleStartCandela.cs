using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartCandela : MonoBehaviour
{
    public Animator candela;
    public Candela ballerinas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        candela.SetTrigger("Idle");
        ballerinas.StartBallerinas();
    }
}
