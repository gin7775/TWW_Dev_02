using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public int interaction;
    public void Interact() 
    { 
      
        if(interaction == 1)
        {
            loadNext1();
        }
        if (interaction == 2)
        {
            loadNext2();
        }

    }

    public void loadNext1()
    {
        GameManager.gameManager.NextScene(9);
    }
    public void loadNext2()
    {
        GameManager.gameManager.NextScene(10);
    }

}
