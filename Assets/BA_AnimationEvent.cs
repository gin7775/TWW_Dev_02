using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_AnimationEvent : MonoBehaviour
{
    public GameObject holderProyectile1, holderProyectile2, holderImpactProyectile,weaponPoint,player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////ANIMATION EVENTS////////
    
    public void proyectileCombo()
    {
        // Puede dar problemas segun la posicion del arma.
        //Crear y colocar los holders y poner el punto del arma en el rig como punto de spawn.
        GameObject toInstantiate1 = Instantiate(holderProyectile1, weaponPoint.transform.position, Quaternion.identity);
        toInstantiate1.transform.LookAt(player.transform.position);
        toInstantiate1.GetComponent<Holder>().SetGun(2, 0.5f, 5, false);
        GameObject toInstantiate2 = Instantiate(holderProyectile2, weaponPoint.transform.position, Quaternion.identity);
        toInstantiate2.transform.LookAt(player.transform.position);
        toInstantiate2.GetComponent<Holder>().SetGun(2, 1, 5, false);

    }

    public void ImpactProyectile()
    {

        GameObject toInstantiate = Instantiate(holderImpactProyectile, weaponPoint.transform.position, Quaternion.identity);


    }


}
